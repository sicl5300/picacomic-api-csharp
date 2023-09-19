using System.Text.Json.Serialization;

namespace PicacomicSharp.Responses;

internal sealed class ProfileResponse : IResponseData
{
    [JsonPropertyName("user")] public required User Data { get; set; }
}