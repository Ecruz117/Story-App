using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using Microsoft.Extensions.Caching.Memory;

namespace Story_App.Controllers
{
    [ApiController]
    [Route("[controller]")]


    public class HackerNewsController : ControllerBase
    {
        List<HackerNews> newest = new List<HackerNews>();
        private const string baseUrl = "https://hacker-news.firebaseio.com/v0/";
        private const string newestStory = "newstories";
        private const string json = ".json?";
        static HttpClient client = new HttpClient();
        private IMemoryCache cache;



        private readonly ILogger<HackerNewsController> _logger;
        

        public HackerNewsController(ILogger<HackerNewsController> logger, IMemoryCache memoryCache)
        {
            _logger = logger;
            cache = memoryCache;
        }

       


        [HttpGet]
        public async Task<IEnumerable<HackerNews>> Get()
        {
            List<HackerNews> newestStorieList = new List<HackerNews>();
           
            var newestStories = baseUrl + newestStory + json;
            var response = await client.GetAsync(newestStories);
            if (response.IsSuccessStatusCode)
            {
                var storyResponse = response.Content.ReadAsStringAsync().Result;
                storyResponse = storyResponse.Replace("[", string.Empty).Replace("]", string.Empty);
                var storyIdList = storyResponse.Split(',').Select(Int32.Parse).ToList();
                foreach(var i in storyIdList)
                {
                   newest.Add(await GetStoryData(i));
                   

                }

                
               
            }
            return newest.ToArray();
        }
        public async Task<HackerNews> GetStoryData(int storyId)
        {
            return await cache.GetOrCreateAsync(storyId, async cached =>
            {
                HackerNews story = new HackerNews();
                var response = await client.GetAsync(string.Format(baseUrl + "item/{0}" + json, storyId));
                if (response.IsSuccessStatusCode)
                {
                    var storyRepo = response.Content.ReadAsStringAsync().Result;
                    story = JsonConvert.DeserializeObject<HackerNews>(storyRepo);
                }

                return story;
            });
            
            
        }
    }
}
