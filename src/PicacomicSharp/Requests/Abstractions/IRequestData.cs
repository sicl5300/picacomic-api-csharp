namespace PicacomicSharp.Requests.Abstractions;

internal interface IRequestData
{
    /// <summary>
    ///     相对URL路径，不包含域名。
    ///     需要被重写，可以自己实现 String Interpolation 来 URL 传参，传参时请注意使用 UrlEncode。
    ///     如果请求的类型是 POST，则必须注解为 <see cref="System.Text.Json.Serialization.JsonIgnoreAttribute"/>！
    /// </summary>
    internal abstract string Url { get; }
}