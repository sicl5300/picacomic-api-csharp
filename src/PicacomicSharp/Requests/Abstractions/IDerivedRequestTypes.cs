using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PicacomicSharp.Requests.Abstractions;

public interface IPost : IRequestData
{
    /// <summary>
    ///     序列化好的POST请求数据。
    /// </summary>
    [JsonIgnore] public StringContent DataSerialized =>
        new StringContent(JsonSerializer.Serialize(this), Encoding.UTF8, "application/json");
}

public interface IGet : IRequestData
{
}