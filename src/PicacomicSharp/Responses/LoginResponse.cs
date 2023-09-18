using System.Text.Json.Serialization;

// ReSharper disable ClassNeverInstantiated.Global
namespace PicacomicSharp.Responses;

public sealed class LoginResponse : IResponseData
{
    /// <summary>
    ///     认证令牌。可能在7天后过期。
    /// </summary>
    [JsonPropertyName("token")]
    public required string Token { get; set; } = string.Empty;
}