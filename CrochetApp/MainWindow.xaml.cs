using CrochetApp.backend.Domain.Model;
using CrochetApp.frontend.ViewModel;
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
        private TagVM _viewmodel;
        private ImageVM _imageViewModel;
        private UserVM _userViewModel;
        private HookVM _hookVM;

        public MainWindow()
        {
            InitializeComponent();
            _viewmodel = new TagVM();
            _imageViewModel = new ImageVM();
            _userViewModel = new UserVM();
            _hookVM = new HookVM();
            DataContext = _viewmodel;
        }
    }
}