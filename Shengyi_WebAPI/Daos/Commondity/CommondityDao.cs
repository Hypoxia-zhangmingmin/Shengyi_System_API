
using Microsoft.EntityFrameworkCore;
using Shengyi_WebAPI.Daos.Commondity;
using Shengyi_WebAPI.EFCore;
using Shengyi_WebAPI.Models.Out;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Shengyi_WebAPI.Daos.Commondity
{
    public class CommondityDao : ICommondityDao
    {
        private readonly DB _db;

        public CommondityDao(DB db)
        {
            _db = db;
        }

        public async Task<object> CreateCommonditySpec(int categoryId, string commondityName, string specification, string length, string width, string height, string weight, string description, string unit, int count, decimal nowTonPrice, decimal price)
        {
            Commodity com = new Commodity()
            {
                CategoryId = categoryId,
                CommodityName = commondityName,
                Description = description,
                Unit = unit,
                Count = count,
                CreateTime = DateTime.Now,
                Weight = Convert.ToDecimal(weight),
                IsDelete = false
            };
            if (!string.IsNullOrWhiteSpace(specification))
                com.Specification = specification;

            if (string.IsNullOrWhiteSpace(specification))
            {
                com.Length = Convert.ToInt32(length);
                com.Width = Convert.ToInt32(width);
                com.Height = Convert.ToDecimal(height);
            }
            _db.Commodities.Add(com);

            if (await _db.SaveChangesAsync() <= 0)
            {
                return false;
            }
            var newCommodityId = com.Id;

            Sale sale = new Sale()
            {
                CommodityId = newCommodityId,
                NowTonPrice = nowTonPrice,
                CreateTime = DateTime.Now,
                IsDelete = false
            };
            if (string.IsNullOrWhiteSpace(specification))
            {
                decimal? size_Price = ((com.Length * 2 + com.Width * 2) * com.Height * 0.785m * nowTonPrice);
                sale.SalePrice = Convert.ToDecimal(size_Price);
            }
            else
            {
                sale.SalePrice = price;
            }
            _db.Sales.Add(sale);
            if (await _db.SaveChangesAsync() <= 0)
            {
                _db.Commodities.Remove(com);
                return false;
            }
            else
            {
                return true;
            }
        }

        public async Task<object> CreateSales()
        {

            _db.Sales.Add(new Sale()
            {
                CommodityId = 18,
                NowTonPrice = 2532,
                SalePrice = 23,
                CreateTime = DateTime.Now,
                IsDelete = false
            });
            return await _db.SaveChangesAsync() >= 1;
        }

        public async Task<object> DeleteCommodity(int id)
        {
            var sale = await _db.Sales.SingleOrDefaultAsync(a => a.CommodityId == id);
            sale.IsDelete = true;
            sale.DeleteTime = DateTime.Now;
            await _db.SaveChangesAsync();
            var data = await _db.Commodities.SingleOrDefaultAsync(a => a.Id == id);
            data.IsDelete = true;
            data.DeleteTime = DateTime.Now;
            return await _db.SaveChangesAsync() >= 1;
        }

        public async Task<object> EditCommodity(int id, string commondityName, string specification, string length, string width, string height, string weight, string description, string unit, int count, decimal nowTonPrice, decimal price)
        {
            var com = await _db.Commodities.SingleOrDefaultAsync(a => a.Id == id);
            com.CommodityName = commondityName;
            com.Specification = specification;
            com.Length = Convert.ToInt32(length);
            com.Width = Convert.ToInt32(width);
            com.Height = Convert.ToDecimal(height);
            com.Description = description;
            com.Count = count;
            com.Weight = Convert.ToDecimal(weight);

            var sale = await _db.Sales.SingleOrDefaultAsync(a => a.CommodityId == id);

            sale.NowTonPrice = nowTonPrice;
            sale.SalePrice = price;
            return await _db.SaveChangesAsync() >= 1;
        }

        public async Task<object> GetCommondityCount(int categoryId, string commondityName, string length, string width, string height, int currentpage, string specification)
        {
            var commondity = _db.Commodities.AsQueryable();
            if (categoryId != -1)
                commondity = commondity.Where(a => a.CategoryId == categoryId);

            if (!string.IsNullOrWhiteSpace(commondityName))
                commondity = commondity.Where(a => a.CommodityName.Contains(commondityName));

            if (!string.IsNullOrWhiteSpace(specification))
            {
                commondity = commondity.Where(a => a.Specification == specification);
            }
            if (!string.IsNullOrWhiteSpace(length) && Convert.ToInt32(length) > 0)
            {
                commondity = commondity.Where(a => a.Length == Convert.ToInt32(length));
            }
            if (!string.IsNullOrWhiteSpace(width) && Convert.ToInt32(width) > 0)
            {
                commondity = commondity.Where(a => a.Width == Convert.ToInt32(width));
            }
            if (!string.IsNullOrWhiteSpace(height) && Convert.ToDecimal(height) > 0)
            {
                commondity = commondity.Where(a => a.Height == Convert.ToDecimal(height));
            }


            var result = await commondity.Where(a => a.IsDelete == false).OrderBy(a => a.Specification).ThenBy(a => a.Length).ToListAsync();
            return result.Count;
        }

        public async Task<object> GetSales(int id)
        {
            var result = await _db.Sales.FirstOrDefaultAsync(a => a.CommodityId == id);
            if (result == null)
            {
                return false;
            }
            var data = new OutSaleInfo()
            {
                NowTonPrice = result.NowTonPrice,
                Price = result.SalePrice
            };
            return data;
        }

        public async Task<object> SearchCommondityInfo(int categoryId, string commondityName, string length, string width, string height, int currentpage, string specification)
        {
            var commondity = _db.Commodities.AsQueryable();
            if (categoryId != -1)
                commondity = commondity.Where(a => a.CategoryId == categoryId);

            if (!string.IsNullOrWhiteSpace(commondityName))
                commondity = commondity.Where(a => a.CommodityName.Contains(commondityName));

            if (!string.IsNullOrWhiteSpace(specification))
            {
                commondity = commondity.Where(a => a.Specification == specification);
            }

            if (!string.IsNullOrWhiteSpace(length) && Convert.ToInt32(length) > 0)
            {
                commondity = commondity.Where(a => a.Length == Convert.ToInt32(length));
            }
            if (!string.IsNullOrWhiteSpace(width) && Convert.ToInt32(width) > 0)
            {
                commondity = commondity.Where(a => a.Width == Convert.ToInt32(width));
            }
            if (!string.IsNullOrWhiteSpace(height) && Convert.ToDecimal(height) > 0)
            {
                commondity = commondity.Where(a => a.Height == Convert.ToDecimal(height));
            }


            var result = await commondity.Where(a => a.IsDelete == false).OrderBy(a => a.Specification).ThenBy(a => a.Length).Skip((currentpage - 1) * 16).Take(16).Select(a => new
            {
                a.Id,
                a.CategoryId,
                a.CommodityName,
                a.Specification,
                Length = a.Length.ToString(),
                Width = a.Width.ToString(),
                Height = a.Height.ToString(),
                a.Weight,
                a.Category.CategoryName,
                a.Unit,
                a.Count,
                a.Description,
                a.Sales.SingleOrDefault(b => b.CommodityId == a.Id).NowTonPrice,
                a.Sales.SingleOrDefault(b => b.CommodityId == a.Id).SalePrice,
            }).ToListAsync();
            return result;
        }
    }
}
