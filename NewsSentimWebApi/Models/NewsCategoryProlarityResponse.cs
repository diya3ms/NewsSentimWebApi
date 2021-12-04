using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsSentimWebApi.Models
{
    public class NewsCategoryProlarityResponse
    {
        public string category { get; set; }
        public string author { get; set; }
        public float avgSentimentPolarity { get; set; }
    }

}
