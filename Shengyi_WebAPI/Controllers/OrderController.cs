using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shengyi_WebAPI.Models.In;
using Shengyi_WebAPI.Models.Out;
using Shengyi_WebAPI.Services.Orders;

namespace Shengyi_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("create")]
        public async Task<object> CreateOrderAsync([FromBody] CreateOrderInfo info)
        {
            var result =await _orderService.CreateOrderAsync(info.CustomerId, info.PaymentId, info.Address, info.ShippingCost, info.Total,info.Remark,info.Items);
            return ((bool)result) == true ? Ok(new OutputModel<object>().Success(result)): Ok(new OutputModel<object>().Failed());
        }

        [HttpPost("search/order")]
        public async Task<object> SearchOrderAsync([FromBody] SearchOrderInfo info)
        {
            var result = await _orderService.SearchOrderAsync(info.Customer, info.Start, info.End, info.CurrentPage);
            return Ok(new OutputModel<object>().Success(result));
        }
        [HttpPost("search/orderdetail")]
        public async Task<object> SearchOrderDetailAsync([FromBody] SearchOrderDetailInfo info)
        {
            var result = await _orderService.SearchOrderDetailAsync(info.OrderCode);
            return Ok(new OutputModel<object>().Success(result));
        }
    }
}
