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
    public class NewsService : INewsService
    {
        private const string URL = "https://inshortsapi.vercel.app/news?category=";
        private const string SentimURL = "https://sentim-api.herokuapp.com/api/v1/";
        HttpClient client = new HttpClient();

        public NewsCategoryProlarityResponse GetMostPositiveNewsCategory()
        {
            NewsCategoryProlarityResponse newsCategoryProlarityResponse = new NewsCategoryProlarityResponse
            {
                avgSentimentPolarity = -100
            };
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
                        var sentimNews = getSentimForNews(data.Title);
                        avgCategorySentiment += sentimNews.NewsSentim.Polarity;
                    }
                    avgCategorySentiment /= newsObj.News.Count;

                    if (avgCategorySentiment > newsCategoryProlarityResponse.avgSentimentPolarity)
                    {
                        newsCategoryProlarityResponse.category = category;
                        newsCategoryProlarityResponse.avgSentimentPolarity = avgCategorySentiment;
                    }
                }
                client.Dispose();
                return newsCategoryProlarityResponse;
            }
            catch (Exception ex)
            {
                client.Dispose();
                throw;
            }

        }
        public PositiveAuthorResponse GetMostPositiveNewsAuthor()
        {
            PositiveAuthorResponse mostPositiveAuthor;
            List<PositiveAuthorResponse> mostPositiveAuthorList = new List<PositiveAuthorResponse>();
            try
            {
                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
                foreach (string category in Enum.GetNames(typeof(NewsCategory)))
                {
                    mostPositiveAuthor = initializePositiveAuthorResponse();
                    var newsObj = getNewsForCategory(category);
                    foreach (var data in newsObj.News)
                    {
                        var sentimNews = getSentimForNews(data.Title);
                        if (sentimNews.NewsSentim.Polarity > mostPositiveAuthor.avgSentimentPolarity || mostPositiveAuthor.author.Equals(data.Author))
                        {
                            if (mostPositiveAuthor.author.Equals(data.Author))
                            {
                                mostPositiveAuthor.count++;
                                mostPositiveAuthor.avgSentimentPolarity = (mostPositiveAuthor.avgSentimentPolarity + sentimNews.NewsSentim.Polarity);
                            }
                            else
                            {
                                mostPositiveAuthor.author = data.Author;
                                mostPositiveAuthor.avgSentimentPolarity = sentimNews.NewsSentim.Polarity;
                            }
                        }
                    }
                    var author = mostPositiveAuthorList.Where(item => item.author.Equals(mostPositiveAuthor.author));
                    if (author.Any())
                    {
                        author.First().avgSentimentPolarity += mostPositiveAuthor.avgSentimentPolarity;
                        author.First().count += 1;
                    }
                    else
                    {
                        mostPositiveAuthorList.Add(mostPositiveAuthor);
                    }
                }
                client.Dispose();
                mostPositiveAuthor=mostPositiveAuthorList.OrderByDescending(i => i.avgSentimentPolarity).First();
                mostPositiveAuthor.avgSentimentPolarity /= mostPositiveAuthor.count;
                return mostPositiveAuthor;
            }
            catch (Exception ex)
            {
                client.Dispose();
                throw;
            }
        }

        public IEnumerable<NewsArticleResponse> GetTop3PositiveArticles()
        {
            List<SentimDetail> sentimDetailsList = new List<SentimDetail>();
            List<NewsArticleResponse> topNewsArticleList = new List<NewsArticleResponse>();
            List<NewsArticleResponse> newsArticleResponses = new List<NewsArticleResponse>();
            try
            {
                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
                foreach (string category in Enum.GetNames(typeof(NewsCategory)))
                {
                    var newsObj = getNewsForCategory(category);
                    foreach (var data in newsObj.News)
                    {
                        var sentimNews = getSentimForNews(data.Title);
                        newsArticleResponses.Add(parseSentimDetail(sentimNews, data, category));
                    }
                    topNewsArticleList = newsArticleResponses.OrderByDescending(i => i.SentimentPolarity).Take(3).ToList();
                    newsArticleResponses = topNewsArticleList;
                }
                return topNewsArticleList.AsEnumerable();
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        private NewsArticleResponse parseSentimDetail(SentimDetail sentimDetail, News news, string category)
        {
            return new NewsArticleResponse
            {
                Author = news.Author,
                category = category,
                SentimentPolarity = sentimDetail.NewsSentim.Polarity,
                Title = news.Title
            };
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
            if (res.IsSuccessStatusCode)
            {
               return res.Content.ReadAsAsync<SentimDetail>().Result;
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)res.StatusCode, res.ReasonPhrase);
                return null;
            }
        }

        private PositiveAuthorResponse initializePositiveAuthorResponse()
        {
            return new PositiveAuthorResponse
            {
                count = 1,
                author = "",
                avgSentimentPolarity= -100
            };

        }
    }
}
