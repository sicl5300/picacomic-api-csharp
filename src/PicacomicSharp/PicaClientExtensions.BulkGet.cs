﻿using PicacomicSharp.Common;
using PicacomicSharp.Requests;

namespace PicacomicSharp;

public static partial class PicaClientExtensions
{
    private const int DefaultIterateToPage = 5;

    private static async IAsyncEnumerable<TDoc> BulkGetPages<TDoc, TFuncReturns>(this PicaClient client,
        Func<int, Task<TFuncReturns>> func, int iterateToPage = DefaultIterateToPage)
        where TFuncReturns : PicaPage<TDoc>
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
    public static async Task<List<CommentDetail>> BulkGetMyComments(this PicaClient client,
        int iterateToPage = DefaultIterateToPage)
    {
        return await client
            .BulkGetPages<CommentDetail, PicaPage<CommentDetail>>(client.GetMyCommentsAsync, iterateToPage)
            .ToListAsync();
    }

    /// <summary>
    ///     批量获取我的收藏
    /// </summary>
    /// <param name="client"></param>
    /// <param name="iterateToPage">获取到第几页</param>
    /// <param name="sort">排序</param>
    /// <returns></returns>
    public static async Task<List<ComicDetail>> BulkGetFavourites(this PicaClient client,
        int iterateToPage = DefaultIterateToPage,
        Sort sort = Sort.Default)
    {
        return await client
            .BulkGetPages<ComicDetail, PicaPage<ComicDetail>>(i => client.GetFavouritesAsync(i, sort), iterateToPage)
            .ToListAsync();
    }

    /// <summary>
    ///     批量搜索分类
    /// </summary>
    /// <param name="client"></param>
    /// <param name="payload">分类，排序，分页</param>
    /// <param name="iterateToPage">获取到第几页</param>
    /// <returns></returns>
    public static async Task<List<ComicDetail>> BulkSearchByCategory(this PicaClient client,
        RequestSearchByCategory payload,
        int iterateToPage = DefaultIterateToPage)
    {
        return await client
            .BulkGetPages<ComicDetail, PicaPage<ComicDetail>>(i => client.SearchByCategoryAsync(payload), iterateToPage)
            .ToListAsync();
    }

    /// <summary>
    ///     批量高级搜索
    /// </summary>
    /// <param name="client"></param>
    /// <param name="payload">分页，排序，关键词</param>
    /// <param name="iterateToPage">获取到第几页</param>
    /// <returns></returns>
    public static async Task<List<ComicDetail>> BulkSearchAdvanced(this PicaClient client,
        RequestAdvancedSearch payload,
        int iterateToPage = DefaultIterateToPage)
    {
        return await client
            .BulkGetPages<ComicDetail, PicaPage<ComicDetail>>(i => client.SearchAdvancedAsync(payload), iterateToPage)
            .ToListAsync();
    }

    /// <summary>
    ///     批量搜索
    /// </summary>
    /// <param name="client"></param>
    /// <param name="queryKeyword">查询关键字</param>
    /// <param name="iterateToPage">获取到第几页</param>
    /// <returns></returns>
    public static async Task<List<ComicDetail>> BulkSearch(this PicaClient client, string queryKeyword,
        int iterateToPage = DefaultIterateToPage)
    {
        return await client
            .BulkGetPages<ComicDetail, PicaPage<ComicDetail>>(i => client.SearchAsync(queryKeyword), iterateToPage)
            .ToListAsync();
    }

    /// <summary>
    ///     批量获取漫画章节
    /// </summary>
    /// <param name="client"></param>
    /// <param name="bookId">漫画独一ID</param>
    /// <param name="iterateToPage">获取到第几页</param>
    /// <returns></returns>
    public static async Task<List<EpisodeInfo>> BulkGetComicBookEpisodes(this PicaClient client, string bookId,
        int iterateToPage = DefaultIterateToPage)
    {
        return await client
            .BulkGetPages<EpisodeInfo, PicaPage<EpisodeInfo>>(i => client.GetComicBookEpisodesAsync(bookId, i),
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
    public static async Task<List<ImageUrl>> BulkOrderBookImages(this PicaClient client, string bookId, int epsOrder,
        int iterateToPage = DefaultIterateToPage)
    {
        return await client
            .BulkGetPages<ImageUrl, PicaPage<ImageUrl>>(i => client.OrderBookImagesAsync(bookId, epsOrder, i),
                iterateToPage)
            .ToListAsync();
    }
}