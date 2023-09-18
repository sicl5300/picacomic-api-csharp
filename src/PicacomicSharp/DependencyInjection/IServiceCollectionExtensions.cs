using Microsoft.Extensions.DependencyInjection;

namespace PicacomicSharp.DependencyInjection;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddPicacomicSharp(this IServiceCollection services,
        Action<PicaConfiguration>? configure = null)
    {
        var configuration = new PicaConfiguration();
        configure?.Invoke(configuration);

        services.AddSingleton<PicaConfiguration>(configuration);
        services.AddTransient<PicaHttpClientHandler>();
        services.AddHttpClient<PicaClient>()
            .ConfigurePrimaryHttpMessageHandler<PicaHttpClientHandler>();
        
        return services;
    }
}