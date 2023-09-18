using System.Text.Json.Serialization;

namespace PicacomicSharp.Responses.Common;

/// <summary>
///     （简化版）漫画详情，不包含上传者信息。
///     注意：本类包含很多 Nullable 类型，请注意判空。
/// </summary>
public class ComicDetail
{
    /// <summary>
    ///     漫画唯一ID
    /// </summary>
    [JsonPropertyName("_id")]
    public string Id { get; set; }

    /// <summary>
    ///     漫画标题
    /// </summary>
    [JsonPropertyName("title")]
    public string Title { get; set; }

    /// <summary>
    ///     漫画作者
    /// </summary>
    [JsonPropertyName("author")]
    public string Author { get; set; }

    [JsonPropertyName("totalViews")]
    public int? TotalViews { get; set; }

    [JsonPropertyName("totalLikes")]
    public int? TotalLikes { get; set; }

    /// <summary>
    ///     漫画总页数，每个章节页数的总和
    /// </summary>
    [JsonPropertyName("pagesCount")]
    public int PagesCount { get; set; }

    /// <summary>
    ///     漫画章节个数
    /// </summary>
    [JsonPropertyName("epsCount")]
    public int EpsCount { get; set; }

    /// <summary>
    ///     漫画是否完结
    /// </summary>
    [JsonPropertyName("finished")]
    public bool Finished { get; set; }

    /// <summary>
    ///     列表，漫画分类名称
    /// </summary>
    [JsonPropertyName("categories")]
    public List<string> Categories { get; set; }

    /// <summary>
    ///     封面
    /// </summary>
    [JsonPropertyName("thumb")]
    public ResourceUrl Cover { get; set; }

    /// <summary>
    ///     排行榜指数（仅在Leaderboard中可用）
    /// </summary>
    [JsonPropertyName("leaderboardCount")]
    public int? LeaderboardCount { get; set; }
    
    /// <summary>
    ///     漫画点赞数
    /// </summary>
    [JsonPropertyName("likesCount")]
    public int? LikesCount { get; set; }
    
    /// <summary>
    ///     漫画浏览数
    /// </summary>
    [JsonPropertyName("viewsCount")]
    public int? ViewssCount { get; set; }
}