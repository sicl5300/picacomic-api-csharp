using System.Text.Json.Serialization;

namespace PicacomicSharp.Responses;

public class KeywordsResponse : IResponseData
{
    [JsonPropertyName("keywords")] public required List<string> Keywords { get; set; }
}