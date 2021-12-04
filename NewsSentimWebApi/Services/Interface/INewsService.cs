using NewsSentimWebApi.Models;
using System.Collections.Generic;

namespace NewsSentimWebApi.Services.Interface
{
    public interface INewsService
    {
        IEnumerable<NewsCategoryProlarityResponse> GetNews();
    }
}
