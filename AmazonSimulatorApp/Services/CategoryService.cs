using AmazonSimulatorApp.Data;
using AmazonSimulatorApp.Data.DTOs;
using AmazonSimulatorApp.Data.Repositories;
using System.Diagnostics;

namespace AmazonSimulatorApp.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public List<CategoryOutputDTO> GetAllCategories()
        {
            var categories = _categoryRepository.GetAllCategories().ToList();

            if (categories == null || categories.Count < 1)
            {
                throw new InvalidOperationException("No categories available");
            }

            List<CategoryOutputDTO> categoriesDTOs = new List<CategoryOutputDTO>();

            foreach (var category in categories)
            {
                categoriesDTOs.Add(new CategoryOutputDTO
                {
                    Id = category.CatID,
                    CategoryName = category.Name,
                    Count = category.Count
                });
            }

            return categoriesDTOs;
        }

        // Retrieve all categories along with products of each category
        public List<Category> GetAllCategoriesWithRelatedData()
        {
            var categories = _categoryRepository.GetAllCategories().ToList();

            if (categories == null || categories.Count < 1)
            {
                throw new InvalidOperationException("No categories available");
            }

            return categories;
        }

        public CategoryOutputDTO GetCategoryById(int id)
        {
            var category = _categoryRepository.GetCategoryById(id);

            if (category == null)
            {
                throw new KeyNotFoundException("Could not find category");
            }

            var categoryOutput = new CategoryOutputDTO
            {
                Id = category.CatID,
                CategoryName = category.Name,
                Count = category.Count
            };

            return categoryOutput;
        }

        // Retrieve category along with its products.
        public Category GetCategoryByIdWithRelatedData(int id)
        {
            var category = _categoryRepository.GetCategoryById(id);

            if (category == null)
            {
                throw new KeyNotFoundException("Could not find category");
            }

            return category;
        }

        public int AddCategory(CategoryInputDTO categoryInput)
        {
            if (string.IsNullOrWhiteSpace(categoryInput.CategoryName))
            {
                throw new ArgumentNullException("Category name cannot be empty");
            }

            var category = new Category
            {
                Name = categoryInput.CategoryName,
                Count = 0,
            };

            return _categoryRepository.AddCategory(category);
        }

        public int UpdateCategory(CategoryInputDTO categoryInput, int id)
        {
            if (string.IsNullOrWhiteSpace(categoryInput.CategoryName))
            {
                throw new ArgumentNullException("Category name cannot be empty");
            }

            var category = GetCategoryByIdWithRelatedData(id);

            category.Name = categoryInput.CategoryName;

            return _categoryRepository.UpdateCategory(category);
        }

        public void DeleteCategory(int id)
        {
            var category = GetCategoryByIdWithRelatedData(id);
            if (category.Products.Count > 0)
            {
                throw new InvalidOperationException("Cannot delete category that contains products");
            }
            _categoryRepository.DeleteCategory(category);
        }
    }
}
