using Refit;

namespace ProtonDrive.BlockVerification;

internal interface IRevisionVerificationApiClient
{
    [Get("/shares/{shareId}/links/{linkId}/revisions/{revisionId}/verification")]
    [Headers("Authorization: Bearer")]
    public Task<VerificationInputResponse> GetVerificationInputAsync(string shareId, string linkId, string revisionId, CancellationToken cancellationToken);
}
