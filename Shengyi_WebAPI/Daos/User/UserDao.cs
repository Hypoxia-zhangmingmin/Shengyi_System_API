using Microsoft.EntityFrameworkCore;
using Shengyi_WebAPI.EFCore;

namespace Shengyi_WebAPI.Daos.User
{
    public class UserDao : IUserDao
    {
        private readonly DB _db;

        public UserDao(DB db)
        {
            _db = db;
        }

        public async Task<Staff?> LoginAsync(string phone, string password)
        {

            var result = await _db.Staff.FirstOrDefaultAsync(a => a.Phone == phone && a.Password == password);

            return result == null ? null : result;

        }
    }
}
