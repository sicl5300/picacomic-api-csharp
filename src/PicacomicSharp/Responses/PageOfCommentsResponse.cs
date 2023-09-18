using System.Text.Json.Serialization;

namespace PicacomicSharp.Responses;

internal class PageOfCommentsResponse : IResponseData, IPagedResponse<CommentDetail>
{
    [JsonPropertyName("comments")] public required PicaPage<CommentDetail> Data { get; set; }
}