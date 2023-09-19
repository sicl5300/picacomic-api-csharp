using System.Web;
using PicacomicSharp.Common;

namespace PicacomicSharp.Requests;

/// <summary>
///     按分类名字搜索（分类，排序，分页）
/// </summary>
public class RequestSearchByCategory : IGet
{
    /// <summary>
    ///     分类的名字（标题），不是分类的id。
    /// </summary>
    public required string CategoryName { get; init; }

    public required Sort Sort { get; init; } = Sort.Default;
    public required int Page { get; set; }

    string IRequestData.Url => $"comics?page={Page}&c={HttpUtility.UrlEncode(CategoryName)}&s={Sort.ToApiString()}";
}