using CrochetApp.backend.Domain.Model;
using CrochetApp.backend.Service;
using CrochetApp.frontend.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CrochetApp.frontend.View
{
    /// <summary>
    /// Interaction logic for ViewPatternsWindow.xaml
    /// </summary>
    public partial class ViewPatternsWindow : Window
    {
        private int id;

        private CategoryVM _categoryVM;

        public ViewPatternsWindow(Category category, CategoryVM categoryVM)
        {
            InitializeComponent();

            _categoryVM = categoryVM;
            id = category.Id;
            DataContext = _categoryVM;
        }
    }
}
