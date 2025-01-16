using AmazonSimulatorApp.Data;
using AmazonSimulatorApp.Data.DTOs;

namespace AmazonSimulatorApp.Services
{
    public interface ICategoryService
    {
        int AddCategory(CategoryInputDTO categoryInput);
        void DeleteCategory(int id);
        List<CategoryOutputDTO> GetAllCategories();
        List<Category> GetAllCategoriesWithRelatedData();
        CategoryOutputDTO GetCategoryById(int id);
        Category GetCategoryByIdWithRelatedData(int id);
        int UpdateCategory(CategoryInputDTO categoryInput, int id);
    }
}