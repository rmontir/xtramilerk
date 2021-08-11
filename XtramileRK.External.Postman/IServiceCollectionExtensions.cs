using Microsoft.Extensions.DependencyInjection;

namespace XtramileRK.External.Postman
{
    public static class IServiceCollectionExtensions
    {
        public static void AddPostmanServices(this IServiceCollection services)
        {
            services.AddSingleton<ICitiesService, CitiesService>();
        }
    }
}
