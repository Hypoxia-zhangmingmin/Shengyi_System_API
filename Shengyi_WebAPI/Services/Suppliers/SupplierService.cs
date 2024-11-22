
using DemoTest.Utils;
using Shengyi_WebAPI.Daos.Suppliers;
using Shengyi_WebAPI.Models.Out;

namespace Shengyi_WebAPI.Services.Suppliers
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierDao _supplierDao;
        private readonly RedisUtils _redisUtils;

        public SupplierService (ISupplierDao supplierDao, RedisUtils redisUtils)
        {
            _supplierDao = supplierDao;
            _redisUtils = redisUtils;
        }
    
        public async Task<object> CreateSupplierAsync(string supplierName, string phone, string address)
        {
            await _redisUtils.DeleteAny("supplier*");
            var result =await _supplierDao.CreateSupplierAsync(supplierName, phone, address);    
            return result;
        }

        public async Task<object> DeleteSupplierAsync(int id)
        {
            await _redisUtils.DeleteAny("supplier*");
            var result =await _supplierDao.DeleteSupplierAsync(id);
            return result;  
        }

        public async Task<object> EditSupplierAsync(int id, string supplierName, string phone, string address)
        {
            await _redisUtils.DeleteAny("supplier*");
            var result = await _supplierDao.EditSupplierAsync(id, supplierName, phone, address);
            return result;
        }

        public async Task<object> GetAll()
        {
            var result = await _supplierDao.GetAll();
            return result;
        }

        public async Task<object> SearchSupplierAsync(string supplierName)
        {
            string key = "supplier_" + supplierName;
            if (_redisUtils.IsSet(key))
            {
                var data = await _redisUtils.Get<List<OutSupplierInfo>>(key);
                return data;
            }
            var result =await _supplierDao.SearchSupplierAsync(supplierName);
            await _redisUtils.Set(key, result);   
            return result;
        }
    }
}
