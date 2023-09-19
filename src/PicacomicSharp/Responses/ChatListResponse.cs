using System.Text.Json.Serialization;

// ReSharper disable ClassNeverInstantiated.Global

namespace PicacomicSharp.Responses;

internal sealed class ChatListResponse : IResponseData
{
    [JsonPropertyName("chatList")] public required List<ChatDetail> ChatDetail { get; set; }
}