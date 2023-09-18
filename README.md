# picacomic-api-csharp

**[WIP]** 本项目尚未完工！

## 声明

- 本项目不与任何应用程序、软件、网站、服务、团体、组织或公司相关，项目名称同理。
- 这是一个个人项目，仅作为练习 C# 语言使用。
- 如果本项目用于非法用途，作者不会对其负责。

## 用法

推荐使用依赖注入的方式创建 `PicaClient` 实例，生命周期为 `Transient`。

```csharp
using Microsoft.Extensions.DependencyInjection;
using PicacomicSharp;
using PicacomicSharp.DependencyInjection;

var services = new ServiceCollection()
    .AddPicacomicSharp()
    .BuildServiceProvider();
PicaClient client = services.GetRequiredService<PicaClient>();
```

登录以获取验证令牌（token），然后保存它。

令牌不保存在 `PicaClient` 中，你无需考虑它的生命周期。

```csharp
string token = await client.LoginAsync(new()
{
    Email = "email",  // 或者用户名
    Password = "password"
}, saveTokenAutomatically: true);

// saveTokenAutomatically 为 true 时，你不需要手动设置验证令牌。
// client.SaveAuthorization(token);
```

令牌过期时间为 1~7 天，过期后需要重新登录。

## 示例：阅读排行榜第一的漫画

文中的 ShowImage 方法仅作为示例，你需要自己按照功能和实际情况实现。

在这个示例里，你将了解到：
- 如何获取阅读排行榜
- 得到漫画图片 URL 的步骤
- 数据的分页返回（`PicaPage<T>`）

为了便于理解，我没有使用 `var` 关键字，而是使用了完整的类型名称，方法调用也显示标注了参数名称，实际运用中你完全可以不这样做。

```csharp
// 获取阅读排行榜（最近一周）
List<ComicDetail> resp = await client.GetLeaderboardAsync(range: RankDateRange.Day7);
// 获取排行榜第一的漫画（简要信息）
ComicDetail firstComic = resp.First();
// 获取该漫画的详细信息
FullComicDetail detail = await client.GetComicBookByIdAsync(bookId: firstComic.Id);

ShowImage(detail.Cover.ToString()); // 返回封面完整 URL

/* 获取漫画的话数
 * 注意这里的 PicaPage<T>。
 * 一个漫画可能有多个话，如果话数很多，夸张一些，成百上千个，一次性返回所有显然不现实。
 * 通常情况下，API会返回 20/40 个数据作为一页。
 * PicaPage<T> 带有分页信息，你可以通过它来获取下一页的数据，我们在这里先不演示，后面会有。
 */
PicaPage<EpisodeInfo> episodes = await client.GetComicBookEpisodesAsync(bookId: detail.Id, page: 1);

/* 获取第一话信息
 * PicaPage<T> 是一个可重用的容器，那20个数据的列表就是 Data 属性。
 */
EpisodeInfo firstEpisode = episodes.Data.First();

// 获取第一话的图片（40张每分页）
PicaPage<ImageUrl> images = await client.OrderBookImagesAsync(
    bookId: detail.Id,
    epsOrder: firstEpisode.Order,
    page: 1);

// 依次显示每个图片
foreach (ImageUrl image in images.Data)
{
    ShowImage(image.Image.ToString()); // 返回图片完整 URL
}

// 如果有下一页，TryGetNextPage 方法会返回 true
// nextPage 变量代表着下一分页的页码
while (images.TryGetNextPage(out int nextPage))
{
    // 获取后面的N张图片
    images = await client.OrderBookImagesAsync(bookId: detail.Id,
        epsOrder: firstEpisode.Order,
        page: nextPage);
    // 显示图片
    foreach (ImageUrl image in images.Data)
    {
        image.Image.ToString();
    }
}

```

## 更多API

请参考 `PicaClient` 类的定义，以及 IDE 的自动补全功能，相关文档正在完善中。
