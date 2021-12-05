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
        public string Title { get; set; }
    }
}
