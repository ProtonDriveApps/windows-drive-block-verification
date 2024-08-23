using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace ProtonDrive.BlockVerification;

public static class ServiceCollectionExtensions
{
    public static void AddBlockVerification(this IServiceCollection services, string httpClientName, RefitSettings settings)
    {
        services.AddSingleton<IBlockVerifierFactory, BlockVerifierFactory>();

        services.AddSingleton(_ => RequestBuilder.ForType<IRevisionVerificationApiClient>(settings));

        services.AddTransient(sp =>
        {
            var httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();
            var httpClient = httpClientFactory.CreateClient(httpClientName);

            return RestService.For(httpClient, sp.GetRequiredService<IRequestBuilder<IRevisionVerificationApiClient>>());
        });
    }
}
