using Newtonsoft.Json;
using System.Collections.Generic;

namespace NewsSentimWebApi.Models
{
    public class NewsDetail
    {
        [JsonProperty("category")]
        public string Category { get; set; }
        [JsonProperty("data")]
        public List<News> News { get; set; }
        [JsonProperty("success")]
        public bool IsSuccess { get; set; }


    }

    public class News
    {
        public string Author { get; set; }
        //public string Title { get; set; }
        //public string ImageUrl { get; set; }
        //public string ReadMoreUrl { get; set; }
        //public string Date { get; set; }
        //public string Url { get; set; }
        public string Content { get; set; }
        //public string Time { get; set; }
    }
}
