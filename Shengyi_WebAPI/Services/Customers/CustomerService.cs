
using DemoTest.Utils;
using Shengyi_WebAPI.Daos.Customers;
using Shengyi_WebAPI.Models.Out;

namespace Shengyi_WebAPI.Services.Customers
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerDao _customerDao;
        private readonly RedisUtils _redisUtils;
        
        public CustomerService (ICustomerDao customerDao, RedisUtils redisUtils)
        {
            _customerDao = customerDao;
            _redisUtils = redisUtils;
        }

        public async Task<object> CreateCustomerAsync(string name, string phone, string address, int creaditId)
        {
            await _redisUtils.DeleteAny("customer*");
            var result =await _customerDao.CreateCustomerAsync(name, phone, address, creaditId); 
            return result;
        }

        public async Task<object> DeleteCustomerAsync(int id)
        {
            await _redisUtils.DeleteAny("customer*");
            var result = await _customerDao.DeleteCustomerAsync(id);
            return result;
        }

        public async Task<object> EditCustomerAsync(int id, string name, string phone, string address, int creaditId)
        {
            await _redisUtils.DeleteAny("customer*");
            var result =await _customerDao.EditCustomerAsync(id,name, phone, address,creaditId);
            return result;
        }

        public async Task<object> GetAddress(int id)
        {
            var result = await _customerDao.GetAddress(id);
            return result;
        }

        public async Task<object> GetAll()
        {
            string key = "customer*";
            if (_redisUtils.IsSet(key))
            {
                var data = await _redisUtils.Get<List<OutCustomerAllInfo>>(key);
                return data;
            }
            var result = await _customerDao.GetAll();
            await _redisUtils.Set(key, result);
            return result;
        }

        public async Task<object> SearchCreditAsync()
        {
            string key = "credit*";
            if (_redisUtils.IsSet(key))
            {
                var data = await _redisUtils.Get<List<OutCreditInfo>>(key);
                return data;
            }
            var result = await _customerDao.SearchCreditAsync();
            await _redisUtils.Set(key, result);
            return result;
        }

        public async Task<object> SearchCustomerAsync(string name)
        {
            string key = "customer" + name;
            if (_redisUtils.IsSet(key))
            {
                var data =await _redisUtils.Get<List<OutCustomerInfo>>(key);
                return data;
            }
            var result = await _customerDao.SearchCustomerAsync(name);
            await _redisUtils.Set(key, result);
            return result;
        }
    }
}
