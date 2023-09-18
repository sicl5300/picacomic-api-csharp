using System.Text.Json.Serialization;
using System.Web;
using PicacomicSharp.Common;

namespace PicacomicSharp.Requests;

/// <summary>
///     用户注册请求
/// </summary>
public class RequestRegister : IPost
{
    public RequestRegister(Gender gender)
    {
        Gender = gender;
        GenderString = Gender.ToApiString();
        Email = HttpUtility.HtmlEncode(Email)!;
    }

    /// <summary>
    ///     用户名或邮箱，如果是邮箱则需要进行UrlEncode（你需要自己进行，这里不会自动进行UrlEncode！）
    /// </summary>
    [JsonPropertyName("email")]
    public required string Email { get; set; }

    [JsonPropertyName("password")] public required string Password { get; init; }

    [JsonPropertyName("name")] public required string Name { get; init; }

    [JsonPropertyName("birthday")] public required string Birthday { get; init; }

    [JsonIgnore] public Gender Gender { get; set; }

    [JsonPropertyName("gender")] public string GenderString { get; init; }

    [JsonPropertyName("question1")] public required string Question1 { get; init; }

    [JsonPropertyName("answer1")] public required string Answer1 { get; init; }

    [JsonPropertyName("question2")] public required string Question2 { get; init; }

    [JsonPropertyName("answer2")] public required string Answer2 { get; init; }

    [JsonPropertyName("question3")] public required string Question3 { get; init; }

    [JsonPropertyName("answer3")] public required string Answer3 { get; init; }
    public string Url => "auth/register";
}