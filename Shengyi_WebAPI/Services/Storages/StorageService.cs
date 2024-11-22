
using DemoTest.Utils;
using Shengyi_WebAPI.Daos.Storages;

namespace Shengyi_WebAPI.Services.Storages
{
    public class StorageService : IStorageService
    {
        private readonly IStorageDao _storageDao;
        private readonly RedisUtils _redisUtils;

        public StorageService(IStorageDao storageDao, RedisUtils redisUtils)
        {
            _storageDao = storageDao;
            _redisUtils = redisUtils;
        }

        public async Task<object> CreateStorageAsync(int unitId, int commodityId, int supplierId, int count, decimal weight, decimal UnitPrice, decimal TotalPrice)
        {
            var result = await _storageDao.CreateStorageAsync(unitId, commodityId, supplierId, count,weight, UnitPrice, TotalPrice);
            return result;
        }

        public async Task<object> SearchStorageAsync(int categoryId, string start, string end)
        {
            var result = await _storageDao.SearchStorageAsync(categoryId, start, end);  
            return result;
        }
    }
}
