using Vienna.Test.API.Services.Implementation;
using Vienna.Test.API.Services;

namespace Vienna.Test.API.Extensions
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddGitHubService(
        this IServiceCollection services,
            Uri baseApiUrl)
        {
            services.AddHttpClient<IGithubService, GithubService>(client =>
            {
                client.BaseAddress = baseApiUrl;
            });

            return services;
        }
    }
}
