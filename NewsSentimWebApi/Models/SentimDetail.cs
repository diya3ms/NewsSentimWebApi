using Newtonsoft.Json;
using System.Collections.Generic;

namespace NewsSentimWebApi.Models
{
    public class SentimDetail
    {
        [JsonProperty("result")]
        public Sentiment NewsSentim { get; set; }
        [JsonProperty("sentences")]
        public List<SentenceSentim> SentenceSentim { get; set; }
    }

    public class SentenceSentim
    {
        public string Sentence { get; set; }
        public Sentiment Sentiment { get; set; }
    }

    public class Sentiment
    {
        public float Polarity { get; set; }
    }
}
