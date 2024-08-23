using Proton.Security.Cryptography.Abstractions;
using Proton.Security.Cryptography.GopenPgp;

namespace ProtonDrive.BlockVerification;

internal sealed class BlockVerifier : IBlockVerifier
{
    private const int MaxVerificationLength = 16;

    private readonly PgpSessionKey _sessionKey;
    private readonly ReadOnlyMemory<byte> _verificationCode;

    private BlockVerifier(PgpSessionKey sessionKey, ReadOnlyMemory<byte> verificationCode)
    {
        _sessionKey = sessionKey;
        _verificationCode = verificationCode;
    }

    public static async Task<BlockVerifier> CreateAsync(
        string shareId,
        string linkId,
        string revisionId,
        PrivatePgpKey nodeKey,
        IRevisionVerificationApiClient revisionVerificationApiClient,
        CancellationToken cancellationToken)
    {
        var verificationInput = await revisionVerificationApiClient.GetVerificationInputAsync(shareId, linkId, revisionId, cancellationToken)
            .ConfigureAwait(false);

        var keyPacketDecrypter = new KeyBasedPgpDecrypter(new[] { nodeKey });
        var sessionKey = keyPacketDecrypter.DecryptSessionKey(verificationInput.ContentKeyPacket);

        return new(sessionKey, verificationInput.VerificationCode);
    }

    public VerificationToken VerifyBlock(ReadOnlySpan<byte> dataPacketPrefix, ReadOnlySpan<byte> plainDataPrefix)
    {
        var verificationLength = Math.Min(MaxVerificationLength, plainDataPrefix.Length);
        var isMatch = _sessionKey.MatchesDataPacketPrefix(dataPacketPrefix, plainDataPrefix[..verificationLength]);

        if (!isMatch)
        {
            throw new SessionKeyAndDataPacketMismatchException();
        }

        return VerificationToken.Create(_verificationCode.Span, dataPacketPrefix);
    }
}
