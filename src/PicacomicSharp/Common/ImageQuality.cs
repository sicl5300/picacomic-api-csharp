namespace PicacomicSharp.Common;

/// <summary>
///     图像的质量。
/// </summary>
public enum ImageQuality
{
    Original,
    Low,
    Medium,
    High
}

public static class ImageQualityExtensions
{
    /// <summary>
    ///     将 <see cref="ImageQuality" /> 转换为 API 所需的字符串。
    /// </summary>
    /// <param name="imageQuality"></param>
    /// <returns>小写名称</returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static string ToApiString(this ImageQuality imageQuality) =>
        imageQuality switch
        {
            ImageQuality.Original => "original",
            ImageQuality.Low => "low",
            ImageQuality.Medium => "medium",
            ImageQuality.High => "high",
            _ => throw new ArgumentOutOfRangeException(nameof(imageQuality), imageQuality, null)
        };
}