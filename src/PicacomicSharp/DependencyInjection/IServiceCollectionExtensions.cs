using Microsoft.Extensions.DependencyInjection;

namespace PicacomicSharp.DependencyInjection;

public static class IServiceCollectionExtensions
{
    /// <summary>
    ///     添加 PicacomicSharp 到依赖注入容器中。依赖于 Microsoft.Extensions.DependencyInjection。
    ///     TODO 抽离出本项目，使本项目不依赖于 Microsoft.Extensions.DependencyInjection。
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configure">设置，具体见<see cref="PicaConfiguration"/></param>
    /// <returns></returns>
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