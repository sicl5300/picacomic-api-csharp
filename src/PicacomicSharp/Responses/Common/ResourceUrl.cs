using System.Text.Json.Serialization;

namespace PicacomicSharp.Responses.Common;
#pragma warning disable CS8618 // 在退出构造函数时，不可为 null 的字段必须包含非 null 值。请考虑声明为可以为 null。
/// <summary>
///     资源地址
/// </summary>
public sealed class ResourceUrl
{
    /// <summary>
    ///     源文件名称，没有用
    /// </summary>
    [JsonPropertyName("originalName")]
    public string OriginalName { get; set; }

    [JsonPropertyName("path")]
    public string Path { get; set; }

    /// <summary>
    ///     文件服务器根地址
    ///     <example>https://assets.helloworld.com</example>
    ///     <example>https://assets.helloworld.com/</example>
    /// </summary>
    [JsonPropertyName("fileServer")]
    public string FileServer { get; set; }
    
    /// <summary>
    ///     转为完整的资源地址 URL。
    /// </summary>
    /// <returns>https://assets.helloworld.com/static/res/res.jpg</returns>
    public override string ToString()
    {
        var fileServer = FileServer.EndsWith('/') ? FileServer : FileServer + "/";  // https://assets.helloworld.com/
        fileServer += "static/";  // https://assets.helloworld.com/static/
        
        if (Path.StartsWith('/')) return fileServer +  Path[1..]; // avoid https://assets.helloworld.com/static//test.jpg
        return fileServer + Path; // https://assets.helloworld.com/static/path.jpg
    }
}