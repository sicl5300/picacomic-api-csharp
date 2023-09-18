namespace PicacomicSharp.Common;

/// <summary>
///     排行榜时间范围
/// </summary>
public enum RankDateRange
{
    /// <summary>
    ///     过去24小时
    /// </summary>
    Hour24,

    /// <summary>
    ///     过去一周
    /// </summary>
    Day7,

    /// <summary>
    ///     过去一月
    /// </summary>
    Day30
}

public static class RankDateRangeExtensions
{
    public static string ToApiString(this RankDateRange range)
    {
        return range switch
        {
            RankDateRange.Hour24 => "H24",
            RankDateRange.Day7 => "D7",
            RankDateRange.Day30 => "D30",
            _ => throw new ArgumentOutOfRangeException(nameof(range), range, null)
        };
    }
}