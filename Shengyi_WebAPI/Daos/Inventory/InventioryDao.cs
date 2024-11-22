
using Microsoft.EntityFrameworkCore;
using Shengyi_WebAPI.EFCore;

namespace Shengyi_WebAPI.Daos.Inventory
{
    public class InventioryDao : IInventoryDao
    {
        private readonly DB _db;

        public InventioryDao(DB db) {  _db = db; }

        public async Task<object> CreateStorageAsync(int unitId,int commodityId, int supplierId, int count, decimal UnitPrice, decimal TotalPrice)
        {
            var commodity = await _db.Commodities.SingleOrDefaultAsync(a => a.Id == commodityId);
            if (commodity == null)
            {
                return false;
            }
            var num = _db.StorageRecords.Where(a => a.Id == commodityId && a.CreateTime.Date == DateTime.Now.Date).Count();
            var storage = new StorageRecord()
            {
                CommodityId = commodityId,
                SupplierId = supplierId,
                UnitPrice = UnitPrice,
                TotalPrice = TotalPrice,
                CreateTime = DateTime.Now,
                IsDelete = false,
                BatchNumber = "SY" + DateTime.Now.ToString("yyyyMMdd") + commodityId + (num + 1).ToString("00")
            };
            if (unitId == 1)
            {
                storage.Count = count;
            }
            else
            {
                storage.Count = commodity.Count * count;
            }
            _db.StorageRecords.Add(storage);
            return await _db.SaveChangesAsync() >= 1;
        }

        public async Task<object> EditInventoryAsync(int id, int count)
        {
            var data =await _db.Inventories.SingleOrDefaultAsync(a => a.CommodityId == id);
            if(data == null)
            {
                return false;
            }
            data.Count = count;
            return await _db.SaveChangesAsync()>=1;
        }


        public async Task<object> SearchInventoryAsync(int categoryId, string name, string specification, string width, string length, string height)
        {
            var commodity =  _db.Commodities.AsQueryable();
            if(categoryId != -1)
            {
                commodity = commodity.Where(a => a.CategoryId == categoryId);
            }
            if(!string.IsNullOrWhiteSpace(name))
            {
                commodity = commodity.Where(a=>a.CommodityName == name);
            }
            if (!string.IsNullOrWhiteSpace(specification))
            {
                commodity = commodity.Where(a=>a.Specification == specification);
            }

            if (!string.IsNullOrWhiteSpace(length))
            {
                commodity = commodity.Where(a => a.Length == Convert.ToInt32(length));
            }
            if(!string.IsNullOrWhiteSpace(height))
            {
                commodity = commodity.Where(a=>a.Height == Convert.ToDecimal(height));
            }
            if (!string.IsNullOrWhiteSpace(width)) 
            {
                commodity = commodity.Where(a => a.Width == Convert.ToInt32(width));
            }

            var result = await commodity.Where(a => a.IsDelete == false).OrderBy(a => a.Specification).OrderBy(a => a.Length).Select(a => new
            {
                a.Id,
                a.CommodityName,
                a.Specification,
                Length = a.Length.ToString(),
                Height = a.Height.ToString(),
                Width = a.Width.ToString(),
                a.Unit,
                a.Category.CategoryName,
                Count = _db.Inventories.Where(b => b.CommodityId == a.Id).Sum(a=>a.Count) - _db.StockoutRecords.Where(a => a.CommodityId == a.Id).Sum(a=>a.Count)
            }).ToListAsync();
            return result;
        }
    }
}
