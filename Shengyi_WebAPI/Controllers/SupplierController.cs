using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shengyi_WebAPI.Models.In;
using Shengyi_WebAPI.Models.Out;
using Shengyi_WebAPI.Services.Suppliers;

namespace Shengyi_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierService _supplierService;

        public SupplierController (ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }
        [HttpPost("search")]
        public async Task<object> SearchSupplierAsync([FromBody] SearchSupplierInfo info)
        {
            var result =await _supplierService.SearchSupplierAsync(info.SupplierName);
            return Ok(new OutputModel<object>().Success(result));
        }
        [HttpPost("update")]
        public async Task<object> EditSupplierAsync([FromBody] EditSupplierInfo info)
        {
            var result = await _supplierService.EditSupplierAsync(info.Id, info.SupplierName,info.Phone,info.Address);
            return ((bool)result) == true ? Ok(new OutputModel<object>().Success(result)) : Ok(new OutputModel<object>().Failed());
        }

        [HttpPost("create")]
        public async Task<object> CreateSupplierAsync([FromBody] CreateSupplierInfo info)
        {
            var result = await _supplierService.CreateSupplierAsync( info.SupplierName, info.Phone, info.Address);
            return ((bool)result) == true ? Ok(new OutputModel<object>().Success(result)) : Ok(new OutputModel<object>().Failed());
        }

        [HttpPost("delete")]
        public async Task<object> DeleteSupplierAsync([FromBody] DeleteInfo info)
        {
            var result = await _supplierService.DeleteSupplierAsync(info.Id);
            return ((bool)result) == true ? Ok(new OutputModel<object>().Success(result)) : Ok(new OutputModel<object>().Failed());
        }
        [HttpGet("all")]
        public async Task<object> GetAll()
        {
            var result = await _supplierService.GetAll();
            return Ok(new OutputModel<object>().Success(result));
        }
    }
}
