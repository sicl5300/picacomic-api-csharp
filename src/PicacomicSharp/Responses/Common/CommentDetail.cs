using System.Text.Json.Serialization;

namespace PicacomicSharp.Responses.Common;
#pragma warning disable CS8618 // 在退出构造函数时，不可为 null 的字段必须包含非 null 值。请考虑声明为可以为 null。
/// <summary>
///     （简化）评论信息
/// </summary>
public sealed class CommentDetail
{
    /// <summary>
    ///     评论的唯一ID
    /// </summary>
    [JsonPropertyName("_id")]
    public string Id { get; set; }

    /// <summary>
    ///     评论的内容
    /// </summary>
    [JsonPropertyName("content")]
    public string Content { get; set; }

    /// <summary>
    ///     评论属于哪个漫画？包含那个漫画的<see cref="TextWithUniqueId.Id" />和<see cref="TextWithUniqueId.Title" />
    /// </summary>
    [JsonPropertyName("_comic")]
    public TextWithUniqueId AtComic { get; set; }
}