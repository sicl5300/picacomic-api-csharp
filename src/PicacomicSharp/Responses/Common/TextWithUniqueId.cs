using System.Text.Json.Serialization;

// ReSharper disable ClassNeverInstantiated.Global
namespace PicacomicSharp.Responses.Common;

/// <summary>
///     文字+唯一ID
/// </summary>
public class TextWithUniqueId
{
    [JsonPropertyName("_id")] public string Id { get; set; }

    [JsonPropertyName("title")] public string Title { get; set; }
}