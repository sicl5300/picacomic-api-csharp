using System.Text.Json.Serialization;

namespace PicacomicSharp.Responses.Common;

public class EpisodeInfo
{
    /// <summary>
    ///     从属于哪本漫画，漫画的唯一id，不要使用
    /// </summary>
    [JsonPropertyName("_id")]
    public required string _Id { get; set; }

    [JsonPropertyName("title")] public required string Title { get; set; }

    /// <summary>
    ///     从1开始，从旧到新 //todo: check
    ///     <br />
    ///     用于获取漫画的图片
    /// </summary>
    [JsonPropertyName("order")]
    public required int Order { get; set; }

    // todo: we can sort by this to customize eps order and iterate eps.
    [JsonPropertyName("updated_at")] public required DateTime UpdatedAt { get; set; }

    /// <summary>
    ///     从属于哪本漫画，漫画的唯一id，不要使用
    /// </summary>
    [JsonPropertyName("id")]
    public required string Id { get; set; }
}