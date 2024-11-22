
using Shengyi_WebAPI.Daos.Invoices;
using Shengyi_WebAPI.EFCore;
using Shengyi_WebAPI.Models.In;

namespace Shengyi_WebAPI.Services.Invoices
{
    public class InvoicesService : IInvoicesService
    {
        private readonly IInvoicesDao _invoicesDao;
        public InvoicesService (IInvoicesDao invoicesDao)
        {
            _invoicesDao = invoicesDao;
        }

        public async Task<object> CreatePurchaseAsync(IFormFile file, int supplierId, decimal totalPrice, decimal taxRate, decimal price, decimal tax, string date, string md5, string remark)
        {
            var result =await _invoicesDao.CreatePurchaseAsync(file, supplierId, totalPrice, taxRate, price, tax, date, md5, remark);
            return result;
        }

        public async Task<object> CreateSaleAsync(IFormFile file, int customerId, decimal totalPrice, decimal taxRate, decimal price, decimal tax, string date, string md5, string remark)
        {
            var result = await _invoicesDao.CreateSaleAsync(file, customerId, totalPrice, taxRate, price, tax, date,md5,remark);
            return result;
        }

        public async Task<FileDownloadResult> GetPurchaseFileAsync(int id)
        {
            var result = await _invoicesDao.GetPurchaseFileAsync(id);
            return result;
        }

        public async Task<FileDownloadResult> GetSaleFileAsync(int id)
        {
            var result =await _invoicesDao.GetSaleFileAsync(id);

            return result;
        }

        public async Task<object> SearchPurchaseAsync(string supplier, string start, string end, int currentpage)
        {
           var result = await _invoicesDao.SearchPurchaseAsync(supplier, start, end,currentpage);
            return result;
        }

        public async Task<object> SearchSaleAsync(string customer, string start, string end, int currentpage)
        {
            var result = await _invoicesDao.SearchSaleAsync(customer, start, end, currentpage);
            return result;
        }
    }
}
