using CrochetApp.backend.Domain.Model;
using CrochetApp.backend.Service;
using CrochetApp.frontend.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Image = CrochetApp.backend.Domain.Model.Image;

namespace CrochetApp.frontend.ViewModel
{
    public class UserVM : INotifyPropertyChanged
    {
        private readonly UserService _userService;
        private readonly ImageService _imageService;

        public ICommand AddUserCommand { get; }
        public ICommand UpdateUserCommand { get; }
        public ICommand DeleteUserCommand { get; }


        public ObservableCollection<User> AllUsers { get; set; } = new();
        public ObservableCollection<User> FilteredUsers { get; set; } = new();

        public List<string> Roles { get; } = new() { "All", "Admin", "Creator", "Regular" };
        public List<string> Levels { get; } = new() { "All", "Beginner", "Advanced" };

        private string selectedRole = "All";
        public string SelectedRole
        {
            get => selectedRole;
            set
            {
                selectedRole = value;
                OnPropertyChanged(nameof(SelectedRole));
                ApplyFilter();
            }
        }

        private string selectedLevel = "All";
        public string SelectedLevel
        {
            get => selectedLevel;
            set
            {
                selectedLevel = value;
                OnPropertyChanged(nameof(SelectedLevel));
                ApplyFilter();
            }
        }

        private User selectedUser;
        public User SelectedUser
        {
            get => selectedUser;
            set
            {
                selectedUser = value;
                OnPropertyChanged(nameof(SelectedUser));
                LoadUserDetails();
                ((RelayCommand)UpdateUserCommand).RaiseCanExecuteChanged();
                ((RelayCommand)DeleteUserCommand).RaiseCanExecuteChanged();
            }
        }

        public string Username => SelectedUser?.Username ?? "";
        public string Email => SelectedUser?.Email ?? "";
        public string Role => SelectedUser?.Role.ToString() ?? "";
        public string UserLevel => SelectedUser?.Level.ToString() ?? "";
        public int Id => SelectedUser?.Id ?? 0;

        private string _profilePic;

        public string ProfilePic {
            get => _profilePic;
            set
            {
                _profilePic = value;
                OnPropertyChanged(nameof(ProfilePic));
            }
        }


        /// /////////////////////////////////////////// ///
        ///                 CONSTRUCTOR                 ///
        /// /////////////////////////////////////////// ///

        public UserVM()
        {
            var app = (App)Application.Current;
            _userService = app.UserService;
            _imageService = app.ImageService;
            AddUserCommand = new RelayCommand(ExecuteAddUser);
            UpdateUserCommand = new RelayCommand(ExecuteUpdateUser, CanModifyUser);
            DeleteUserCommand = new RelayCommand(ExecuteDeleteUser, CanModifyUser);

            LoadUsers();
        }

        private void LoadUsers()
        {
            var users = _userService.GetAllUsers();
            AllUsers.Clear();
            foreach (var user in users)
                AllUsers.Add(user);

            ApplyFilter();
        }

        private void ApplyFilter()
        {
            var filtered = AllUsers.Where(u =>
                (SelectedRole == "All" || u.Role.ToString() == SelectedRole) &&
                (SelectedLevel == "All" || u.Level.ToString() == SelectedLevel)).ToList();

            FilteredUsers.Clear();
            foreach (var user in filtered)
                FilteredUsers.Add(user);
        }

        private void LoadUserDetails()
        {
            if (SelectedUser == null) return;

            var fullUser = _userService.GetById(SelectedUser.Id.Value);
            selectedUser = fullUser;

            ProfilePic = _imageService.GetImageById(fullUser.ImageId).URL;

            OnPropertyChanged(nameof(Username));
            OnPropertyChanged(nameof(Email));
            OnPropertyChanged(nameof(Role));
            OnPropertyChanged(nameof(UserLevel));
            OnPropertyChanged(nameof(Id));
        }

        private void ExecuteAddUser()
        {
            var addWindow = new AddUserWindow();
            addWindow.ShowDialog();
            LoadUsers(); // Refresh after closing
        }

        private void ExecuteUpdateUser()
        {
            if (SelectedUser == null) return;
            var updateWindow = new UpdateUserWindow(SelectedUser);
            updateWindow.ShowDialog();
            LoadUsers(); // Refresh after closing
        }


        private void ExecuteDeleteUser()
        {
            if (SelectedUser == null) return;

            _userService.DeleteUser(SelectedUser.Id.Value);
            SelectedUser = null;
            LoadUsers();
        }

        public void Save_Click(User user, string imageUrl)
        {
            Image newImage = _imageService.GetImageByURL(imageUrl);
            if (newImage.Id != 0) {
                user.ImageId = newImage.Id;
            }
            else
            {
                _imageService.AddImage(imageUrl);
                var addedImage = _imageService.GetImageByURL(imageUrl);
                user.ImageId = addedImage.Id;
            }

            _userService.UpdateUser(user);

        }

        internal void Add_Click(User user, string imageUrl)
        {
            Image newImage = _imageService.GetImageByURL(imageUrl);
            if (newImage.Id != 0)
            {
                user.ImageId = newImage.Id;
            }
            else
            {
                _imageService.AddImage(imageUrl);
                var addedImage = _imageService.GetImageByURL(imageUrl);
                user.ImageId = addedImage.Id;
            }

            _userService.AddUser(user);
        }

        private bool CanModifyUser() => SelectedUser != null;

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
