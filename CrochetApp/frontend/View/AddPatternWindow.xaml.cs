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
        public AddPatternWindow()
        {
            InitializeComponent();
            DataContext = new PatternVM();

        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            var app = (App)Application.Current;
            var service = app.PatternService;

            service.AddPattern(
                TitleBox.Text,
                DescBox.Text,
                LevelBox.SelectedItem.ToString(),
                DateBox.SelectedDate ?? DateTime.Now,
                double.TryParse(RatingBox.Text, CultureInfo.InvariantCulture, out var rating) ? rating : 0,
                InstBox.Text,
                "Pending");
            this.Close();
        }


    }
}
