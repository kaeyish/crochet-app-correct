using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrochetApp.backend.Domain.Model
{
    public class AppUser 
    {
        public int Id { get; set; }


        public Level Level { get; set; }


        public string Email { get; set; }


        public string Password { get; set; }


        public string Username { get; set; }

        public Role Role { get; set; }

        public Image ProfileImg { get; set; }

        public AppUser() { }

        public AppUser(int id, Level level, string email, string password, string username, Role role, Image image) {
            Id = id;
            Level = level;
            Email = email;
            Password = password;
            Username = username;
            Role = role;
            ProfileImg = image;
        }

    }
}
