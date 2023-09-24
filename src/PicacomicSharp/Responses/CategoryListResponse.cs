using System.Text.Json.Serialization;

namespace PicacomicSharp.Responses;

internal sealed class CategoryListResponse : IResponseData
{
    [JsonPropertyName("categories")] public required List<CategoryDetail> Categories { get; set; }
}