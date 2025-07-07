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
        List<AppUser> GetAllUsers();

        AppUser GetById(int id);
        AppUser GetByUsername(string name);

        AppUser GetByEmail(string email);

        void UpdateUser(AppUser user);

        void DeleteUser(int id);

        void AddUser (string level, string email, string pass, string username, int imageId, string role);

    }
}
