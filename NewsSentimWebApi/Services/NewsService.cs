using NewsSentimWebApi.Services.Interface;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using NewsSentimWebApi.Models;
using static NewsSentimWebApi.Enums.Enums;
using System.Text;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;

namespace NewsSentimWebApi.Services
{
    public class NewsService: INewsService
    {
        private const string URL = "https://inshortsapi.vercel.app/news?category=";
        private const string SentimURL = "https://sentim-api.herokuapp.com/api/v1/";
        HttpClient client = new HttpClient();

        public IEnumerable<NewsCategoryProlarityResponse> GetNews()
        {
            List<NewsCategoryProlarityResponse> newsCategoryProlarityResponse = new List<NewsCategoryProlarityResponse>();
            try
            {
                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
                foreach (string category in Enum.GetNames(typeof(NewsCategory)))
                {
                    float avgCategorySentiment = 0;
                    var newsObj = getNewsForCategory(category);
                    foreach (var data in newsObj.News)
                    {
                       var sentimNews = getSentimForNews(data.Content);
                       avgCategorySentiment = (avgCategorySentiment + sentimNews.NewsSentim.Polarity);
                    }
                    avgCategorySentiment = avgCategorySentiment / newsObj.News.Count;
                    newsCategoryProlarityResponse.Add(new Models.NewsCategoryProlarityResponse
                    {
                        category = category,
                        avgSentimentPolarity= avgCategorySentiment
                    });
                }
                client.Dispose();
                var res=newsCategoryProlarityResponse.Where(i=>i.avgSentimentPolarity.Equals(newsCategoryProlarityResponse.Max(j=>j.avgSentimentPolarity)));
                return res;
            }
            catch(Exception ex)
            {
                client.Dispose();
                throw;
            }
            
        }

        private NewsDetail getNewsForCategory( string category)
        {
           var url= URL + category;
           HttpResponseMessage response = client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<NewsDetail>().Result;
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);               
                return null;
            }
        }

        private SentimDetail getSentimForNews(string content)
        {
            var body = new SentimRequest
            {
                text = content
            };
            var requestContent = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");
            HttpResponseMessage res = client.PostAsync(SentimURL, requestContent).Result;
            //if (res.IsSuccessStatusCode)
            {
               return res.Content.ReadAsAsync<SentimDetail>().Result;
            }
        }

    }
}
