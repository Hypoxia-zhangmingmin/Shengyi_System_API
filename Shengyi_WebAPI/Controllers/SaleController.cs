using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shengyi_WebAPI.Models.In;
using Shengyi_WebAPI.Models.Out;
using Shengyi_WebAPI.Services.Sales;

namespace Shengyi_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly ISaleService _saleService;

        public SaleController(ISaleService saleService)
        {
            _saleService = saleService;
        }
        [HttpPost("search")]
        public async Task<object> SearchCommodityAsync([FromBody] SearchSaleInfo info)
        {
            var result = await _saleService.SearchCommodityAsync(info.CommodityName, info.Specification, info.Width, info.Length, info.Height, info.CategoryId);
            return Ok(new OutputModel<object>().Success(result));
        }
    }
}
