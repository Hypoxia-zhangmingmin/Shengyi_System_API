using DemoTest.Utils;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace DemoTest.DLL
{
    public static class RedisBuilderExensions
    {
        public static WebApplicationBuilder UseRedis(this WebApplicationBuilder builder)
        {
            ConfigurationOptions? redisOptions = null;
            if(builder.Environment.IsDevelopment())
            {
                redisOptions = ConfigurationOptions.Parse(builder.Configuration.GetSection("RedisCacheTest:ConnectionStr").Value);

            }
            else
            {
                redisOptions = ConfigurationOptions.Parse(builder.Configuration.GetSection("RedisCache:ConnectionStr").Value);
                string redisPassword = builder.Configuration.GetSection("RedisCache:Password").Value;
                if(!string.IsNullOrEmpty(redisPassword))
                {
                    redisOptions.Password = builder.Configuration.GetSection("RedisCache:Password").Value;
                }
                
            }
            var multiplexer = ConnectionMultiplexer.Connect(redisOptions);
            builder.Services.AddSingleton<IConnectionMultiplexer>(multiplexer);
            builder.Services.AddSingleton<RedisUtils>();
            return builder;
        }
    }
}
