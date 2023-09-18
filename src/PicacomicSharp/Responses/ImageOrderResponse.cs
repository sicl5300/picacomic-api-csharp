using System.Text.Json.Serialization;

namespace PicacomicSharp.Responses;

public class ImageOrderResponse : IResponseData, IPagedResponse<ImageUrl>
{
    [JsonPropertyName("pages")] public required PicaPage<ImageUrl> Data { get; set; }
}