using DemoTest.Utils;
using Shengyi_WebAPI.Daos.PaymentStatus;
using Shengyi_WebAPI.Models.Out;

namespace Shengyi_WebAPI.Services.PaymentStatus
{
    public class PaymentStatusService:IPaymentStatusService
    {
        private readonly IPaymentStatusDao _paymentStatusDao;
        private readonly RedisUtils _redisUtils;

        public PaymentStatusService(IPaymentStatusDao paymentStatusDao, RedisUtils redisUtils)
        {
            _paymentStatusDao = paymentStatusDao;
            _redisUtils = redisUtils;
        }

        public async Task<object> GetAll()
        {
            string key = "paymentStatus*";
            if (_redisUtils.IsSet(key))
            {
                var data = await _redisUtils.Get<List<OutPaymentStatusAllInfo>>(key);
                return data;
            }
            var result = await _paymentStatusDao.GetAll();
            await _redisUtils.Set(key, result);
            return result;
        }
    }
}
