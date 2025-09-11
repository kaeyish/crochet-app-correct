using CrochetApp.backend.Domain.Model;
using CrochetApp.frontend.ViewModel;
using CrochetApp.frontend.View;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace CrochetApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

    private void ClassButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Content is string className)
            {
                Window window = className switch
                {
                    "User" => new UserWindow(),
                    "Pattern" => new PatternWindow(),
                    "Project" => new ProjectWindow(),
                    "Category" => new CategoryWindow(),
                    "Library" => new LibraryWindow(),
                    "Tutorial" => new TutorialWindow(),
                    //review
                    _ => null
                };

                window?.ShowDialog();
            }
        }
    }
}