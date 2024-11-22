
using DemoTest.Utils;
using Shengyi_WebAPI.Daos.Inventory;

namespace Shengyi_WebAPI.Services.Inventory
{
    public class InventoryService : IInventoryService
    {
        private readonly IInventoryDao _inventoryDao;
        private readonly RedisUtils _redisUtils;

        public InventoryService (IInventoryDao inventoryDao, RedisUtils redisUtils)
        {
            _inventoryDao = inventoryDao;
            _redisUtils = redisUtils;
        }

        public async Task<object> CreateInventoryAsync(int unitId,int commodityId, int supplierId, int count, decimal UnitPrice, decimal TotalPrice)
        {
            var result = await _inventoryDao.CreateStorageAsync(unitId,commodityId,supplierId, count, UnitPrice, TotalPrice);
            return result;
        }

        public async Task<object> EditInventoryAsync(int id, int count)
        {
            var result = await _inventoryDao.EditInventoryAsync(id, count);  
            return result;
        }

        public async Task<object> SearchInventoryAsync(int categoryId, string name, string specification, string width, string length, string height)
        {
            var result = await _inventoryDao.SearchInventoryAsync(categoryId, name, specification, width, length,height);
            return result;
        }
    }
}
