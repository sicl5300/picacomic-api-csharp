using System.Text.Json.Serialization;

namespace PicacomicSharp.Responses;

public class InitResult : IResponseData
{
    [JsonPropertyName("isPunched")] public bool IsPunched { get; set; }

    /// <summary>
    ///     应用更新信息
    /// </summary>
    [JsonPropertyName("latestApplication")]
    public LatestApplication LatestApplication { get; set; }

    /// <summary>
    ///     Base image server.
    /// </summary>
    [JsonPropertyName("imageServer")]
    public string ImageServer { get; set; }

    [JsonPropertyName("apiLevel")] public int ApiLevel { get; set; }

    [JsonPropertyName("minApiLevel")] public int MinApiLevel { get; set; }

    /// <summary>
    ///     基础分类
    /// </summary>
    [JsonPropertyName("categories")]
    public List<TextWithUniqueId> Categories { get; set; }

    /// <summary>
    ///     null
    /// </summary>
    [JsonPropertyName("notification")]
    public string? Notification { get; set; }

    [JsonPropertyName("isIdUpdated")] public bool IsIdUpdated { get; set; }
}

public class LatestApplication
{
    /// <summary>
    ///     应用id，无用
    /// </summary>
    [JsonPropertyName("_id")]
    public string Id { get; set; }

    /// <summary>
    ///     更新内容
    /// </summary>
    [JsonPropertyName("updateContent")]
    public required string UpdateContent { get; set; }

    [JsonPropertyName("downloadUrl")] public required string DownloadUrl { get; set; }

    [JsonPropertyName("version")] public required string Version { get; set; }

    [JsonPropertyName("updated_at")] public required DateTime UpdatedAt { get; set; }

    [JsonPropertyName("created_at")] public required DateTime CreatedAt { get; set; }

    /// <summary>
    ///     更新Apk下载地址
    /// </summary>
    [JsonPropertyName("apk")]
    public required ResourceUrl Apk { get; set; }
}