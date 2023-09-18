using PicacomicSharp.DependencyInjection;

namespace PicacomicSharp;

/// <summary>
///     用来发送API请求的 Client，推荐使用 DI Container 来获取实例。
/// </summary>
public class PicaClient
{
    public HttpClient Client { get; private set; }

    public PicaClient(PicaConfiguration configuration, HttpClient client)
    {
        client.BaseAddress = new Uri(configuration.BaseUrl);
        client.Timeout = TimeSpan.FromSeconds(configuration.Timeout);
        foreach (var pair in configuration.DefaultHeaders)
        {
            client.DefaultRequestHeaders.Add(pair.Key, pair.Value);
        }

        Client = client;
    }
}