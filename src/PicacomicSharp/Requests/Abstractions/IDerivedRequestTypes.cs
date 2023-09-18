using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PicacomicSharp.Requests.Abstractions;

public interface IPost : IRequestData
{
    [JsonIgnore]
    public StringContent DataSerialized =>
        new StringContent(JsonSerializer.Serialize(this), Encoding.UTF8, "application/json");
}

public interface IGet : IRequestData
{
}