namespace PicacomicSharp.Common;

public enum ImageQuality
{
    Original,
    Low,
    Medium,
    High
}

public static class ImageQualityExtensions
{
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