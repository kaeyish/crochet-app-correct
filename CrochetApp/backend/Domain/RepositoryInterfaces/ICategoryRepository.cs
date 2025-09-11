using CrochetApp.backend.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrochetApp.backend.Domain.RepositoryInterfaces
{
    public interface ICategoryRepository
    {
        List<Category> GetAllCategories();

        Category GetCategoryById(int id);

        Category GetCategoryByName(string categoryName);

        int AddCategory(string categoryName);

        void DeleteCategoryById(int id);

        void DeleteCategoryByName(string categoryName);

        void UpdateCategory(int id, string newCategoryName);

        List<Pattern> GetPatternsByCategoryId(int categoryId);

        void ConnectToPattern(int patternId, int categoryId);

    }
}
