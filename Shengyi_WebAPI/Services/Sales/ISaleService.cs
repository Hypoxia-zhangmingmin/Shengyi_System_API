namespace Shengyi_WebAPI.Services.Sales
{
    public interface ISaleService
    {
        Task<object> SearchCommodityAsync(string name, string specification, string width, string length, string height, int categoryId);
    }
}
