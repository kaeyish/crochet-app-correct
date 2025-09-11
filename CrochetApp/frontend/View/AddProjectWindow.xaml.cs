using CrochetApp.backend.Domain.Model;
using CrochetApp.backend.Service;
using CrochetApp.frontend.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Interaction logic for AddProjectWindow.xaml
    /// </summary>
    public partial class AddProjectWindow : Window
    {
        private readonly ProjectVM _projectVM;
        private readonly PatternService _patternService;
        private readonly int? _parentId;

        public ObservableCollection<Pattern> AvailablePatterns { get; set; } = new();

        public AddProjectWindow(ProjectVM vm, int? parentId)
        {
            InitializeComponent();

            var app = (App)Application.Current;
            _patternService = app.PatternService;

            _projectVM = vm;
            _parentId = parentId;

            DataContext = this;
            LoadPatterns();
        }

        private void LoadPatterns()
        {
            var patterns = _patternService.GetAllPatterns();
            AvailablePatterns = new ObservableCollection<Pattern>(patterns);
            OnPropertyChanged(nameof(AvailablePatterns));
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            var title = TitleBox.Text.Trim();
            var selectedPatterns = PatternListBox.SelectedItems.Cast<Pattern>().ToList();

            _projectVM.CreateProject(title, _parentId, selectedPatterns);
            this.Close();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
