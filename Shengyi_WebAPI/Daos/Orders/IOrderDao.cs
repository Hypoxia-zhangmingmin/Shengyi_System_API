using Shengyi_WebAPI.Models.In;

namespace Shengyi_WebAPI.Daos.Order
{
    public interface IOrderDao
    {
    
        Task<object> CreateOrderAsync(int customerId, int paymentId, string address, int shippingCost, decimal total, string remark, List<AddItemInfo> items);
        Task<object> SearchOrderAsync(string customer, string start, string end,int currentPage);

        Task<object> SearchOrderDetailAsync(string orderCode);
    }
}
