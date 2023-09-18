using System.Text.Json.Serialization;

namespace PicacomicSharp.Responses.Common;

/// <summary>
///     标准的分页响应数据，代表着下面例子中的<c>data</c>节。包含内容列表、当前页码、总页数、总元素数（所有页面中的元素之和）等等。
/// </summary>
/// <example>针对如下API返回内容<code>{ "code": 200, "data":{ "comics": { "page":1 ... ,"docs": [具体元素列表] }} } </code></example>
/// <typeparam name="TDoc"><c>docs[]</c>中的元素类型</typeparam>
public class PicaPage<TDoc>
{
    /// <summary>
    ///    "Detail of contents"，具体内容列表。
    /// </summary>
    [JsonPropertyName("docs")]
    public List<TDoc> Data { get; set; }

    /// <summary>
    ///     总<see cref="TDoc" />元素数，所有页面中的元素之和。
    /// </summary>
    [JsonPropertyName("total")]
    public int Total { get; set; }

    /// <summary>
    ///     每个页面的最大元素数，一般为20。
    /// </summary>
    [JsonPropertyName("limit")]
    public int Limit { get; set; }

    /// <summary>
    ///     当前页码。
    /// </summary>
    [JsonPropertyName("page")]
    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
    public int Page { get; set; }

    /// <summary>
    ///     总页数。
    /// </summary>
    [JsonPropertyName("pages")]
    public int Pages { get; set; }

    /// <summary>
    ///     获取下一页页码，如果没有下一页，则将<see cref="nextPage" />设置为-1。
    /// </summary>
    /// <param name="nextPage">下一页的页码</param>
    /// <returns>如果没有下一页，返回<c>false</c></returns>
    public bool TryGetNextPage(out int nextPage)
    {
        if (Page == Pages)
        {
            nextPage = -1;
            return false;
        }

        nextPage = Page + 1;
        return true;
    }

    /// <summary>
    ///     获取一个包含所有页码的<see cref="IEnumerable{T}" />，最大页码由<paramref name="iterateToPage" />指定。
    /// </summary>
    /// <param name="iterateToPage">
    ///     页面总数为10, iterateToPage=4, 返回 [1,2,3,4].<br />
    ///     页面总数为3, iterateToPage=4, 返回 [1,2,3].<br />
    /// </param>
    /// <returns></returns>
    public IEnumerable<int> GetPagesList(int iterateToPage = 5)
    {
        if (iterateToPage > Pages || iterateToPage == -1) iterateToPage = Pages;

        return Enumerable.Range(1, iterateToPage);
    }
}