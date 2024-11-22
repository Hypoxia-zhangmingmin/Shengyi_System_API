namespace Shengyi_WebAPI.Services.Inventory
{
    public interface IInventoryService
    {
        Task<object> CreateInventoryAsync(int unitId,int commodityId, int supplierId, int count, decimal UnitPrice, decimal TotalPrice);
        Task<object> EditInventoryAsync(int id, int count);
        Task<object> SearchInventoryAsync(int categoryId, string name, string specification, string width, string length, string height);
    }
}
