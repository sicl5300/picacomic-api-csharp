using System.Text.Json.Serialization;
using PicacomicSharp.Common;

namespace PicacomicSharp.Requests;

/// <summary>
///     POST数据包，用于高级搜索（分页，排序，关键词）。<br />
///     你需要在构造时手动传入页码，而不是使用 required initializer。
/// </summary>
public sealed class RequestAdvancedSearch : IPost
{
    /// <summary>
    ///     Manually pass page number in it.
    /// </summary>
    /// <param name="page">Page number</param>
    public RequestAdvancedSearch(int page = 1)
    {
        Page = page;
        SortString = Sort.ToApiString();
    }

    // Todo: Due to the API design, we have to pass page as parameter in url,
    // however [JsonIgnore] is not working on required properties,
    // so I removed "required" qualifier from it and set it to 1 by default.
    // User should pass page in Constructor.
    // https://github.com/dotnet/runtime/issues/82879
    [JsonIgnore] public int Page { get; set; }
    [JsonIgnore] public Sort Sort { get; set; } = Sort.Default;

    [JsonPropertyName("keyword")] public required string Keyword { get; set; }
    [JsonPropertyName("sort")] public string SortString { get; set; }
    [JsonIgnore] string IRequestData.Url => $"comics/advanced-search?page={Page}";
}