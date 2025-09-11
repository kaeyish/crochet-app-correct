using CrochetApp.backend.Domain.Model;
using CrochetApp.frontend.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for AddLibraryWindow.xaml
    /// </summary>
    public partial class AddLibraryWindow : Window
    {
        private readonly LibraryVM _vm;

        public AddLibraryWindow(LibraryVM vm)
        {
            InitializeComponent();
            DataContext = this;
            _vm = vm;

            var app = (App)Application.Current;
            var patternService = app.PatternService;
            AvailablePatterns = new ObservableCollection<Pattern>(patternService.GetAllPatterns());
        }

        public ObservableCollection<Pattern> AvailablePatterns { get; set; }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            var name = NameBox.Text.Trim();
            var desc = DescBox.Text.Trim();
            var selected = PatternListBox.SelectedItems.Cast<Pattern>().ToList();

            _vm.CreateLibrary(name, desc, selected);
            this.Close();
        }

    }
}
