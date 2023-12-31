﻿using System.Text.Json.Serialization;

namespace PicacomicSharp.Responses;

internal sealed class CollectionResponse : IResponseData
{
    [JsonPropertyName("collections")] public required List<ComicDetail> Comics { get; set; }
}