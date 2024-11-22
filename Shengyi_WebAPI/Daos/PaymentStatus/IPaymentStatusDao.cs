namespace Shengyi_WebAPI.Daos.PaymentStatus
{
    public interface IPaymentStatusDao
    {
        Task<object> GetAll();
    }
}
