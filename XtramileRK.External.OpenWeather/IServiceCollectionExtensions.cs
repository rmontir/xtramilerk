using System;
using Microsoft.Extensions.DependencyInjection;

namespace XtramileRK.External.OpenWeather
{
    public static class IServiceCollectionExtensions
    {
        public static void AddOpenWeatherServices(this IServiceCollection services, Action<OpenWeatherOptions> setupAction)
        {
            services.AddOptions<OpenWeatherOptions>();
            services.AddSingleton<IWeatherService, WeatherService>();
            services.Configure(setupAction);
        }
    }
}
