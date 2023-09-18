namespace PicacomicSharp.Requests.Abstractions;

public record SimplePostRequest(string Url) : IPost;

public record SimpleGetRequest(string Url) : IGet;