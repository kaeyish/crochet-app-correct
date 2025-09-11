using CrochetApp.backend.Domain.Model;
using CrochetApp.frontend.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Printing;
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
    /// Interaction logic for AddTutorialWindow.xaml
    /// </summary>
    public partial class AddTutorialWindow : Window
    {
        private readonly TutorialVM _vm;
        public ObservableCollection<Technique> AvailableTechniques { get; set; } = new();

        public AddTutorialWindow(TutorialVM vm)
        {
            InitializeComponent();
            DataContext = this;
            _vm = vm;

            var app = (App)Application.Current;
            var techniqueService = app.TechniqueService;
            AvailableTechniques = new ObservableCollection<Technique>(techniqueService.GetAllTechniques());
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            var title = TitleBox.Text.Trim();
            var difficulty = DifficultyBox.Text.Trim();
            var videoUrl = VideoBox.Text.Trim();
            var text = TextBox.Text.Trim();
            var selected = TechniqueListBox.SelectedItems.Cast<Technique>().ToList();

            _vm.CreateTutorial(title, difficulty, videoUrl, text, selected);
            this.Close();
        }

    }
}
