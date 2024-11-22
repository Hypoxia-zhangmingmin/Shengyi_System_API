namespace Shengyi_WebAPI.Services.Category
{
    public interface ICategoryService
    {
        Task<object> AddCategory(string name,string description);

        Task<object> SearchCategory(string? name, int currentPage);
        Task<object> EditCategory(int id,string name,string description);

        Task<object> GetCategoryCount(string? name);

        Task<object> DeleteCategory(int id);

        Task<object> GetCategory();

    }
}
