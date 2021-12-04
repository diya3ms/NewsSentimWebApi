using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NewsSentimWebApi.Models;
using NewsSentimWebApi.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity;


namespace NewsSentimWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NewsSentimController : ControllerBase
    {
        private readonly ILogger<NewsSentimController> _logger;
        private readonly INewsService _newsService;


        public NewsSentimController(ILogger<NewsSentimController> logger, INewsService newsService)
        {
            _logger = logger;
            _newsService = newsService;
       
        }

        [HttpGet]
        [Route("getMostPositiveNewsCategory")]
        public IEnumerable<NewsCategoryProlarityResponse> Get()
        {
            return  _newsService.GetNews();
        }
    }
}
