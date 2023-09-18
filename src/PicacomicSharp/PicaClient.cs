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
    public HttpClient Client { get; private set; }

    public PicaClient(PicaConfiguration configuration, HttpClient client)
    {
        _configuration = configuration;
        client.BaseAddress = new Uri(configuration.BaseUrl);
        client.Timeout = TimeSpan.FromSeconds(configuration.Timeout);
        foreach (var pair in configuration.DefaultHeaders)
        {
            client.DefaultRequestHeaders.Add(pair.Key, pair.Value);
        }

        Client = client;
    }

    internal async Task<TResponse> SendAsync<TRequest, TResponse>(TRequest config)
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

        var response = await Client.SendAsync(requestMessage);
        var message = await response.Content.ReadAsStringAsync();

        var wrapped = JsonSerializer.Deserialize<WrappedResponse<TResponse>>(message, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        if (wrapped.Data is not null) return wrapped.Data;

        if (wrapped.Error is not null) // Data is null, but Error is not null
            throw new InvalidOperationException($"Error: {wrapped.Error}, Message: {wrapped.Message ?? "None"}.");

        throw new InvalidOperationException("Invalid response.");
    }

    public void SaveAuthorization(string token)
    {
        _configuration.AuthorizationToken = token;
    }

    public async Task<string> LoginAsync(RequestLogin payload, bool saveTokenAutomatically = false)
    {
        var re = await SendAsync<RequestLogin, LoginResponse>(payload);
        var token = re.Token;
        if (saveTokenAutomatically) this.SaveAuthorization(token);
        return token;
    }

    public async Task<bool> RegisterAsync(RequestRegister payload)
    {
        _ = await SendAsync<RequestRegister, RegisterResponse>(payload);
        return true;
    }

    public async Task<PunchInResult> PunchInAsync()
    {
        return await SendAsync<SimplePostRequest, PunchInResult>(new SimplePostRequest("users/punch-in"));
    }

    public async Task<User> GetProfileAsync(string? id = null)
    {
        return (await SendAsync<RequestProfile, ProfileResponse>(new RequestProfile { Id = id })).Data;
    }

    public async Task<List<ChatDetail>> GetChatListAsync()
    {
        return (await SendAsync<SimpleGetRequest, ChatListResponse>(new SimpleGetRequest("chat"))).ChatDetail;
    }

    public async Task<InitResult> GetInitMessage()
    {
        return await SendAsync<SimpleGetRequest, InitResult>(new SimpleGetRequest("init?platform=android"));
    }

    public async Task<List<CategoryDetail>> GetCategoryListAsync()
    {
        return (await SendAsync<SimpleGetRequest, CategoryListResponse>(new SimpleGetRequest("categories"))).Categories;
    }

    public async Task<PicaPage<ComicDetail>> SearchByCategoryAsync(RequestSearchByCategory payload)
    {
        return (await SendAsync<RequestSearchByCategory, PageOfComicsResponse>(payload)).Data;
    }

    public async Task<PicaPage<ComicDetail>> SearchAdvancedAsync(RequestAdvancedSearch payload)
    {
        return (await SendAsync<RequestAdvancedSearch, PageOfComicsResponse>(payload)).Data;
    }

    public async Task<PicaPage<ComicDetail>> SearchAsync(string queryKeyword, int page = 1)
    {
        return (await SendAsync<SimpleGetRequest, PageOfComicsResponse>(
            new SimpleGetRequest($"comics/search?page={page.ToString()}&q={HttpUtility.UrlEncode(queryKeyword)}"))).Data;
    }

    public async Task<PicaPage<CommentDetail>> GetMyCommentsAsync(int page = 1)
    {
        return (await SendAsync<SimpleGetRequest, PageOfCommentsResponse>(
            new SimpleGetRequest($"users/my-comments?page={page.ToString()}"))).Data;
    }

    public async Task<PicaPage<ComicDetail>> GetFavouritesAsync(int page = 1, Sort sort = Sort.Default)
    {
        return (await SendAsync<SimpleGetRequest, PageOfComicsResponse>(
            new SimpleGetRequest($"users/favourite?s={sort.ToApiString()}" +
                                 $"&page={page.ToString()}"))).Data;
    }

    public async Task<List<ComicDetail>> GetLeaderboardAsync(RankDateRange range) // is paged?
    {
        return (await SendAsync<SimpleGetRequest, LeaderboardResponse>(
            new SimpleGetRequest($"comics/leaderboard?tt={range.ToApiString()}&ct=VC"))).Comics;
    }

    public async Task<AddFavouriteResult> AddFavouriteAsync(string comicId)
    {
        var re = await SendAsync<SimplePostRequest, AddFavouriteResponse>(
            new SimplePostRequest($"comics/{comicId}/favourite"));
        return re.ActionEnum;
    }

    public async Task<FullComicDetail> GetComicBookByIdAsync(string bookId)
    {
        return (await SendAsync<SimpleGetRequest, ComicBookResponse>(
            new SimpleGetRequest($"comics/{bookId}"))).ComicDetail;
    }

    [Obsolete("永远返回空列表，疑似弃用")]
    public async Task<List<ComicDetail>> GetRecommendationByBookAsync(string bookId)
    {
        return (await SendAsync<SimpleGetRequest, RecommendationsResponse>(
            new SimpleGetRequest($"comics/{bookId}/recommendation"))).Comics;
    }

    public async Task<PicaPage<EpisodeInfo>> GetComicBookEpisodesAsync(string bookId, int page = 1)
    {
        return (await SendAsync<SimpleGetRequest, EpisodesResponse>(
            new SimpleGetRequest($"comics/{bookId}/eps?page={page}"))).Data;
    }

    public async Task<PicaPage<ImageUrl>> OrderBookImagesAsync(string bookId, int epsOrder, int page = 1)
    {
        return (await SendAsync<SimpleGetRequest, ImageOrderResponse>(
            // epsOrder starts from 1
            new SimpleGetRequest($"comics/{bookId}/order/{epsOrder}/pages?page={page}"))).Data;
    }

    public async Task<List<string>> GetKeywordsAsync()
    {
        return (await SendAsync<SimpleGetRequest, KeywordsResponse>(
            new SimpleGetRequest("keywords"))).Keywords;
    }

    public async Task<List<ComicDetail>> GetRandomAsync()
    {
        return (await SendAsync<SimpleGetRequest, RecommendationsResponse>(
            new SimpleGetRequest("comics/random"))).Comics;
    }

    public async Task<List<ComicDetail>> GetCollectionsAsync()
    {
        return (await SendAsync<SimpleGetRequest, CollectionResponse>(
            new SimpleGetRequest("collections"))).Comics;
    }

    public async Task<PicaPage<ComicDetail>> GetEverybodyLoves(int page = 1)
    {
        return (await SendAsync<SimpleGetRequest, PageOfComicsResponse>(
            new SimpleGetRequest($"comics/?page={page}&c=%E5%A4%A7%E5%AE%B6%E9%83%BD%E5%9C%A8%E7%9C%8B&s=ld"))).Data;
    }
}