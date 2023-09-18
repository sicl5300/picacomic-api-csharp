using System.Text.Json.Serialization;

namespace PicacomicSharp.Responses;

public class PunchInResult : IResponseData
{
    /// <summary>
    ///     第一次签到: <code>"res": {"status": "ok", "punchInLastDay": "2021-08-01"}</code>
    ///     多次签到: <code>"res": {"status": "fail"}</code>
    ///     签到失败: res is null.
    /// </summary>
    [JsonPropertyName("res")]
    public required __Res? Result { get; set; } = default;

    [JsonIgnore] public bool IsSuccess => Result is not null;
    [JsonIgnore] public bool IsDuplicate => Result is not null && Result?.Status == "fail";

    /// <summary>
    ///     Result of punch-in.data
    /// </summary>
    public record __Res
    {
        /// <summary>
        ///     第一次 = "ok", 多次 = "fail".
        /// </summary>
        [JsonPropertyName("status")]
        public required string? Status { get; set; } = default;

        /// <summary>
        ///     今天签到过了就是今天的日期, 多次签到时为null。
        /// </summary>
        [JsonPropertyName("punchInLastDay")]
        public required DateTime? LastPunchDate { get; set; } = default;
    }
}