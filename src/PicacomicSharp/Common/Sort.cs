namespace PicacomicSharp.Common;

public enum Sort
{
    /// <summary>
    ///     默认排序
    /// </summary>
    Default,

    /// <summary>
    ///     日期降序（Date descending），从新到旧
    /// </summary>
    DateDesc,

    /// <summary>
    ///     日期升序（Date ascending），从旧到新
    /// </summary>
    DateAsc,

    /// <summary>
    ///     喜欢数降序（Love descending），从多到少
    /// </summary>
    LikeDesc,

    /// <summary>
    ///     访问/绅士指数降序（Visits descending），从多到少
    /// </summary>
    ViewDesc
}

public static class SortExtensions
{
    public static string ToApiString(this Sort sort)
    {
        return sort switch
        {
            Sort.Default => "ua",
            Sort.DateDesc => "dd",
            Sort.DateAsc => "da",
            Sort.LikeDesc => "ld",
            Sort.ViewDesc => "vd",
            _ => throw new ArgumentOutOfRangeException(nameof(sort), sort, null)
        };
    }
}