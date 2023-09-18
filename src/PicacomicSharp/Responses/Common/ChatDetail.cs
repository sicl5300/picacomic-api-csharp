using System.Text.Json.Serialization;

namespace PicacomicSharp.Responses.Common;

public class ChatDetail
{
    [JsonPropertyName("title")] public required string Title { get; set; }

    [JsonPropertyName("description")] public required string Description { get; set; }

    [JsonPropertyName("url")] public required string Url { get; set; }

    [JsonPropertyName("avatar")] public required string Avatar { get; set; }
}