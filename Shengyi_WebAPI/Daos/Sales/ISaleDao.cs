namespace Shengyi_WebAPI.Daos.Sales
{
    public interface ISaleDao
    {
        Task<object> GetAllCategoryAsync();
        Task<object> SearchCommodityAsync(string name, string specification, string width, string length, string height,int categoryId);

    }
}
