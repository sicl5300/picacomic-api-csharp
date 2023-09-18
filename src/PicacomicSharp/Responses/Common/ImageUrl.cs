using System.Text.Json.Serialization;

namespace PicacomicSharp.Responses.Common;

public class ImageUrl
{
    [JsonPropertyName("_id")] public required string _Id { get; set; }

    [JsonPropertyName("media")]
    public required ResourceUrl Media { get; set; }

    [JsonPropertyName("id")] public required string Id { get; set; }
}