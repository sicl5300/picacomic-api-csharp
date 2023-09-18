using System.Security.Cryptography;
using System.Text;
using PicacomicSharp.Common;
using PicacomicSharp.DependencyInjection;

namespace PicacomicSharp;

/// <summary>
///     本类继承自 <see cref="HttpClientHandler" />，并重写了 <see cref="HttpClientHandler.SendAsync" /> 方法，以便在发送请求前进行签名。
/// </summary>
internal class PicaHttpClientHandler : HttpClientHandler
{
    private readonly PicaConfiguration _configuration;

    public PicaHttpClientHandler(PicaConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage message,
        CancellationToken cancellationToken)
    {
        // Add headers customized
        message.Headers.Add("image-quality", _configuration.ImageQuality.ToApiString());

        var ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
        var time = Convert.ToInt64(ts.TotalSeconds).ToString();
        message.Headers.Add("time", time);

        message.Headers.Add("signature", GetSignature(message, time));

        if (!string.IsNullOrEmpty(_configuration.AuthorizationToken))
        {
            message.Headers.Add("authorization", _configuration.AuthorizationToken);
        }

        return base.SendAsync(message, cancellationToken);
    }

    private string GetSignature(HttpRequestMessage message, string time)
    {
        var url = message.RequestUri!.PathAndQuery[1..]; // Remove the leading slash

        var sigData = $"{url}{time}{_configuration._nonce}{message.Method}{_configuration._apiKey}";
        var sigKey = "~d}$Q7$eIni=V)9\\RK/P.RM4;9[7|@/CA}b~OW!3?EV`:<>M7pddUBL5n|0/*Cn";

        return Hmac256(sigData, sigKey);

        static string Hmac256(string data, string secret)
        {
            var encoding = Encoding.UTF8;
            var keyByte = encoding.GetBytes(secret);
            var dataBytes = encoding.GetBytes(data.ToLower());
            using (var hmacsha256 = new HMACSHA256(keyByte))
            {
                var hashmessage = hmacsha256.ComputeHash(dataBytes);
                var sb = new StringBuilder();
                return Convert.ToHexString(hashmessage).ToLower();
            }
        }
    }
}