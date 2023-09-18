using System.Text.Json.Serialization;

namespace PicacomicSharp.Responses.Common;

/// <summary>
///     资源地址
/// </summary>
public class ResourceUrl
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
        var fileserver = FileServer.EndsWith("/") ? FileServer : FileServer + "/";  // https://assets.helloworld.com/
        fileserver += "static/";  // https://assets.helloworld.com/static/
        
        if (Path.StartsWith("/")) return fileserver +  Path[1..]; // avoid https://assets.helloworld.com/static//test.jpg
        return fileserver + Path; // https://assets.helloworld.com/static/path.jpg
    }
}