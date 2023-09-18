using System.Text.Json.Serialization;
using PicacomicSharp.Common;

namespace PicacomicSharp.Responses;

public class AddFavouriteResponse : IResponseData
{
    /// <summary>
    ///     获取 <see cref="ActionEnum" />，不要直接使用此属性。
    /// </summary>
    [JsonPropertyName("action")]
    public required string Action { get; set; }

    /// <summary>
    ///     获取结果枚举
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    [JsonIgnore]
    public AddFavouriteResult ActionEnum => Action switch
    {
        "favourite" => AddFavouriteResult.Added,
        "un_favourite" => AddFavouriteResult.Removed,
        _ => throw new ArgumentOutOfRangeException(nameof(Action),
            $"favourite | un_favourite expected, {Action} given.")
    };
}