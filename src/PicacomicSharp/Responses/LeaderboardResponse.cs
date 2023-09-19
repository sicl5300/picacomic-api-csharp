using System.Text.Json.Serialization;

namespace PicacomicSharp.Responses;

internal sealed class LeaderboardResponse : IResponseData
{
    [JsonPropertyName("comics")] public List<ComicDetail> Comics { get; set; } = new();
}