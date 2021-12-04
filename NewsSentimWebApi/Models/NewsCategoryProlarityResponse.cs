namespace NewsSentimWebApi.Models
{
    public class NewsCategoryProlarityResponse
    {
        public string category { get; set; }
        public string author { get; set; }
        public float avgSentimentPolarity { get; set; }
    }

}
