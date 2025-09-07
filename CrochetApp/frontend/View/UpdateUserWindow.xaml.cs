using CrochetApp.backend.Domain.Model;
using CrochetApp.backend.Service;
using CrochetApp.frontend.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
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
    /// Interaction logic for UpdateUserWindow.xaml
    /// </summary>
    public partial class UpdateUserWindow : Window
    {

        private UserVM _userVM => new();

        private readonly User _originalUser;

        public List<string> Roles { get; } = new() { "Admin", "Creator", "Regular" };
        public List<string> Levels { get; } = new() { "Beginner", "Advanced" };
        public string SelectedRole { get; set; }
        public string SelectedLevel { get; set; }
        public string ProfilePic { get; set; }

        public UpdateUserWindow(User user, string profilePic = "")
        {
            InitializeComponent();

            _originalUser = user;
            DataContext = _userVM;

            // Pre-fill fields
            UsernameBox.Text = user.Username;
            PasswordBox.Text = user.Password;
            ImageIdBox.Text = profilePic;
            SelectedRole = user.Role.ToString();
            SelectedLevel = user.Level.ToString();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            _originalUser.Level = (Level)Enum.Parse(typeof(Level), SelectedLevel, true);
            _originalUser.Password = PasswordBox.Text;
            _originalUser.Username = UsernameBox.Text;
            _originalUser.Role = (Role)Enum.Parse(typeof(Role), SelectedRole, true);

         _userVM.Save_Click(_originalUser, ImageIdBox.Text);
         this.Close();
        }
    }
}
