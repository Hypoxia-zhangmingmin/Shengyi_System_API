using DemoTest.Utils;
using Shengyi_WebAPI.Daos.Category;
using Shengyi_WebAPI.EFCore;
using Shengyi_WebAPI.Models.Out;

namespace Shengyi_WebAPI.Services.Category
{
    public class CategoryService:ICategoryService
    {

        private readonly ICategoryDao _categoryDao;
        private readonly RedisUtils _redisUtils;

        public CategoryService(ICategoryDao categoryDao, RedisUtils redisUtils)
        {
            _categoryDao = categoryDao;
            _redisUtils = redisUtils;
        }

        public async Task<object> AddCategory(string name,string description)
        {
            await _redisUtils.DeleteAny("category*");
            return await _categoryDao.AddCategory(name, description);
        }

        public async Task<object> DeleteCategory(int id)
        {
            await _redisUtils.DeleteAny("category*");
            return await _categoryDao.DeleteCategory(id);
        }

        public async Task<object> EditCategory(int id, string name, string description)
        {

            await _redisUtils.DeleteAny("category*");
            return await  _categoryDao.EditCategory(id, name, description);
        }

        public async Task<object> GetCategory()
        {
            string key = "category_All";
            if(_redisUtils.IsSet(key))
            {
                var data = await _redisUtils.Get<OutCategoryAllInfo>(key);
                return data;
            }
            var result = await _categoryDao.GetCategory();
            return result;
        }

        public async Task<object> GetCategoryCount(string? name)
        {
            return await _categoryDao.GetCategoryCount(name);
        }

        public async Task<object> SearchCategory(string? name, int currentPage)
        {
            string key = "category_"+ name +"_"+currentPage;
            if (_redisUtils.IsSet(key))
            {
                var data = await _redisUtils.Get<List<OutCategoryInfo>>(key);
                if(data != null)
                {
                    return data;
                }
            }
            var result =  await _categoryDao.SearchCategory(name,currentPage);
            await _redisUtils.Set(key, result);
            return result;
            
        }
    }
}
