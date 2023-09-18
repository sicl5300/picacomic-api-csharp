using System.Text.Json.Serialization;

namespace PicacomicSharp.Responses;

public class ProfileResponse : IResponseData
{
    [JsonPropertyName("user")] public required User Data { get; set; }
}