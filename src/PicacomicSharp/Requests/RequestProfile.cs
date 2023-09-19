namespace PicacomicSharp.Requests;

/// <summary>
///     用户信息请求
/// </summary>
public sealed class RequestProfile : IGet
{
    /// <summary>
    ///     用户的 UUID，不是用户名。
    /// </summary>
    public required string? Id { get; init; } = default;

    string IRequestData.Url => Id switch
    {
        null or "" => "users/profile",
        _ => $"users/{Id}/profile"
    };
}