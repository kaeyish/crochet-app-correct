using CrochetApp.backend.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CrochetApp.frontend.ViewModel
{
    public class UserVM : INotifyPropertyChanged
    {
        private UserService _userService;

        public UserVM() {
            var app = (App)Application.Current;
            _userService = app.UserService;
            //var testresult1 =_userService.GetByUsername;
            //var testresult2 =_userService.GetByEmail;
            //var testresult3 =_userService.GetById;
            //_userService.DeleteUser(1);
            _userService.AddUser("Beginner", "nekimejl@gmail.com", "sifra", "korisnik123", 1, "Regular");
            var testresult = _userService.GetAllUsers();
            var breakpoint = 1;
        }




        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string v)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(v));
        }


    }
}
