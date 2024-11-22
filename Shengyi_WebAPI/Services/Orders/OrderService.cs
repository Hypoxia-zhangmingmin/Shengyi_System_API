using DemoTest.Utils;
using Shengyi_WebAPI.Daos.Order;
using Shengyi_WebAPI.Models.In;

namespace Shengyi_WebAPI.Services.Orders
{
    public class OrderService:IOrderService
    {
        private readonly RedisUtils _redisUtils;

        private readonly IOrderDao _orderDao;


        public OrderService (IOrderDao orderDao, RedisUtils redisUtils)
        {
            _redisUtils = redisUtils;
            _orderDao = orderDao;
        }

        public async Task<object> CreateOrderAsync(int customerId, int paymentId, string address, int shippingCost, decimal total, string remark, List<AddItemInfo> items)
        {
            var result = await _orderDao.CreateOrderAsync(customerId, paymentId, address, shippingCost, total, remark, items);
            return result;
        }

        public async Task<object> SearchOrderAsync(string customer, string start, string end, int currentPage)
        {
            var result = await _orderDao.SearchOrderAsync(customer, start, end, currentPage);   
            return result;
        }

        public async Task<object> SearchOrderDetailAsync(string orderCode)
        {
            var result = await _orderDao.SearchOrderDetailAsync(orderCode);
            return result;
        }
    }
}
