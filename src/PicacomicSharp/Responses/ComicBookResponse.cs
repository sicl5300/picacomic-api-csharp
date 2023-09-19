using System.Text.Json.Serialization;

namespace PicacomicSharp.Responses;

internal sealed class ComicBookResponse : IResponseData
{
    [JsonPropertyName("comic")] public required FullComicDetail ComicDetail { get; set; }
}