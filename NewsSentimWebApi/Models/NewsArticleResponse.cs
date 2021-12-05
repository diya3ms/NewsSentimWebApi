
namespace NewsSentimWebApi.Models
{
    public class NewsArticleResponse
    {
        public string Title { get; set; }
        public string category { get; set; }
        public string Author { get; set; }
        public float SentimentPolarity { get; set; }

    }
}
