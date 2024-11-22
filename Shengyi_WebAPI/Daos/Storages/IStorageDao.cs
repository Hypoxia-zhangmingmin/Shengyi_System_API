namespace Shengyi_WebAPI.Daos.Storages
{
    public interface IStorageDao
    {
        Task<object> CreateStorageAsync(int unitId, int commodityId, int supplierId, int count,decimal weight, decimal UnitPrice, decimal TotalPrice);
        Task<object> SearchStorageAsync(int categoryId, string start, string end);
    }
}
