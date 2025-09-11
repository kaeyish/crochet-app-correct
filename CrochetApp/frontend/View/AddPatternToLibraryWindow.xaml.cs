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
    /// Interaction logic for AddPatternToLibraryWindow.xaml
    /// </summary>
    public partial class AddPatternToLibraryWindow : Window
    {
        private readonly LibraryVM _vm;
        private readonly int _libraryId;
        public ObservableCollection<Pattern> AvailablePatterns { get; set; } = new();

        public AddPatternToLibraryWindow(LibraryVM vm, int libraryId)
        {
            InitializeComponent();
            DataContext = this;
            _vm = vm;
            _libraryId = libraryId;

            var app = (App)Application.Current;
            var patternService = app.PatternService;
            AvailablePatterns = new ObservableCollection<Pattern>(patternService.GetAllPatterns());
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            var selected = PatternListBox.SelectedItems.Cast<Pattern>().ToList();
            _vm.LinkPatternsToLibrary(_libraryId, selected);
            this.Close();
        }
    }

}
