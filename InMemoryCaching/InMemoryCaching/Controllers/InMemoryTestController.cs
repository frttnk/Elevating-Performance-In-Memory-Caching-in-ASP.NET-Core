using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace InMemoryCaching.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InMemoryTestController : ControllerBase
    {
        private readonly IMemoryCache _cache;

        public InMemoryTestController(IMemoryCache cache)
        {
            this._cache = cache;
        }

        [HttpGet]
        public IActionResult InMemoryCache()
        {
            string result = "";
            //var cacheResult = _cache.Get<string>("CacheKey") ?? "";
            //if (cacheResult=="")
            //{
            //    result = FakeDbConnection();
            //    // you can put everything as a cachekey.
            //    // you can configure cache time up to your requirements.
            //    _cache.Set<string>("CacheKey", result, TimeSpan.FromMinutes(2));
            //}

            //if (!_cache.TryGetValue("CacheKey", out result))
            //{
            //    result = FakeDbConnection();
            //    _cache.Set<string>("CacheKey", result, TimeSpan.FromMinutes(2));
            //}

            if (!_cache.TryGetValue("CacheKey", out result))
            {
                result = FakeDbConnection();
                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromHours(1))//how long it will be inactive
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(10))// if there is a problem with sliding expiration, the absolute expiration will determine the cache
                    .SetPriority(CacheItemPriority.High)// how important the cache is
                    .SetSize(2048);//size of cache entry value
                _cache.Set<string>("CacheKey", result, cacheOptions);
            }

            
            return Ok(result);
        }

        private string FakeDbConnection()
        {
            return "Connected Db";
        }

    }
}