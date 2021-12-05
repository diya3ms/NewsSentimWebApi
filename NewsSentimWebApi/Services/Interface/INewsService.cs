using NewsSentimWebApi.Models;
using System.Collections.Generic;

namespace NewsSentimWebApi.Services.Interface
{
    public interface INewsService
    {
        NewsCategoryProlarityResponse GetMostPositiveNewsCategory();
        AuthorPolarityResponse GetMostPositiveNewsAuthor();
        IEnumerable<NewsArticleResponse> GetTop3PositiveArticles();
    }
}
