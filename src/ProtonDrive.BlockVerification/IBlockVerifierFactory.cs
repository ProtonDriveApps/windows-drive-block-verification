using Proton.Security.Cryptography.Abstractions;

namespace ProtonDrive.BlockVerification;

public interface IBlockVerifierFactory
{
    Task<IBlockVerifier> CreateAsync(string shareId, string linkId, string revisionId, PrivatePgpKey nodeKey, CancellationToken cancellationToken);
}
