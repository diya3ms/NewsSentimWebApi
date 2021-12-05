

namespace NewsSentimWebApi.Models
{
    public class PositiveAuthorResponse
    {
        public string author { get; set; }
        public float avgSentimentPolarity { get; set; }
        public int count { get; set; }
    }
}
