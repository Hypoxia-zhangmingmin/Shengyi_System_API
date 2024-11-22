namespace Shengyi_WebAPI.Services.Storages
{
    public interface IStorageService
    {
        Task<object> CreateStorageAsync(int unitId, int commodityId, int supplierId, int count, decimal weight, decimal UnitPrice, decimal TotalPrice);
        Task<object> SearchStorageAsync(int categoryId, string start, string end);
    }
}
