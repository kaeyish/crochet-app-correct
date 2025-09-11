using CrochetApp.frontend.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for AddYarnWindow.xaml
    /// </summary>
    public partial class AddYarnWindow : Window
    {

        private AddPatternVM _viewModel;

        public AddYarnWindow(AddPatternVM patternVM)
        {
            InitializeComponent();
            _viewModel = patternVM;
            DataContext = _viewModel;

        }

        private void SaveYarn_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.AddYarn(
                YarnNameBox.Text,
                TypeBox.Text,
                MaterialBox.Text,
                WeightBox.Text,
                MinSizeBox.Text,
                MaxSizeBox.Text,
                ColorBox.Text
                );
            _viewModel.ExistingYarns.Add(YarnNameBox.Text);
            _viewModel.OnPropertyChanged("ExistingYarns");
            _viewModel.SelectYarn(YarnNameBox.Text);
            this.Close();
        }
    }
}
