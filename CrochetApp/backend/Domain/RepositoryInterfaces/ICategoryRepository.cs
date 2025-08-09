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

        void AddCategory(string categoryName);

        void DeleteCategoryById(int id);

        void DeleteCategoryByName(string categoryName);

//        bool CategoryExists(string categoryName);

    }
}
