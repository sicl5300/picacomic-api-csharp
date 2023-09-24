using System.Text.Json.Serialization;
#pragma warning disable CS8618 // 在退出构造函数时，不可为 null 的字段必须包含非 null 值。请考虑声明为可以为 null。

namespace PicacomicSharp.Responses;

internal sealed class PageOfComicsResponse : IResponseData, IPagedResponse<ComicDetail>
{
    [JsonPropertyName("comics")] public PicaPage<ComicDetail> Data { get; set; }
}