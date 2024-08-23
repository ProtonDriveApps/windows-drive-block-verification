using Proton.Security.Cryptography.Abstractions;

namespace ProtonDrive.BlockVerification;

internal sealed class BlockVerifierFactory : IBlockVerifierFactory
{
    private readonly IRevisionVerificationApiClient _revisionVerificationApiClient;

    public BlockVerifierFactory(IRevisionVerificationApiClient revisionVerificationApiClient)
    {
        _revisionVerificationApiClient = revisionVerificationApiClient;
    }

    public async Task<IBlockVerifier> CreateAsync(string shareId, string linkId, string revisionId, PrivatePgpKey nodeKey, CancellationToken cancellationToken)
    {
        return await BlockVerifier.CreateAsync(shareId, linkId, revisionId, nodeKey, _revisionVerificationApiClient, cancellationToken).ConfigureAwait(false);
    }
}
