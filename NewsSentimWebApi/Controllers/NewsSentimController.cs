using Microsoft.AspNetCore.Mvc;
using NewsSentimWebApi.Models;
using NewsSentimWebApi.Services.Interface;
using System.Collections.Generic;


namespace NewsSentimWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NewsSentimController : ControllerBase
    {
        private readonly INewsService _newsService;


        public NewsSentimController(INewsService newsService)
        {
            _newsService = newsService;     
        }

        [HttpGet]
        [Route("getMostPositiveNewsCategory")]
        public NewsCategoryProlarityResponse GetMostPositiveNewsCategory()
        {
            return  _newsService.GetMostPositiveNewsCategory();
        }

        [HttpGet]
        [Route("getMostPositiveNewsAuthor")]
        public PositiveAuthorResponse GetMostPositiveNewsAuthor()
        {
            return _newsService.GetMostPositiveNewsAuthor();
        }
    }
}
