using System.Text.Json.Serialization;

namespace PicacomicSharp.Responses.Common;
#pragma warning disable CS8618 // 在退出构造函数时，不可为 null 的字段必须包含非 null 值。请考虑声明为可以为 null。
/// <summary>
///     用户信息
/// </summary>
public sealed class User
{
    /// <summary>
    ///     用户唯一ID
    /// </summary>
    [JsonPropertyName("_id")]
    public string Id { get; set; }

    /// <summary>
    ///     用户名或邮箱，不是显示昵称
    /// </summary>
    [JsonPropertyName("email")]
    public string Email { get; set; }

    /// <summary>
    ///     显示昵称
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; }

    /// <summary>
    ///     性别
    /// </summary>
    [JsonPropertyName("gender")]
    public string Gender { get; set; }

    /// <summary>
    ///     个人头衔（？）
    /// </summary>
    [JsonPropertyName("title")]
    public string Title { get; set; }

    /// <summary>
    ///     个人简介
    /// </summary>
    [JsonPropertyName("slogan")]
    public string Slogan { get; set; }

    /// <summary>
    ///     未知
    /// </summary>
    [JsonPropertyName("verified")]
    public bool Verified { get; set; }

    /// <summary>
    ///     经验值
    /// </summary>
    [JsonPropertyName("exp")]
    public int Exp { get; set; }

    /// <summary>
    ///     用户等级
    /// </summary>
    [JsonPropertyName("level")]
    public int Level { get; set; }

    /// <summary>
    ///     上传者等级-骑士、管理员等 角色名称
    /// </summary>
    [JsonPropertyName("characters")]
    public string[] Characters { get; set; }

    [JsonPropertyName("birthday")]
    public DateTime Birthday { get; set; }

    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }

    /// <summary>
    ///     用户头像
    /// </summary>
    [JsonPropertyName("avatar")]
    public ResourceUrl Avatar { get; set; }

    /// <summary>
    ///     用户头像边框
    /// </summary>
    [JsonPropertyName("character")]
    public string AvatarFrame { get; set; }

    /// <summary>
    ///     是否签到
    /// </summary>
    [JsonPropertyName("isPunched")]
    public bool IsPunched { get; set; }
}