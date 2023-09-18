using PicacomicSharp.DependencyInjection;

namespace PicacomicSharp;

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