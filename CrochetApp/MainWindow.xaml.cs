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
        private TagVM _viewmodel;
        private ImageVM _imageViewModel;
        private UserVM _userViewModel;
        private HookVM _hookVM;
        private YarnVM _yarnVM; 
        private CategoryVM _categoryVM;
        private TechniqueVM _techniqueVM;
        private SuggestionVM _suggestionVM;
        private TutorialVM _tutorialVM;
        private LibraryVM _libraryVM;
        private RequestVM _requestVM;
        private PatternVM _patternVM;
        private ProjectVM _projectVM;

        public MainWindow()
        {
            InitializeComponent();
            _viewmodel = new TagVM();
            _imageViewModel = new ImageVM();
            _userViewModel = new UserVM();
            _hookVM = new HookVM();
            _yarnVM = new YarnVM();
            _categoryVM = new CategoryVM();
            _suggestionVM = new SuggestionVM();
            _techniqueVM = new TechniqueVM();
            _tutorialVM = new TutorialVM();
            _libraryVM = new LibraryVM();
            _requestVM = new RequestVM();
            _patternVM = new PatternVM();
            _projectVM = new ProjectVM();
            DataContext = _viewmodel;
        }

    private void ClassButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Content is string className)
            {
                Window window = className switch
                {
                    "User" => new UserWindow(),
                    "Pattern" => new PatternWindow(),
                    //        "Project" => new ProjectWindow(),
                    //        "Category" => new CategoryWindow(),
                    //        "Hook" => new HookWindow(),
                    //        "Image" => new ImageWindow(),
                    //        "Library" => new LibraryWindow(),
                    //        "Request" => new RequestWindow(),
                    //        "Suggestion" => new SuggestionWindow(),
                    //        "Tag" => new TagWindow(),
                    //        "Technique" => new TechniqueWindow(),
                    //        "Tutorial" => new TutorialWindow(),
                    //        "Yarn" => new YarnWindow(),
                    _ => null
                };

                window?.Show();
            }
        }
    }
}