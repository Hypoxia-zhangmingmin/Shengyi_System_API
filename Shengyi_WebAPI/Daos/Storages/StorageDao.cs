
using Microsoft.EntityFrameworkCore;
using Shengyi_WebAPI.EFCore;

namespace Shengyi_WebAPI.Daos.Storages
{
    public class StorageDao : IStorageDao
    {
        private readonly DB _db;

        public StorageDao(DB db)
        {
            _db = db;
        }
        public async Task<object> CreateStorageAsync(int unitId, int commodityId, int supplierId, int count,decimal weight, decimal UnitPrice, decimal TotalPrice)
        {
            int Icount = 0;
            var commodity = await _db.Commodities.SingleOrDefaultAsync(a => a.Id == commodityId);
            if (commodity == null)
            {
                return false;
            }
            if (unitId == 1)
            {
                Icount = count;
            }
            else
            {
                Icount = commodity.Count * count;
            }

            var num = _db.StorageRecords.Where(a => a.Id == commodityId && a.CreateTime.Date == DateTime.Now.Date).Count();
            var storage = new StorageRecord()
            {
                CommodityId = commodityId,
                SupplierId = supplierId,
                Weight = weight,
                UnitPrice = UnitPrice,
                TotalPrice = TotalPrice,
                CreateTime = DateTime.Now,
                StaffId = 1,
                IsDelete = false,
                Count = Icount,
                BatchNumber = "SY-IN" + DateTime.Now.ToString("yyyyMMdd") + commodityId + (num + 1).ToString("00")
            };
            _db.StorageRecords.Add(storage);
            if (await _db.SaveChangesAsync() <= 0)
            {
                return false;
            }
            var inventory = await _db.Inventories.SingleOrDefaultAsync(a => a.CommodityId == commodityId);
            if (inventory != null)
            {
                inventory.Count = inventory.Count +  count;
            }
            else
            {
                EFCore.Inventory new_inventory = new EFCore.Inventory()
                {
                    Count = Icount,
                    BatchStatusId = 1,
                    CreateTime = DateTime.Now,
                    IsDelete = false,
                    CommodityId = commodityId
                };
                _db.Inventories.Add(new_inventory);
            }

            if( await _db.SaveChangesAsync() <= 0)
            {
                return false;
            }

            _db.Sales.SingleOrDefault(a => a.CommodityId == commodityId).NowTonPrice = UnitPrice;

            return await _db.SaveChangesAsync() >=1;
        }

        public async Task<object> SearchStorageAsync(int categoryId, string start, string end)
        {
            var data = _db.StorageRecords.AsQueryable();
            if(categoryId != -1)
            {
                data = data.Where(a=>a.Commodity.CategoryId == categoryId);
            }
            if(!string.IsNullOrWhiteSpace(start))
            {
                data = data.Where(a=>a.CreateTime.Date >= Convert.ToDateTime(start).Date);
            }
            if(!string.IsNullOrWhiteSpace(end))
            {
                data = data.Where(a=>a.CreateTime.Date <= Convert.ToDateTime(end).Date);
            }
            var result = await data.Where(a => a.IsDelete == false).Select(a => new
            {
                a.Id,
                a.Commodity.CommodityName,
                a.Commodity.Specification,
                Length = a.Commodity.Length.ToString(),
                Width =  a.Commodity.Width.ToString(),
                Height = a.Commodity.Height.ToString(),
                UnitPrice = a.UnitPrice.ToString(),
                a.Commodity.Category.CategoryName,
                a.Count,
                Staff = a.Staff.Name,
                Date = a.CreateTime.ToString("yyyy-MM-dd"),
                Diff =(_db.Sales.SingleOrDefault(b => b.CommodityId == a.CommodityId).NowTonPrice - a.UnitPrice).ToString(),
                SinglePrice = (a.TotalPrice / a.Count).ToString("0.00")
            }).ToListAsync();
            return result;
        }
    }
}
