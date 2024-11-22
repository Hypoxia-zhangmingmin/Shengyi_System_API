using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shengyi_WebAPI.Models.In;
using Shengyi_WebAPI.Models.Out;
using Shengyi_WebAPI.Services.Storages;

namespace Shengyi_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StorageController : ControllerBase
    {
        private readonly IStorageService _storageService;

        public StorageController(IStorageService storageService)
        {
            _storageService = storageService;
        }
        [HttpPost("create")]
        public async Task<object> CreateStorageAsync([FromBody] CreateStorageInfo info)
        {
            var result = await _storageService.CreateStorageAsync(info.UnitId, info.CommodityId, info.SupplierId, info.Count,info.Weight, info.UnitPrice, info.TotalPrice);
            return ((bool)result) == true ? Ok(new OutputModel<object>().Success(result)) : Ok(new OutputModel<object>().Failed());
        }
        [HttpPost("search")]
        public async Task<object> SearchStorageAsync([FromBody] SearchStorageInfo info)
        {
            var result = await _storageService.SearchStorageAsync(info.CategoryId, info.Start, info.End);
            return Ok(new OutputModel<object>().Success(result));
        }
    }
}
