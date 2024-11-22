using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shengyi_WebAPI.Models.In;
using Shengyi_WebAPI.Models.Out;
using Shengyi_WebAPI.Services.Customers;

namespace Shengyi_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public CustomerController (ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost("search")]
        public async Task<object> SearchCustomer([FromBody] SearchCustomerInfo info) {
            var result = await _customerService.SearchCustomerAsync(info.Name);
            return Ok(new OutputModel<object>().Success(result)); 
        }

        [HttpPost("credit")]
        public async Task<object> SearchCredit()
        {
            var result = await _customerService.SearchCreditAsync();
            return Ok(new OutputModel<object>().Success(result));
        }

        [HttpPost("create")]
        public async Task<object> CreateCustomer([FromBody] CreateCustomerInfo info)
        {
            var result = await _customerService.CreateCustomerAsync(info.Name, info.Phone, info.Address, info.CreditId);
            return ((bool)result)== true ? Ok(new OutputModel<object>().Success()):Ok(new OutputModel<object>().Failed());
        }

        [HttpPost("update")]
        public async Task<object> EditCustomer([FromBody] EditCustomerInfo info)
        {
            var result = await _customerService.EditCustomerAsync(info.Id, info.Name, info.Phone, info.Address, info.CreditId);
            return ((bool)result) == true ? Ok(new OutputModel<object>().Success()) : Ok(new OutputModel<object>().Failed());
        }

        [HttpPost("delete")]
        public async Task<object> DeleteCustomer([FromBody] DeleteInfo info)
        {
            var result = await _customerService.DeleteCustomerAsync(info.Id);
            return ((bool)result) == true ? Ok(new OutputModel<object>().Success()) : Ok(new OutputModel<object>().Failed());
        }
        [HttpGet("all")]
        public async Task<object> GetAll()
        {
            var result = await _customerService.GetAll();
            return Ok(new OutputModel<object>().Success(result));
        }
        [HttpPost("address")]
        public async Task<object> GetAddress(int Id) {
            var result = await _customerService.GetAddress(Id);
            return Ok(new OutputModel<object>().Success(result));
        }
    }
}
