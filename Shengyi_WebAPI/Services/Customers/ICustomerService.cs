namespace Shengyi_WebAPI.Services.Customers
{
    public interface ICustomerService
    {
        Task<object> SearchCustomerAsync(string name);
        Task<object> SearchCreditAsync();

        Task<object> CreateCustomerAsync(string name, string phone, string address, int creaditId);

        Task<object> EditCustomerAsync(int id, string name, string phone, string address, int creaditId);

        Task<object> DeleteCustomerAsync(int id);
        Task<object> GetAll();
        Task<object> GetAddress(int id);
    }
}
