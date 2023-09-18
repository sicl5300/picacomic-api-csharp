using System.Text;
using System.Text.Json;
using PicacomicSharp.DependencyInjection;
using PicacomicSharp.Requests.Abstractions;
using PicacomicSharp.Responses.Abstractions;

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

    internal async Task<TResponse> SendAsync<TRequest, TResponse>(TRequest config)
        where TResponse : IResponseData
        where TRequest : IRequestData
    {
        var method = config switch
        {
            IPost => HttpMethod.Post,
            IGet => HttpMethod.Get,
            _ => throw new ArgumentOutOfRangeException(nameof(config), config, null)
        };
        
        var requestMessage = new HttpRequestMessage(method, config.Url);
        
        if (config is IPost)
        {
            requestMessage.Content = new StringContent(JsonSerializer.Serialize(config), Encoding.UTF8, "application/json");
        }
        
        var response = await Client.SendAsync(requestMessage);
        var message = await response.Content.ReadAsStringAsync();

        var wrapped = JsonSerializer.Deserialize<WrappedResponse<TResponse>>(message, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        if (wrapped.Data is not null) return wrapped.Data;

        if (wrapped.Error is not null) // Data is null, but Error is not null
            throw new InvalidOperationException($"Error: {wrapped.Error}, Message: {wrapped.Message ?? "None"}.");

        throw new InvalidOperationException("Invalid response.");
    }
}