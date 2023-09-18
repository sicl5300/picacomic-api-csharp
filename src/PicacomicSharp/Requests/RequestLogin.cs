using System.Text.Json.Serialization;

namespace PicacomicSharp.Requests;

/// <summary>
///     登陆请求参数
/// </summary>
public sealed class RequestLogin : IPost
{
    /// <summary>
    ///     电子邮箱或者用户名。一般使用用户名。
    /// </summary>
    /// <example>"your-username"</example>
    [JsonPropertyName("email")]
    public required string Email { get; init; }

    /// <summary>
    ///     明文密码，无任何加密。
    /// </summary>
    [JsonPropertyName("password")]
    public required string Password { get; init; }

    [JsonIgnore] string IRequestData.Url => "auth/sign-in";
}