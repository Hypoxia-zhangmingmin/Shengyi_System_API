using Newtonsoft.Json;
using Shengyi_WebAPI.Utils;
using StackExchange.Redis;

namespace DemoTest.Utils
{
    public class RedisUtils
    {
        private readonly IConnectionMultiplexer _redis;
        private readonly IConfiguration _configuration;

        public RedisUtils(IConnectionMultiplexer redis, IConfiguration configuration)
        {
            _redis = redis;
            _configuration = configuration;
        }
        private IDatabase GetDatabase()=> _redis.GetDatabase();

        public bool IsSet(string key) => GetDatabase().KeyExists(key);

        public async Task<T?> Get<T>(string key)
        {
            string data = await GetDatabase().StringGetAsync(key);
            return JsonConvert.DeserializeObject<T>(data);
        }

        public async Task Set<T>(string key,T value,TimeSpan? expiry = null)=>await GetDatabase().StringSetAsync(key,JsonConvert.SerializeObject(value),expiry);


        public async Task Push(string key,string value,TimeSpan? expire = null)
        {
            bool isExist = IsSet(key);
            await GetDatabase().ListLeftPushAsync(key, value);
            if(!isExist)
                await KeyExpireAsync(key,expire);
        }

        public async Task<List<string>> GetList(string key)
        {
            var data = await GetDatabase().ListRangeAsync(key);
            var array = Array.ConvertAll(data,x=>(string)x);
            return array.ToList();
        } 

        public async Task<bool> KeyExpireAsync(string key,TimeSpan? expire)
        {
            return await GetDatabase().KeyExpireAsync(key, expire);
        }

        public async Task Delete(string key) => await GetDatabase().KeyDeleteAsync(key);

        public async Task DeleteAny(string key)
        {
            string redisAddress = string.Empty;
            if (CommonExt.Is_Dev)
                redisAddress = _configuration.GetSection("RedisCacheTest:ConnectionStr").Value;
            else
                redisAddress = _configuration.GetSection("RedisCache:ConnectionStr").Value;

            var keys = _redis.GetServer(redisAddress, 6379).Keys(0, key).ToList();
            foreach (var item in keys)
            {
                var keyName = item.ToString();
                await Delete(keyName);
            }
        }
    }
}
