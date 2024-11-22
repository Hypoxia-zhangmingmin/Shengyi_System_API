
using DemoTest.Utils;
using Shengyi_WebAPI.Daos.Commondity;
using Shengyi_WebAPI.Models.Out;

namespace Shengyi_WebAPI.Services.Commondity
{
    public class CommondityService : ICommondityService
    {
        private readonly ICommondityDao _commondityDao;
        private readonly RedisUtils _redisUtils;

        public CommondityService(ICommondityDao commondityDao, RedisUtils redisUtils)
        {
            _commondityDao = commondityDao;
            _redisUtils = redisUtils;
        }

        public async Task<object> CreateCommonditySpec(int categoryId, string commondityName, string specification, string length, string width, string height,string weight, string description, string unit, int count, decimal nowTonPrice, decimal price)
        {
           await  _redisUtils.DeleteAny("commondity*");
            var result = await _commondityDao.CreateCommonditySpec(categoryId, commondityName, specification, length, width, height,weight, description, unit, count, nowTonPrice, price);
            return result;
        }

        public async Task<object> CreateSales()
        {
            var result = await _commondityDao.CreateSales();
            return result;
        }

        public async Task<object> DeleteCommodity(int id)
        {
            var result = await _commondityDao.DeleteCommodity(id); return result;
        }

        public async Task<object> EditCommodity(int id, string commondityName, string specification, string length, string width, string height, string weight, string description, string unit, int count, decimal nowTonPrice, decimal price)
        {
            var result = await _commondityDao.EditCommodity(id,commondityName, specification, length,width,height,weight, description, unit, count,nowTonPrice, price);
            return result;
        }

        public async Task<object> GetCommondityCount(int categoryId, string commondityName, string length, string width, string height, int currentpage, string specification)
        {
            var result = await _commondityDao.GetCommondityCount(categoryId, commondityName, length, width, height, currentpage, specification);
            return result;
        }

        public async Task<object> GetSales(int id)
        {
            var result = await _commondityDao.GetSales(id);
            return result;          
        }

        public async Task<object> SearchCommondityInfo(int CategoryId, string Name, string length, string width, string height, int currentpage, string specification)
        {
            var result = await _commondityDao.SearchCommondityInfo(CategoryId, Name, length,width,height, currentpage, specification);
            return result;
        }
    }
}
