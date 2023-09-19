using System.Text.Json.Serialization;

namespace PicacomicSharp.Responses.Abstractions;

/// <summary>
///     标准API返回内容的包装，包含<see cref="Code" />、<see cref="Error" />、<see cref="Message" />和<see cref="Data" />。
/// </summary>
/// <typeparam name="T"><see cref="IResponseData" />，即包装在内的<see cref="Data"/></typeparam>
internal sealed class WrappedResponse<T> where T : IResponseData
{
    [JsonPropertyName("code")]
    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
    public int Code { get; set; }

    /// <summary>
    ///     有错误时才不为<c>null</c>，表示错误代码。
    /// </summary>
    [JsonPropertyName("error")]
    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
    public int? Error { get; set; } = default;

    [JsonPropertyName("message")]
    public string? Message { get; set; } = default;

    /// <summary>
    ///     实际数据。
    /// </summary>
    [JsonPropertyName("data")]
    public T? Data { get; set; } = default;

    public bool IsSuccess => Code == 200;
    public bool IsError => Error is not null;
}