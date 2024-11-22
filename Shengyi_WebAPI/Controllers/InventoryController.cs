using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shengyi_WebAPI.Models.In;
using Shengyi_WebAPI.Models.Out;
using Shengyi_WebAPI.Services.Inventory;

namespace Shengyi_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _inventoryService;

        public InventoryController (IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }


        [HttpPost("update")]
        public async Task<object> EditInventoryAsync([FromBody] EditInventoryInfo info)
        {
            var result = await _inventoryService.EditInventoryAsync(info.Id,info.Count); 
            return ((bool)result) == true ? Ok(new OutputModel<object>().Success(result)) : Ok(new OutputModel<object>().Failed());
        }

        [HttpPost("Search")]
        public async Task<object> SearchInventoryAsync([FromBody] SearchInventoryInfo info)
        {
            var result = await _inventoryService.SearchInventoryAsync(info.CategoryId, info.Name, info.Specification, info.Width, info.Length, info.Height);
            return Ok(new OutputModel<object>().Success(result));
        }
    }
}
