using System.Text.Json.Serialization;

// ReSharper disable ClassNeverInstantiated.Global
namespace PicacomicSharp.Responses.Common;
#pragma warning disable CS8618 // 在退出构造函数时，不可为 null 的字段必须包含非 null 值。请考虑声明为可以为 null。
/// <summary>
///     文字+唯一ID
/// </summary>
public sealed class TextWithUniqueId
{
    [JsonPropertyName("_id")] public string Id { get; set; }

    [JsonPropertyName("title")] public string Title { get; set; }
}