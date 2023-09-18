using PicacomicSharp.Common;

namespace PicacomicSharp.DependencyInjection;

/// <summary>
///     程序配置。目前混合了 DI 和 HttpClient 所需要的配置，还有一些其他的只读字段，供API签名（认证）使用。
///     TODO 分离 DI、HttpClient、API签名的配置。
/// </summary>
public class PicaConfiguration
{
    /// <summary>
    ///     API 根地址，需要以 / 结尾。
    /// </summary>
    public string BaseUrl = "https://picaapi.picacomic.com/";

    /// <summary>
    ///     超时时间，单位为秒。
    /// </summary>
    public int Timeout = 10;

    /// <summary>
    ///     加载漫画图片时的质量。
    /// </summary>
    public ImageQuality ImageQuality = ImageQuality.Original;
    
    /// <summary>
    ///     加载漫画缩略图、头像略缩图、分类略缩图等内容时的质量。
    /// </summary>
    public ImageQuality ThumbnailQuality = ImageQuality.Medium;

    #region API Authorization

    internal readonly string _host = "picaapi.picacomic.com";
    internal readonly string _channel = "1";
    internal readonly string _version = "2.2.1.2.3.4";
    internal readonly string _buildVersion = "45";
    internal readonly string _platform = "android";
    internal readonly string _userAgent = "okhttp/3.8.1";
    internal readonly string _appUuid = "defaultUuid";
    internal readonly string _accept = "application/vnd.picacomic.com.v1+json";

    internal readonly string _apiKey = "C69BAF41DA5ABD1FFEDC6D2FEA56B";
    internal readonly string _nonce = Guid.NewGuid().ToString().Replace("-", string.Empty);
    public string AuthorizationToken { get; internal set; } = string.Empty;

    public Dictionary<string, string> DefaultHeaders { get; }

    #endregion

    public PicaConfiguration()
    {
        DefaultHeaders = new Dictionary<string, string>
        {
            ["api-key"] = _apiKey,
            ["accept"] = _accept,
            ["app-channel"] = _channel,
            ["app-version"] = _version,
            ["app-build-version"] = _buildVersion,
            ["nonce"] = _nonce,
            ["app-platform"] = _platform,
            ["app-uuid"] = _appUuid,
            ["user-Agent"] = _userAgent,
            ["Host"] = _host,
        };
    }
}