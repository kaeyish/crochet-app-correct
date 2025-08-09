using CrochetApp.backend.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CrochetApp.frontend.ViewModel
{

    public class CategoryVM : INotifyPropertyChanged 
    {
        private CategoryService _categoryService;

        public CategoryVM()
        {
            var app = (App)Application.Current;
            _categoryService = app.CategoryService;
            

            var stop = 1; //breakpoint for debugging



            //TESTED
            /*
            _categoryService.DeleteCategoryById(1);
            _categoryService.DeleteCategoryByName("trinkets");
            var testresult = _categoryService.GetAllCategories();
            _categoryService.AddCategory("testCategory");
            var testresult1 = _categoryService.GetCategoryById(2);
            var testresult2 = _categoryService.GetCategoryByName("blanket");S
            */
        }










        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string v)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(v));
        }

    }
}
