using Shengyi_WebAPI.EFCore;

namespace Shengyi_WebAPI.Services.User
{
    public interface IUserService
    {
        Task<Staff?> Login(string phone,string password);
    }
}
