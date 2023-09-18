using System.Text.Json.Serialization;

namespace PicacomicSharp.Responses;

internal class LeaderboardResponse : IResponseData
{
    [JsonPropertyName("comics")] public List<ComicDetail> Comics { get; set; } = new();
}