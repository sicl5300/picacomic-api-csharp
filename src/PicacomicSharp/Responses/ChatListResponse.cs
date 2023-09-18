using System.Text.Json.Serialization;

// ReSharper disable ClassNeverInstantiated.Global

namespace PicacomicSharp.Responses;

public class ChatListResponse : IResponseData
{
    [JsonPropertyName("chatList")] public required List<ChatDetail> ChatDetail { get; set; }
}

public class ChatDetail
{
    [JsonPropertyName("title")] public required string Title { get; set; }

    [JsonPropertyName("description")] public required string Description { get; set; }

    [JsonPropertyName("url")] public required string Url { get; set; }

    [JsonPropertyName("avatar")] public required string Avatar { get; set; }
}