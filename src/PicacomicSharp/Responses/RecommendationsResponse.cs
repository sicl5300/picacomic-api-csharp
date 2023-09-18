using System.Text.Json.Serialization;

namespace PicacomicSharp.Responses;

internal class RecommendationsResponse : IResponseData
{
    [JsonPropertyName("comics")] public required List<ComicDetail> Comics { get; set; }
}