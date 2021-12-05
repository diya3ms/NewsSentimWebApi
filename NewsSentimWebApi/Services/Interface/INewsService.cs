using NewsSentimWebApi.Models;
using System.Collections.Generic;

namespace NewsSentimWebApi.Services.Interface
{
    public interface INewsService
    {
        NewsCategoryProlarityResponse GetMostPositiveNewsCategory();
        PositiveAuthorResponse GetMostPositiveNewsAuthor();
        IEnumerable<NewsArticleResponse> GetTop3PositiveArticles();
    }
}
