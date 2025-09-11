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
    /// Interaction logic for UpdatePatternWindow.xaml
    /// </summary>
    public partial class UpdatePatternWindow : Window
    {
        private int _patternId;

        public UpdatePatternWindow(PatternVM vm)
        {

            InitializeComponent();
            DataContext = vm;
            TitleBox.Text = vm.SelectedPattern.Title;
            DescBox.Text = vm.SelectedPattern.Description;
            LevelBox.SelectedItem = vm.SelectedPattern.Level.ToString();
            DateBox.SelectedDate = vm.SelectedPattern.Date;
            RatingBox.Text = vm.SelectedPattern.Rating.ToString();
            InstBox.Text = vm.SelectedPattern.Instructions;
            StatusBox.SelectedItem = vm.SelectedPattern.Status.ToString();
            _patternId = vm.SelectedPattern.Id;
        }


        private void Save_Click(object sender, RoutedEventArgs e)
        {
            var app = (App)Application.Current;
            var service = app.PatternService;

            service.UpdatePattern(_patternId,
                TitleBox.Text,
                DescBox.Text,
                LevelBox.SelectedItem.ToString(),
                DateBox.SelectedDate ?? DateTime.Now,
                double.TryParse(RatingBox.Text, System.Globalization.CultureInfo.InvariantCulture, out var rating) ? rating : 0,
                InstBox.Text,
                StatusBox.SelectedItem.ToString());
            this.Close();
        }


    }
}
