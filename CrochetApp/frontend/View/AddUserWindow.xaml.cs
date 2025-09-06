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
    /// Interaction logic for AddUserWindow.xaml
    /// </summary>
    public partial class AddUserWindow : Window
    {
        private readonly UserVM _userVM;

        public List<string> Roles { get; } = new() { "Admin", "Creator", "Regular" };
        public List<string> Levels { get; } = new() { "Beginner", "Advanced" };

        public string SelectedRole { get; set; } = "Regular";
        public string SelectedLevel { get; set; } = "Beginner";

        public AddUserWindow()
        {
            InitializeComponent();
            _userVM = new UserVM();
            DataContext = _userVM;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameBox.Text;
            string email = EmailBox.Text;
            string password = PasswordBox.Text;
            string imageUrl = ImageUrlBox.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please fill in all required fields.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var newUser = new User
            {
                Username = username,
                Email = email,
                Password = password,
                Role = (Role)Enum.Parse(typeof(Role), SelectedRole, true),
                Level = (Level)Enum.Parse(typeof(Level), SelectedLevel, true),
                ImageId = 0
            };

            _userVM.Add_Click(newUser, imageUrl);
            this.Close();

        }
    }
}
