using CrochetApp.backend.Domain.Model;
using CrochetApp.backend.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrochetApp.backend.Service
{
    public class CategoryService
    {

        private ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository) {
            _categoryRepository = categoryRepository;
        }

        public int AddCategory(string categoryName)
        {
            return _categoryRepository.AddCategory(categoryName.ToLower());
        }

        public void DeleteCategoryById(int id)
        {
            _categoryRepository.DeleteCategoryById(id);
        }

        public void DeleteCategoryByName(string categoryName)
        {
            _categoryRepository.DeleteCategoryByName(categoryName.ToLower());
        }

        public List<Category> GetAllCategories()
        {
            return _categoryRepository.GetAllCategories();
        }

        public Category GetCategoryById(int id)
        {
            return _categoryRepository.GetCategoryById(id);
        }

        public Category GetCategoryByName(string categoryName)
        {
            return _categoryRepository.GetCategoryByName(categoryName.ToLower());
        }

        public void UpdateCategory(int id, string newCategoryName)
        {
            _categoryRepository.UpdateCategory(id, newCategoryName.ToLower());
        }

        public List<Pattern> GetPatternsByCategoryId(int categoryId)
        {
            return _categoryRepository.GetPatternsByCategoryId(categoryId);
        }

        public void ConnectToPattern(int patternId, int categoryId)
        {
            _categoryRepository.ConnectToPattern(patternId, categoryId);
        }
    }
}
