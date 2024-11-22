using Microsoft.AspNetCore.Mvc;
using Shengyi_WebAPI.Models.In;

namespace Shengyi_WebAPI.Daos.Invoices
{
    public interface IInvoicesDao
    {
        Task<object> SearchPurchaseAsync(string supplier, string start, string end,int currentpage);
        Task<object> SearchSaleAsync(string customer, string start, string end, int currentpage);

        Task<object> CreateSaleAsync(IFormFile file, int customerId, decimal totalPrice, decimal taxRate,decimal price,decimal tax,string date,string md5,string remark);

        Task<object> CreatePurchaseAsync(IFormFile file, int supplierId, decimal totalPrice, decimal taxRate, decimal price, decimal tax, string date, string md5, string remark);

        Task<FileDownloadResult> GetSaleFileAsync(int id);
        Task<FileDownloadResult> GetPurchaseFileAsync(int id);

    }
}
