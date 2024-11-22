
using DemoTest.Utils;
using Shengyi_WebAPI.Daos.Sales;
using Shengyi_WebAPI.Models.Out;

namespace Shengyi_WebAPI.Services.Sales
{
    public class SaleService : ISaleService
    {
        private readonly ISaleDao _saleDao;
        private readonly RedisUtils _redisUtils;

        public SaleService(ISaleDao saleDao, RedisUtils redisUtils)
        {
            _saleDao = saleDao;
            _redisUtils = redisUtils;
        }

        public async Task<object> SearchCommodityAsync(string name, string specification, string width, string length, string height, int categoryId)
        {
            var result = await _saleDao.SearchCommodityAsync(name, specification, width, length, height, categoryId);
            return result;
        }
    }
}
