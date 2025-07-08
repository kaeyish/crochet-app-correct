using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrochetApp.backend.Domain.Model
{
    public class AppUser 
    {
        public int? Id { get; set; }


        public Level Level { get; set; }


        public string Email { get; set; }


        public string Password { get; set; }


        public string Username { get; set; }

        public Role Role { get; set; }

        public int ImageId { get; set; }

        public AppUser() { }

        public AppUser(string level, string email, string password, string username, int imageId, string role, int? id = null) {
            Id = id;
            Level = (Level)Enum.Parse(typeof(Level), level, true);
            Email = email;
            Password = password;
            Username = username;
            Role = (Role)Enum.Parse(typeof(Role), role, true);
            ImageId = imageId;
        }
        
    }
}
