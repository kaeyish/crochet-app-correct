using CrochetApp.backend.Domain.Model;
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
    /// Interaction logic for UpdateCategoryWindow.xaml
    /// </summary>
    public partial class UpdateCategoryWindow : Window
    {
        private int id;
        public UpdateCategoryWindow(Category SelectedCategory)
        {
            InitializeComponent();
            CategoryNameBox.Text = SelectedCategory.Name;
            id = SelectedCategory.Id;
            DataContext = new CategoryVM();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            var name = CategoryNameBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(name)) return;

            var app = (App)Application.Current;
            app.CategoryService.UpdateCategory(id, name);
            this.Close();
        }
    }
}
