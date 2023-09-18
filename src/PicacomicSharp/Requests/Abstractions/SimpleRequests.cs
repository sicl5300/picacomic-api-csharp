namespace PicacomicSharp.Requests.Abstractions;

/// <summary>
///     这个 record 的 <see cref="Url" /> 属性没有注解为 <see cref="System.Text.Json.Serialization.JsonIgnoreAttribute"/>，
///     且没有任何参数，所以在在发送请求时，要跳过序列化，使用 Type pattern matching 判断一下！
/// </summary>
/// <param name="Url"></param>
public record SimplePostRequest(string Url) : IPost;

public record SimpleGetRequest(string Url) : IGet;