using Microsoft.EntityFrameworkCore;
using Shengyi_WebAPI.EFCore;
using Shengyi_WebAPI.Models.Out;

namespace Shengyi_WebAPI.Daos.Category
{
    public class CategoryDao : ICategoryDao
    {
        private readonly DB _db;
        public CategoryDao(DB db)
        {
            _db = db;
        }

       public async Task<object> AddCategory(string name, string description)
        {
            _db.Categories.Add(new EFCore.Category
            {
                CategoryName = name,
                Description = description,
                CreateTime = DateTime.Now,
                IsDelete = false,
            });
            return await _db.SaveChangesAsync() >= 1;
        }

        public async Task<object> SearchCategory(string? name, int currentPage)
        {
           var result = await _db.Categories.Where(a=> (string.IsNullOrEmpty(name) || a.CategoryName.ToLower().Contains(name.ToLower())) && a.IsDelete == false).Select(a=>new
            {
               a.Id,
                a.CategoryName,
                a.Description,
            }).Skip((currentPage-1) * 16).Take(16).ToListAsync();
            
            return result;
        }

        public async Task<object> EditCategory(int id, string name, string description)
        {
            var data = await _db.Categories.SingleAsync(a => a.Id == id);

            data.CategoryName = name;
            data.Description = description;
            return await _db.SaveChangesAsync() >= 1;
        }

        public async Task<object> GetCategoryCount(string? name)
        {
            var data = await _db.Categories.Where(a => string.IsNullOrEmpty(name) || a.CategoryName.ToLower().Contains(name.ToLower())).ToListAsync();

            return data.Count;

        }

        public async Task<object> DeleteCategory(int id)
        {
            var data = await _db.Categories.SingleAsync(a => a.Id== id);
            data.DeleteTime = DateTime.Now;
            data.IsDelete = true;   
            return await _db.SaveChangesAsync() >=1;
        }

        public async Task<object> GetCategory()
        {
            var result = await _db.Categories.Select(a => new
            {
                a.Id,
                a.CategoryName
            }).ToListAsync();
            return result;
        }
    }
}
