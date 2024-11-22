using Shengyi_WebAPI.EFCore;

namespace Shengyi_WebAPI.Daos.User
{
    public interface IUserDao
    {
        Task<Staff?> LoginAsync(string phone,string password);
    }
}
