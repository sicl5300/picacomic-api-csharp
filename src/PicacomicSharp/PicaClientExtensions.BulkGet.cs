using System.Diagnostics.CodeAnalysis;
using PicacomicSharp.Common;
using PicacomicSharp.Requests;

namespace PicacomicSharp;

[SuppressMessage("Usage", "MA0004:Use Task.ConfigureAwait(false)")]
public static partial class PicaClientExtensions
{
    private const int DefaultIterateToPage = 5;

    private static async IAsyncEnumerable<TDoc> BulkGetPages<TDoc>(Func<int, Task<PicaPage<TDoc>>> func,
        int iterateToPage = DefaultIterateToPage)
    {
        var it = await func(1);

        while (it.TryGetNextPage(out int nextPage))
        {
            if (nextPage > iterateToPage) break;
            foreach (var tDoc in it.Data) yield return tDoc;
            it = await func(nextPage);
        }
    }


    /// <summary>
    ///     批量获取我的评论
    /// </summary>
    /// <param name="client"></param>
    /// <param name="iterateToPage">获取到第几页</param>
    /// <returns></returns>
    public static async Task<IList<CommentDetail>> BulkGetMyComments(this PicaClient client,
        int iterateToPage = DefaultIterateToPage)
    {
        return await BulkGetPages<CommentDetail>(client.GetMyCommentsAsync, iterateToPage)
            .ToListAsync();
    }

    /// <summary>
    ///     批量获取我的收藏
    /// </summary>
    /// <param name="client"></param>
    /// <param name="iterateToPage">获取到第几页</param>
    /// <param name="sort">排序</param>
    /// <returns></returns>
    public static async Task<IList<ComicDetail>> BulkGetFavourites(this PicaClient client,
        int iterateToPage = DefaultIterateToPage,
        Sort sort = Sort.Default)
    {
        return await BulkGetPages<ComicDetail>(i => client.GetFavouritesAsync(i, sort), iterateToPage)
            .ToListAsync();
    }

    /// <summary>
    ///     批量搜索分类
    /// </summary>
    /// <param name="client"></param>
    /// <param name="payload">分类，排序，分页</param>
    /// <param name="iterateToPage">获取到第几页</param>
    /// <returns></returns>
    public static async Task<IList<ComicDetail>> BulkSearchByCategory(this PicaClient client,
        RequestSearchByCategory payload,
        int iterateToPage = DefaultIterateToPage)
    {
        return await BulkGetPages<ComicDetail>(i =>
            {
                payload.Page = i;
                return client.SearchByCategoryAsync(payload);
            }, iterateToPage)
            .ToListAsync();
    }

    /// <summary>
    ///     批量高级搜索
    /// </summary>
    /// <param name="client"></param>
    /// <param name="payload">分页，排序，关键词</param>
    /// <param name="iterateToPage">获取到第几页</param>
    /// <returns></returns>
    public static async Task<IList<ComicDetail>> BulkSearchAdvanced(this PicaClient client,
        RequestAdvancedSearch payload,
        int iterateToPage = DefaultIterateToPage)
    {
        return await BulkGetPages<ComicDetail>(i =>
            {
                payload.Page = i;
                return client.SearchAdvancedAsync(payload);
            }, iterateToPage)
            .ToListAsync();
    }

    /// <summary>
    ///     批量搜索
    /// </summary>
    /// <param name="client"></param>
    /// <param name="queryKeyword">查询关键字</param>
    /// <param name="iterateToPage">获取到第几页</param>
    /// <returns></returns>
    public static async Task<IList<ComicDetail>> BulkSearch(this PicaClient client, string queryKeyword,
        int iterateToPage = DefaultIterateToPage)
    {
        return await BulkGetPages<ComicDetail>(i => client.SearchAsync(queryKeyword, i), iterateToPage)
            .ToListAsync();
    }

    /// <summary>
    ///     批量获取漫画章节
    /// </summary>
    /// <param name="client"></param>
    /// <param name="bookId">漫画独一ID</param>
    /// <param name="iterateToPage">获取到第几页</param>
    /// <returns></returns>
    public static async Task<IList<EpisodeInfo>> BulkGetComicBookEpisodes(this PicaClient client, string bookId,
        int iterateToPage = DefaultIterateToPage)
    {
        return await BulkGetPages<EpisodeInfo>(i => client.GetComicBookEpisodesAsync(bookId, i),
                iterateToPage)
            .ToListAsync();
    }

    /// <summary>
    ///     批量获取漫画章节图片
    /// </summary>
    /// <param name="client"></param>
    /// <param name="bookId">漫画独一Id</param>
    /// <param name="epsOrder">章节编号</param>
    /// <param name="iterateToPage">获取到第几页</param>
    /// <returns></returns>
    public static async Task<IList<ImageUrl>> BulkOrderBookImages(this PicaClient client, string bookId, int epsOrder,
        int iterateToPage = DefaultIterateToPage)
    {
        return await BulkGetPages<ImageUrl>(i => client.OrderBookImagesAsync(bookId, epsOrder, i),
                iterateToPage)
            .ToListAsync();
    }
}