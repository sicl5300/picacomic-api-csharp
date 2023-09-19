using System.Text.Json.Serialization;

namespace PicacomicSharp.Responses.Common;

public sealed class CategoryDetail
{
    /// <summary>
    ///     分类名称，可以用于搜索
    /// </summary>
    [JsonPropertyName("title")]
    public required string Title { get; set; }

    /// <summary>
    ///     分类封面
    /// </summary>
    [JsonPropertyName("thumb")] public required ResourceUrl Cover { get; set; }

    /// <summary>
    ///     是否为网页链接
    /// </summary>
    [JsonPropertyName("isWeb")]
    public required bool IsWeb { get; set; }

    [JsonPropertyName("active")]
    public required bool IsActive { get; set; }

    /// <summary>
    ///     仅在 <see cref="IsWeb" /> = true 时存在
    /// </summary>
    [JsonPropertyName("link")]
    public required string? Link { get; set; } = default;
}