namespace Shengyi_WebAPI.Daos.Customers
{
    public interface ICustomerDao
    {
        Task<object> SearchCustomerAsync(string name);
        Task<object> SearchCreditAsync();
        Task<object> CreateCustomerAsync(string name,string phone,string address,int creaditId);
        Task<object> EditCustomerAsync(int id ,string name,string phone,string address,int creaditId);

        Task<object> DeleteCustomerAsync(int id);

        Task<object> GetAll();
        Task<object> GetAddress(int id);

    }
}
