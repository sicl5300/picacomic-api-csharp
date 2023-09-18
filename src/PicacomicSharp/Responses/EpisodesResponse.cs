using System.Text.Json.Serialization;

namespace PicacomicSharp.Responses;

internal class EpisodesResponse : IResponseData, IPagedResponse<EpisodeInfo>
{
    [JsonPropertyName("eps")] public required PicaPage<EpisodeInfo> Data { get; set; }
}