using PicacomicSharp.Common;

namespace PicacomicSharp.DependencyInjection;

public class PicaConfiguration
{
    public string BaseUrl = "https://picaapi.picacomic.com/";
    public int Timeout = 10;
    public ImageQuality ImageQuality = ImageQuality.Original;
    public ImageQuality ThumbnailQuality = ImageQuality.Medium;

    #region API Authorization

    public readonly string _host = "picaapi.picacomic.com";
    public readonly string _channel = "1";
    public readonly string _version = "2.2.1.2.3.4";
    public readonly string _buildVersion = "45";
    public readonly string _platform = "android";
    public readonly string _userAgent = "okhttp/3.8.1";
    public readonly string _appUuid = "defaultUuid";
    public readonly string _accept = "application/vnd.picacomic.com.v1+json";

    public readonly string _apiKey = "C69BAF41DA5ABD1FFEDC6D2FEA56B";
    public readonly string _nonce = Guid.NewGuid().ToString().Replace("-", string.Empty);
    public string AuthorizationToken { get; set; } = string.Empty;


    #endregion

    public PicaConfiguration()
    {
    }
}