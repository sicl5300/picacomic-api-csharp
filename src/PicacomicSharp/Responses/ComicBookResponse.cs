using System.Text.Json.Serialization;

namespace PicacomicSharp.Responses;

public class ComicBookResponse : IResponseData
{
    [JsonPropertyName("comic")] public required FullComicDetail ComicDetail { get; set; }
}