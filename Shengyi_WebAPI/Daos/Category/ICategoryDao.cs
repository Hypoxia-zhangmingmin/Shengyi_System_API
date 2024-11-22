using System.Globalization;

namespace Shengyi_WebAPI.Daos.Category
{
    public interface ICategoryDao
    {
        Task<object> AddCategory(string name, string description);
        Task<object> SearchCategory(string? name,int currentPage);
        Task<object> EditCategory (int id,string name,string description);
        Task<object> GetCategoryCount(string? name);

        Task<object> DeleteCategory(int id);
        Task<object> GetCategory();   

    }
}
