using CrochetApp.backend.Domain.Model;
using CrochetApp.backend.Domain.RepositoryInterfaces;
using CrochetApp.backend.Repository;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrochetApp.backend.Service
{
    public class UserService
    {
        private IUserRepository _userRepository;
        public UserService(IUserRepository userRepository) {
            _userRepository = userRepository;
        }

        public List<User> GetAllUsers() {
            return _userRepository.GetAllUsers();
        }

        public User GetById(int id) {
           return  _userRepository.GetById(id);
        }
        public User GetByUsername(string username) {
            return _userRepository.GetByUsername(username);
        }

        public User GetByEmail(string email) {
            return _userRepository.GetByEmail(email);
        }

        public List<User> GetByLevel(string level) {
            return _userRepository.GetByLevel(level);
        }

        public List<User> GetByRole(string role) {
            return _userRepository.GetByRole(role);
        }

        public void UpdateUser(User user) {

            _userRepository.UpdateUser(Enum.GetName(typeof(Level), user.Level), user.Password, user.Username, user.ImageId, Enum.GetName(typeof(Role), user.Role), user.Id.Value);
        }

        public void DeleteUser(int id) {
            _userRepository.DeleteUser(id);
        }

        public void AddUser(User user) {

            _userRepository.AddUser(Enum.GetName(typeof(Level), user.Level), user.Email, user.Password, user.Username, user.ImageId, Enum.GetName(typeof(Role), user.Role));
        }


    }
}
