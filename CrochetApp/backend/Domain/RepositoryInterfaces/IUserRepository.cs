using CrochetApp.backend.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrochetApp.backend.Domain.RepositoryInterfaces
{
    public interface IUserRepository
    {
        List<User> GetAllUsers();
        User GetById(int id);
        User GetByUsername(string name);

        User GetByEmail(string email);

        List<User>GetByLevel(string level);
        List<User>GetByRole(string roles);
        void UpdateUser(string level, string password, string username, int imageId, string role, int id);
        void DeleteUser(int id);
        void AddUser (string level, string email, string pass, string username, int imageId, string role);

    }
}
