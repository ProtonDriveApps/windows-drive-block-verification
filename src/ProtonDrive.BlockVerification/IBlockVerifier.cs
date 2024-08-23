namespace ProtonDrive.BlockVerification;

public interface IBlockVerifier
{
    VerificationToken VerifyBlock(ReadOnlySpan<byte> dataPacketPrefix, ReadOnlySpan<byte> plainDataPrefix);
}
