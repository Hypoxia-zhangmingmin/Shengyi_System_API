using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shengyi_WebAPI.Models.In;
using Shengyi_WebAPI.Models.Out;
using Shengyi_WebAPI.Services.Commondity;

namespace Shengyi_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommodityController : ControllerBase
    {
        private readonly ICommondityService _commondityService;

        public CommodityController(ICommondityService commondityService)
        {
            _commondityService = commondityService;
        }

        [HttpPost("search")]
        public async Task<object> SearchCommondityInfo([FromBody] SearchCommondityInfo info)
        {
            var result = await _commondityService.SearchCommondityInfo(info.CategoryId, info.CommodityName, info.Length,info.Width,info.Height, info.CurrentPage,info.Specification);
            return Ok(new OutputModel<object>().Success(result));
        }
        [HttpPost("count")]
        public async Task<object> GetCommondityCount([FromBody] SearchCommondityInfo info)
        {
            var result = await _commondityService.GetCommondityCount(info.CategoryId, info.CommodityName, info.Length, info.Width, info.Height, info.CurrentPage, info.Specification);
            return Ok(new OutputModel<object>().Success(result));
            //((bool)result) == true ? Ok(new OutputModel<object>().Success(result)) : Ok(new OutputModel<object>().Failed());
        }

        [HttpPost("create")]
        public async Task<object> CreateCommodity([FromBody] CreateCommodityInfo info)
        {
            var result = await _commondityService.CreateCommonditySpec(info.CategoryId, info.CommodityName, info.Specification, info.Length, info.Width, info.Height,info.Weight, info.Description,info.Unit,info.Count,info.NowTonPrice,info.Price);
            return  ((bool)result) == true ? Ok(new OutputModel<object>().Success(result)) : Ok(new OutputModel<object>().Failed());
        }

        [HttpPost("sale")]
        public async Task<object> GetSale(int Id)
        {
            var result =await _commondityService.GetSales(Id);
            return Ok(new OutputModel<object>().Success(result));
        }

        [HttpPost("update")]
        public async Task<object> EditCommodity([FromBody] EditCommodityInfo info)
        {
            var result = await _commondityService.EditCommodity(info.Id, info.CommodityName, info.Specification, info.Length, info.Width, info.Height,info.Weight, info.Description, info.Unit, info.Count, info.NowTonPrice, info.SalePrice);
            return ((bool)result) == true ? Ok(new OutputModel<object>().Success(result)) : Ok(new OutputModel<object>().Failed());
        }

        [HttpPost("delete")]
        public async Task<object> DeleteCommodity([FromBody] DeleteInfo info)
        {
            var result = await _commondityService.DeleteCommodity(info.Id);
            return ((bool)result) == true ? Ok(new OutputModel<object>().Success(result)) : Ok(new OutputModel<object>().Failed());
        }
    }
}
