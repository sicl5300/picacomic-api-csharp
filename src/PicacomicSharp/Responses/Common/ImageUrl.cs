using System.Text.Json.Serialization;

namespace PicacomicSharp.Responses.Common;

public sealed class ImageUrl
{
    [JsonPropertyName("_id")] public required string _Id { get; set; }

    [JsonPropertyName("media")]
    public required ResourceUrl Image { get; set; }

    [JsonPropertyName("id")] public required string Id { get; set; }
}