using Microsoft.EntityFrameworkCore;
using Shengyi_WebAPI.EFCore;

namespace Shengyi_WebAPI.Daos.PaymentStatus
{
    public class PaymentStatusDao:IPaymentStatusDao
    {
        private readonly DB _db;
        public PaymentStatusDao(DB db)
        {
            _db = db;
        }

        public async Task<object> GetAll()
        {
            var result =await _db.PaymentStatuses.Select(a => new
            {
                a.Id,
                a.StatusName
            }).ToListAsync();
            return result;
        }
    }
}
