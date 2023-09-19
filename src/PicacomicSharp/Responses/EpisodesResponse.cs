using System.Text.Json.Serialization;

namespace PicacomicSharp.Responses;

internal sealed class EpisodesResponse : IResponseData, IPagedResponse<EpisodeInfo>
{
    [JsonPropertyName("eps")] public required PicaPage<EpisodeInfo> Data { get; set; }
}