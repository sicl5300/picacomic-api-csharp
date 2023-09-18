using System.Text.Json.Serialization;

namespace PicacomicSharp.Responses;

public class PageOfComicsResponse : IResponseData, IPagedResponse<ComicDetail>
{
    [JsonPropertyName("comics")] public PicaPage<ComicDetail> Data { get; set; }
}