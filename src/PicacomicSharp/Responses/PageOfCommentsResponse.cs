using System.Text.Json.Serialization;

namespace PicacomicSharp.Responses;

internal sealed class PageOfCommentsResponse : IResponseData, IPagedResponse<CommentDetail>
{
    [JsonPropertyName("comments")] public required PicaPage<CommentDetail> Data { get; set; }
}