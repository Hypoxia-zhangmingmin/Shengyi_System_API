namespace Shengyi_WebAPI.Daos.Commondity
{
    public interface ICommondityDao
    {
        Task<object> SearchCommondityInfo(int categoryId, string commondityName, string length, string width, string height, int currentpage, string specification);
        Task<object> GetCommondityCount(int categoryId, string commondityName, string length, string width, string height, int currentpage, string specification);
        Task<object> CreateCommonditySpec(int categoryId, string commondityName, string specification, string length, string width, string height, string weight, string description, string unit, int count,decimal nowTonPrice, decimal price);
        Task<object> CreateSales();
        Task<object> GetSales(int id);
        Task<object> EditCommodity(int id, string commondityName, string specification, string length, string width, string height, string weight, string description, string unit, int count, decimal nowTonPrice, decimal price);

        Task<object> DeleteCommodity(int id);
    }
}
