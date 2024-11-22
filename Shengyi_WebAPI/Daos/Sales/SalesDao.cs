
using Microsoft.EntityFrameworkCore;
using Shengyi_WebAPI.EFCore;

namespace Shengyi_WebAPI.Daos.Sales
{
    public class SalesDao : ISaleDao
    {
        private readonly DB _db;
        public SalesDao(DB db)
        {
            _db = db;
        }

        public async Task<object> GetAllCategoryAsync()
        {
            var result = await _db.Categories.Select(a => new
            {
                a.Id,
                a.CategoryName
            }).ToListAsync();
            return result;
        }

        public async Task<object> SearchCommodityAsync(string name, string specification, string width, string length, string height,int categoryId)
        {
            var data = _db.Sales.AsQueryable();
            if(!string.IsNullOrWhiteSpace(name))
            {
                data = data.Where(a => a.Commodity.CommodityName.Contains(name));
            }
            if(!string.IsNullOrWhiteSpace(specification))
            {
                data = data.Where(a=>a.Commodity.Specification.Contains( specification));
            }
            if(categoryId != -1)
            {
                 data = data.Where(a=>a.Commodity.CategoryId ==  categoryId);
            }
            if (!string.IsNullOrWhiteSpace(width))
            {
                data= data.Where(a=>a.Commodity.Width == Convert.ToInt32(width));
            }
            if(!string.IsNullOrWhiteSpace(length))
            {
                data = data.Where(a=>a.Commodity.Length == Convert.ToInt32(length));
            }
            if (!string.IsNullOrWhiteSpace(height))
            {
                data = data.Where(a=> a.Commodity.Height == Convert.ToDecimal(height));
            }
            var result = await data.Where(a => a.IsDelete == false).OrderBy(a => a.Commodity.Specification).ThenBy(a => a.Commodity.Length).Select(a => new
            {
                a.Id,
                a.CommodityId,
                a.Commodity.CommodityName,
                a.Commodity.Specification,
                a.Commodity.Length,
                a.Commodity.Width,
                a.Commodity.Height,
                a.Commodity.Category.CategoryName,
                a.Commodity.Unit,
                Count = _db.Inventories.SingleOrDefault(b => b.CommodityId == a.CommodityId) ==null ?0:_db.Inventories.SingleOrDefault(b=>b.CommodityId == a.CommodityId).Count,
                a.SalePrice
            }).ToListAsync();
            return result;
        }
    }
}
