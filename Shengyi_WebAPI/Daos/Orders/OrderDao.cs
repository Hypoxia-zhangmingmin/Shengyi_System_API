using Microsoft.EntityFrameworkCore;
using Shengyi_WebAPI.EFCore;
using Shengyi_WebAPI.Models.In;

namespace Shengyi_WebAPI.Daos.Order
{
    public class OrderDao : IOrderDao
    {
        private readonly DB _db;
        public OrderDao(DB db)
        {
            _db = db;
        }

        public async Task<object> CreateOrderAsync(int customerId, int paymentId, string address, int shippingCost, decimal total, string remark, List<AddItemInfo> items)
        {
            int count = _db.Orders.Where(a => a.CreateTime.Date == DateTime.Now.Date).Count() + 1;
            EFCore.Order order = new EFCore.Order()
            {
                CustomerId = customerId,
                TotalPrice = total,
                ShippingCost = shippingCost,
                Address = address,
                StatusId = paymentId,
                Remark = remark,
                IsDelete = false,
                CreateTime = DateTime.Now,
                OrderCode = "SY" + DateTime.Now.ToString("yyyyMMdd") + count.ToString("000")
            };
            _db.Orders.Add(order);
            if (await _db.SaveChangesAsync() <= 0)
            {
                return false;
            }
            int num = 0;
            foreach (var item in items)
            {
                OrderDetail orderDetail = new OrderDetail()
                {
                    OrderCode = order.OrderCode,
                    TotalPrice = item.TotalPrice,
                    Count = item.Count,
                    CommodityId = item.CommodityId,
                    PresalePrice = _db.Sales.SingleOrDefault(a => a.Id == item.Id).SalePrice,
                    CreateTime = DateTime.Now,
                    IsDelete = false,
                    ActualSellingPrice = item.SalePrice
                };
                _db.OrderDetails.Add(orderDetail);
                _db.StockoutRecords.Add(new StockoutRecord()
                {
                    CommodityId = item.CommodityId,
                    StaffId = 1,
                    Count = item.Count,
                    CreateTime = DateTime.Now,
                    IsDelete = false
                });
                _db.Inventories.SingleOrDefault(a => a.CommodityId == item.CommodityId).Count = _db.Inventories.SingleOrDefault(a => a.CommodityId == item.CommodityId).Count - item.Count;
                num = await _db.SaveChangesAsync();
            }
            if (num >= 1)
            {
                return true;
            }
            else
            {
                _db.Orders.Remove(order);
                await _db.SaveChangesAsync();
                return false;
            }
        }

        public async Task<object> SearchOrderAsync(string customer, string start, string end, int currentPage)
        {
            var data = _db.Orders.AsQueryable();
            if (!string.IsNullOrWhiteSpace(start))
            {
                data = data.Where(a => a.CreateTime.Date >= Convert.ToDateTime(start).Date);
            }
            if (!string.IsNullOrWhiteSpace(end))
            {
                data = data.Where(a => a.CreateTime.Date <= Convert.ToDateTime(end).Date);
            }
            var result = await data.Where(a => (!string.IsNullOrEmpty(customer) || a.Customer.Name.Contains(customer)) && a.IsDelete == false).OrderByDescending(a => a.CreateTime).Skip((currentPage - 1) * 16).Take(16).Select(a => new
            {
                a.OrderCode,
                a.Customer.Name,
                a.Customer.Phone,
                a.Customer.Address,
                a.ShippingCost,
                a.TotalPrice,
                Date = a.CreateTime.ToString("yyyy-MM-dd")
            }).ToListAsync();
            return result;
        }

        public async Task<object> SearchOrderDetailAsync(string orderCode)
        {
            var result = await _db.OrderDetails.Where(a => a.OrderCode == orderCode && a.IsDelete == false).Select(a => new
            {
                a.Commodity.CategoryId,
                a.Commodity.CommodityName,
                a.Commodity.Specification,
                a.Commodity.Unit,
                Length = a.Commodity.Length.ToString(),
                Width = a.Commodity.Width.ToString(),
                Height = a.Commodity.Height.ToString(),
                a.Count,
                a.ActualSellingPrice,
                a.TotalPrice
            }).ToListAsync();
            return result;
        }
    }
}
