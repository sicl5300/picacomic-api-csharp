namespace PicacomicSharp.Responses.Abstractions;

/// <summary>
///     内部包装了 <see cref="PicaPage{TDoc}" /> 的响应数据的接口，代表着下面例子中的<c>comics</c>节。
/// </summary>
/// <example>针对如下API返回内容<code>{ "code": 200, "data":{ "comics": { "page":1 ... ,"docs": [具体元素列表] }} } </code></example>
/// <typeparam name="TDoc"><c>docs[]</c>中的元素类型</typeparam>
public interface IPagedResponse<TDoc>
{
    /// <summary>
    ///     <see cref="PicaPage{TDoc}" />, 必须注解上
    ///     <see cref="System.Text.Json.Serialization.JsonPropertyNameAttribute" />，
    ///     因为在返回的JSON中，这个属性名不是"data"，而是"comics"、"comments"等等，取决于API。
    /// </summary>
    public PicaPage<TDoc> Data { get; set; }
}