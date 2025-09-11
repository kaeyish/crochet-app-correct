using CrochetApp.frontend.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Interaction logic for AddPatternWindow.xaml
    /// </summary>
    public partial class AddPatternWindow : Window
    {
        private AddPatternVM _addPatternVM;


        public AddPatternWindow()
        {
            InitializeComponent();
            _addPatternVM = new AddPatternVM();
            DataContext = _addPatternVM;
        }


        private void Save_Click(object sender, RoutedEventArgs e)
        {
            _addPatternVM.SavePattern(
                TitleBox.Text,
                DescBox.Text,
                LevelBox.SelectedItem.ToString(),
                DateBox.SelectedDate ?? DateTime.Now,
                InstBox.Text);
            this.Close();
        }

        private void AddCategory_Click(object sender, RoutedEventArgs e)
        {
            _addPatternVM.AddCategory(CategoryInputBox.Text);
        }
        
        private void AddHook_Click(object sender, RoutedEventArgs e)
        {
            _addPatternVM.AddHook(HookInputBox.Text);
        }
        private void AddTag_Click(object sender, RoutedEventArgs e)
        {
            _addPatternVM.AddTag(TagInputBox.Text);
        }
        private void SelectYarn_Click(object sender, RoutedEventArgs e)
        {
            _addPatternVM.SelectYarn(YarnCombobox.Text);
        }

        private void OpenAddYarnWindow_Click(object sender, RoutedEventArgs e)
        {
            _addPatternVM.OpenAddYarnWindow();
        }

        
        


    }
}
