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

            if (!_cache.TryGetValue("CacheKey", out result))
            {
                result = FakeDbConnection();
                _cache.Set<string>("CacheKey", result, TimeSpan.FromMinutes(2));
            }
            return Ok(result);
        }

        private string FakeDbConnection()
        {
            return "Connected Db";
        }

    }
}