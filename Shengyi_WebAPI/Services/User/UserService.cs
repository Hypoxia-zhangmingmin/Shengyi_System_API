using DemoTest.Utils;
using Shengyi_WebAPI.Daos.User;
using Shengyi_WebAPI.EFCore;

namespace Shengyi_WebAPI.Services.User
{
    public class UserService:IUserService
    {
        private readonly IUserDao _userDao;

        public UserService(IUserDao userDao)
        {
            _userDao = userDao;
        }   

        public async Task<Staff?> Login(string phone,string password)
        {
            var result = await _userDao.LoginAsync(phone, password);
            return result;
        }
    }
}
