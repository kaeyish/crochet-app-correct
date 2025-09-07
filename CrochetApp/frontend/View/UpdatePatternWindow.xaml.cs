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

        public UpdatePatternWindow(Pattern pattern)
        {

            InitializeComponent();
            DataContext = new PatternVM();
            TitleBox.Text = pattern.Title;
            DescBox.Text = pattern.Description;
            LevelBox.SelectedItem = pattern.Level.ToString();
            DateBox.SelectedDate = pattern.Date;
            RatingBox.Text = pattern.Rating.ToString();
            InstBox.Text = pattern.Instructions;
            StatusBox.SelectedItem = pattern.Status.ToString();
            _patternId = pattern.Id;
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
