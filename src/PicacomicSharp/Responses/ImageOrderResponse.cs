using System.Text.Json.Serialization;

namespace PicacomicSharp.Responses;

public class ImageOrderResponse : IResponseData, IPagedResponse<ImageUrl>
{
    [JsonPropertyName("pages")] public required PicaPage<ImageUrl> Data { get; set; }
}

public class ImageUrl
{
    [JsonPropertyName("_id")] public required string _Id { get; set; }
    
    [JsonPropertyName("media")]
    public required ResourceUrl Media { get; set; }

    [JsonPropertyName("id")] public required string Id { get; set; }
}