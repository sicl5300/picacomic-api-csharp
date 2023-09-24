using System.Text.Json.Serialization;

namespace PicacomicSharp.Responses.Common;
#pragma warning disable CS8618 // 在退出构造函数时，不可为 null 的字段必须包含非 null 值。请考虑声明为可以为 null。
/// <summary>
///     完整的漫画详情，包含上传者信息，漫画信息，漫画状态，漫画统计信息...
/// </summary>
public sealed class FullComicDetail
{
    /// <summary>
    ///     漫画唯一ID
    /// </summary>
    [JsonPropertyName("_id")]
    public string Id { get; set; }

    /// <summary>
    ///     上传者的信息，不是漫画作者。
    /// </summary>
    [JsonPropertyName("_creator")]
    public User Creator { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    /// <summary>
    ///     漫画封面
    /// </summary>
    [JsonPropertyName("thumb")]
    public ResourceUrl Cover { get; set; }

    /// <summary>
    ///     漫画的作者（画家）
    /// </summary>
    [JsonPropertyName("author")]
    public string Author { get; set; }

    /// <summary>
    ///     汉化组信息
    /// </summary>
    [JsonPropertyName("chineseTeam")]
    public string? ChineseTeam { get; set; }

    /// <summary>
    ///     列表，漫画分类名称
    /// </summary>
    [JsonPropertyName("categories")]
    public IList<string> Categories { get; set; }

    /// <summary>
    ///     列表，漫画标签名称
    /// </summary>
    [JsonPropertyName("tags")]
    public IList<string> Tags { get; set; }

    /// <summary>
    ///     漫画总页数，每个章节页数的总和
    /// </summary>
    [JsonPropertyName("pagesCount")]
    public int PagesCount { get; set; }

    /// <summary>
    ///     漫画章节个数
    /// </summary>
    [JsonPropertyName("epsCount")] public int EpsCount { get; set; }

    /// <summary>
    ///     漫画是否“已完结”
    /// </summary>
    [JsonPropertyName("finished")]
    public bool Finished { get; set; }

    [JsonPropertyName("updated_at")]
    public DateTime UpdatedAt { get; set; }

    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }

    /// <summary>
    ///     漫画是否允许下载，即使不允许，我们通过API也可以下载。
    /// </summary>
    [JsonPropertyName("allowDownload")]
    public bool AllowDownload { get; set; }

    /// <summary>
    ///     漫画是否允许评论
    /// </summary>
    [JsonPropertyName("allowComment")]
    public bool AllowComment { get; set; }

    [JsonPropertyName("totalLikes")]
    public int TotalLikes { get; set; }

    [JsonPropertyName("totalViews")]
    public int TotalViews { get; set; }

    [JsonPropertyName("totalComments")]
    public int TotalComments { get; set; }

    [JsonPropertyName("viewsCount")] 
    public int ViewsCount { get; set; }

    [JsonPropertyName("likesCount")]
    public int LikesCount { get; set; }

    [JsonPropertyName("commentsCount")]
    public int CommentsCount { get; set; }

    /// <summary>
    ///     该漫画是否已经被收藏
    /// </summary>
    [JsonPropertyName("isFavourite")]
    public bool IsFavourite { get; set; }

    /// <summary>
    ///     该漫画是否已经被点赞
    /// </summary>
    [JsonPropertyName("isLiked")]
    public bool IsLiked { get; set; }
}