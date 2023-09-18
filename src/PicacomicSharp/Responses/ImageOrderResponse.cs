using System.Text.Json.Serialization;

namespace PicacomicSharp.Responses;

internal class ImageOrderResponse : IResponseData, IPagedResponse<ImageUrl>
{
    [JsonPropertyName("pages")] public required PicaPage<ImageUrl> Data { get; set; }
}