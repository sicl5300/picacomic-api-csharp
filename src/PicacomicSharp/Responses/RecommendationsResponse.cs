using System.Text.Json.Serialization;

namespace PicacomicSharp.Responses;

public class RecommendationsResponse : IResponseData
{
    [JsonPropertyName("comics")] public required List<ComicDetail> Comics { get; set; }
}