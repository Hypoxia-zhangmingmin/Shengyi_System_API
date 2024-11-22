namespace Shengyi_WebAPI.Services.Suppliers
{
    public interface ISupplierService
    {
        Task<object> SearchSupplierAsync(string supplierName);
        Task<object> CreateSupplierAsync(string supplierName, string phone, string address);
        Task<object> EditSupplierAsync(int id, string supplierName, string phone, string address);
        Task<object> DeleteSupplierAsync(int id);
        Task<object> GetAll();
    }
}
