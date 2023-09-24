using System.Text;
using System.Text.Json;
using System.Web;
using PicacomicSharp.Common;
using PicacomicSharp.DependencyInjection;
using PicacomicSharp.Requests;
using PicacomicSharp.Responses;

namespace PicacomicSharp;

/// <summary>
///     用来发送API请求的 Client，推荐使用 DI Container 来获取实例。
/// </summary>
public class PicaClient
{
    private readonly PicaConfiguration _configuration;
    private readonly HttpClient _client;

    public PicaClient(PicaConfiguration configuration, HttpClient client)
    {
        _configuration = configuration;
        client.BaseAddress = new Uri(configuration.BaseUrl);
        client.Timeout = TimeSpan.FromSeconds(configuration.Timeout);
        foreach (var pair in configuration.DefaultHeaders)
        {
            client.DefaultRequestHeaders.Add(pair.Key, pair.Value);
        }

        _client = client;
    }

    private async Task<TResponse> SendAsync<TRequest, TResponse>(TRequest config)
        where TResponse : IResponseData
        where TRequest : IRequestData
    {
        var method = config switch
        {
            IPost => HttpMethod.Post,
            IGet => HttpMethod.Get,
            _ => throw new ArgumentOutOfRangeException(nameof(config), config, null)
        };

        var requestMessage = new HttpRequestMessage(method, config.Url);

        if (config is IPost)
        {
            var content = new StringContent(JsonSerializer.Serialize(config), Encoding.UTF8, "application/json");
            content.Headers.ContentType!.CharSet = "UTF-8";
            requestMessage.Content = content;
        }

        var response = await _client.SendAsync(requestMessage).ConfigureAwait(false);
        var message = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

        var wrapped = JsonSerializer.Deserialize<WrappedResponse<TResponse>>(message, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        if (wrapped.Data is not null) return wrapped.Data;

        if (wrapped.Error is not null) // Data is null, but Error is not null
            throw new InvalidOperationException($"Error: {wrapped.Error}, Message: {wrapped.Message ?? "None"}.");

        throw new InvalidOperationException("Invalid response.");
    }

    /// <summary>
    ///     保存授权信息（令牌）到配置中。
    /// </summary>
    /// <param name="token">令牌</param>
    public void SaveAuthorization(string token)
    {
        _configuration.AuthorizationToken = token;
    }

    /// <summary>
    ///     尝试登录，如果成功则返回令牌。
    /// </summary>
    /// <param name="payload">账号，密码</param>
    /// <param name="saveTokenAutomatically">是否自动保存令牌</param>
    /// <returns>令牌</returns>
    public async Task<string> LoginAsync(RequestLogin payload, bool saveTokenAutomatically = false)
    {
        var re = await SendAsync<RequestLogin, LoginResponse>(payload).ConfigureAwait(false);
        var token = re.Token;
        if (saveTokenAutomatically) this.SaveAuthorization(token);
        return token;
    }

    /// <summary>
    ///     注册账号。
    /// </summary>
    /// <param name="payload">账号，密码，昵称，生日，性别，密保问题等等</param>
    /// <returns>如果不报错就是成功，返回True</returns>
    public async Task<bool> RegisterAsync(RequestRegister payload)
    {
        _ = await SendAsync<RequestRegister, RegisterResponse>(payload).ConfigureAwait(false);
        return true;
    }

    /// <summary>
    ///     尝试签到，如果成功则返回签到结果。
    /// </summary>
    /// <returns>签到结果<see cref="PunchInResult"/></returns>
    public async Task<PunchInResult> PunchInAsync()
    {
        return await SendAsync<SimplePostRequest, PunchInResult>(new SimplePostRequest("users/punch-in")).ConfigureAwait(false);
    }

    /// <summary>
    ///    获取用户信息。
    /// </summary>
    /// <param name="id">用户独一Id，如果为空则获取自己的</param>
    /// <returns>用户信息<see cref="User"/></returns>
    public async Task<User> GetProfileAsync(string? id = null)
    {
        return (await SendAsync<RequestProfile, ProfileResponse>(new RequestProfile { Id = id }).ConfigureAwait(false)).Data;
    }

    /// <summary>
    ///     获取聊天室列表（聊天室就是特殊的评论区）。
    /// </summary>
    /// <returns>聊天室信息列表<see cref="ChatDetail"/></returns>
    public async Task<IList<ChatDetail>> GetChatListAsync()
    {
        return (await SendAsync<SimpleGetRequest, ChatListResponse>(new SimpleGetRequest("chat")).ConfigureAwait(false)).ChatDetail;
    }

    /// <summary>
    ///     获取应用初始化信息，包括一些配置信息。没用。
    /// </summary>
    /// <returns></returns>
    public async Task<InitResult> GetInitMessage()
    {
        return await SendAsync<SimpleGetRequest, InitResult>(new SimpleGetRequest("init?platform=android")).ConfigureAwait(false);
    }

    /// <summary>
    ///     获取分类列表。
    /// </summary>
    /// <returns>分类信息</returns>
    public async Task<IList<CategoryDetail>> GetCategoryListAsync()
    {
        return (await SendAsync<SimpleGetRequest, CategoryListResponse>(new SimpleGetRequest("categories")).ConfigureAwait(false)).Categories;
    }

    /// <summary>
    ///     按分类搜索漫画。
    /// </summary>
    /// <param name="payload">分类，排序，分页</param>
    /// <returns>Page</returns>
    public async Task<PicaPage<ComicDetail>> SearchByCategoryAsync(RequestSearchByCategory payload)
    {
        return (await SendAsync<RequestSearchByCategory, PageOfComicsResponse>(payload).ConfigureAwait(false)).Data;
    }

    /// <summary>
    ///     高级搜索（分页，排序，关键词）。不支持分类。
    /// </summary>
    /// <param name="payload"></param>
    /// <returns></returns>
    public async Task<PicaPage<ComicDetail>> SearchAdvancedAsync(RequestAdvancedSearch payload)
    {
        return (await SendAsync<RequestAdvancedSearch, PageOfComicsResponse>(payload).ConfigureAwait(false)).Data;
    }

    /// <summary>
    ///     普通关键字搜索。
    /// </summary>
    /// <param name="queryKeyword"></param>
    /// <param name="page"></param>
    /// <returns></returns>
    public async Task<PicaPage<ComicDetail>> SearchAsync(string queryKeyword, int page = 1)
    {
        return (await SendAsync<SimpleGetRequest, PageOfComicsResponse>(
                new SimpleGetRequest($"comics/search?page={page.ToString()}&q={HttpUtility.UrlEncode(queryKeyword)}")).ConfigureAwait(false))
            .Data;
    }
    
    /// <summary>
    ///     获取当前登录用户的评论，分页，从最新到最旧。
    /// </summary>
    /// <param name="page"></param>
    /// <returns></returns>
    public async Task<PicaPage<CommentDetail>> GetMyCommentsAsync(int page = 1)
    {
        return (await SendAsync<SimpleGetRequest, PageOfCommentsResponse>(
            new SimpleGetRequest($"users/my-comments?page={page.ToString()}")).ConfigureAwait(false)).Data;
    }

    /// <summary>
    ///     获取当前登录用户的收藏，分页，排序。
    /// </summary>
    /// <param name="page"></param>
    /// <param name="sort"></param>
    /// <returns></returns>
    public async Task<PicaPage<ComicDetail>> GetFavouritesAsync(int page = 1, Sort sort = Sort.Default)
    {
        return (await SendAsync<SimpleGetRequest, PageOfComicsResponse>(
            new SimpleGetRequest($"users/favourite?s={sort.ToApiString()}" +
                                 $"&page={page.ToString()}")).ConfigureAwait(false)).Data;
    }

    /// <summary>
    ///     获取排行榜，支持获取日榜，周榜，月榜。
    /// </summary>
    /// <param name="range"></param>
    /// <returns></returns>
    public async Task<IList<ComicDetail>> GetLeaderboardAsync(RankDateRange range)
    {
        return (await SendAsync<SimpleGetRequest, LeaderboardResponse>(
            new SimpleGetRequest($"comics/leaderboard?tt={range.ToApiString()}&ct=VC")).ConfigureAwait(false)).Comics;
    }

    /// <summary>
    ///     添加/移除收藏。如果已经收藏则移除，如果没有收藏则添加。
    /// </summary>
    /// <param name="comicId"></param>
    /// <returns>枚举，代表本次操作是添加还是移除了。</returns>
    public async Task<AddFavouriteResult> AddFavouriteAsync(string comicId)
    {
        var re = await SendAsync<SimplePostRequest, AddFavouriteResponse>(
            new SimplePostRequest($"comics/{comicId}/favourite")).ConfigureAwait(false);
        return re.ActionEnum;
    }

    /// <summary>
    ///     获取漫画详情。
    /// </summary>
    /// <param name="bookId"></param>
    /// <returns></returns>
    public async Task<FullComicDetail> GetComicBookByIdAsync(string bookId)
    {
        return (await SendAsync<SimpleGetRequest, ComicBookResponse>(
            new SimpleGetRequest($"comics/{bookId}")).ConfigureAwait(false)).ComicDetail;
    }

    [Obsolete("永远返回空列表，疑似弃用")]
    public async Task<IList<ComicDetail>> GetRecommendationByBookAsync(string bookId)
    {
        return (await SendAsync<SimpleGetRequest, RecommendationsResponse>(
            new SimpleGetRequest($"comics/{bookId}/recommendation")).ConfigureAwait(false)).Comics;
    }

    /// <summary>
    ///     获取漫画分集，以便于后续获取图片。
    /// </summary>
    /// <param name="bookId"></param>
    /// <param name="page"></param>
    /// <returns></returns>
    public async Task<PicaPage<EpisodeInfo>> GetComicBookEpisodesAsync(string bookId, int page = 1)
    {
        return (await SendAsync<SimpleGetRequest, EpisodesResponse>(
            new SimpleGetRequest($"comics/{bookId}/eps?page={page}")).ConfigureAwait(false)).Data;
    }

    /// <summary>
    ///     获取漫画分集的图片。
    /// </summary>
    /// <param name="bookId"></param>
    /// <param name="epsOrder"></param>
    /// <param name="page"></param>
    /// <returns></returns>
    public async Task<PicaPage<ImageUrl>> OrderBookImagesAsync(string bookId, int epsOrder, int page = 1)
    {
        return (await SendAsync<SimpleGetRequest, ImageOrderResponse>(
            // epsOrder starts from 1
            new SimpleGetRequest($"comics/{bookId}/order/{epsOrder}/pages?page={page}")).ConfigureAwait(false)).Data;
    }

    /// <summary>
    ///     获取热搜关键词。
    /// </summary>
    /// <returns></returns>
    public async Task<IList<string>> GetKeywordsAsync()
    {
        return (await SendAsync<SimpleGetRequest, KeywordsResponse>(
            new SimpleGetRequest("keywords")).ConfigureAwait(false)).Keywords;
    }

    /// <summary>
    ///     获取随机漫画。
    /// </summary>
    /// <returns></returns>
    public async Task<IList<ComicDetail>> GetRandomAsync()
    {
        return (await SendAsync<SimpleGetRequest, RecommendationsResponse>(
            new SimpleGetRequest("comics/random")).ConfigureAwait(false)).Comics;
    }

    /// <summary>
    ///     获取“本子妹推荐”
    /// </summary>
    /// <returns></returns>
    public async Task<IList<ComicDetail>> GetCollectionsAsync()
    {
        return (await SendAsync<SimpleGetRequest, CollectionResponse>(
            new SimpleGetRequest("collections")).ConfigureAwait(false)).Comics;
    }

    /// <summary>
    ///     获取“大家都在看”
    /// </summary>
    /// <param name="page"></param>
    /// <returns></returns>
    public async Task<PicaPage<ComicDetail>> GetEverybodyLoves(int page = 1)
    {
        return (await SendAsync<SimpleGetRequest, PageOfComicsResponse>(
            new SimpleGetRequest($"comics/?page={page}&c=%E5%A4%A7%E5%AE%B6%E9%83%BD%E5%9C%A8%E7%9C%8B&s=ld")).ConfigureAwait(false)).Data;
    }
}