using CrochetApp.backend.Domain.Model;
using CrochetApp.backend.Domain.RepositoryInterfaces;
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

        public List<AppUser> GetAllUsers() {
            return _userRepository.GetAllUsers();
        }

        public AppUser GetById(int id) {
           return  _userRepository.GetById(id);
        }
        public AppUser GetByUsername(string username) {
            return _userRepository.GetByUsername(username);
        }

        public AppUser GetByEmail(string email) {
            return _userRepository.GetByEmail(email);
        }

        public void UpdateUser(string? level, string? email, string? password, string? username, int? imageId, string? role, int id) {

            AppUser og = GetById(id);

            if (level == null) {
                level = Enum.GetName(typeof(Level), og.Level);
            }

            if (email == null)
            {
                email = og.Email;
            }

            if (password == null)
            {
                password = og.Password;
            }

            if (username == null) {
                username = og.Username;
            }

            if (imageId == null)
            {
                imageId = og.ImageId;
            }

            if (role == null) {
                role = Enum.GetName(typeof(Role), og.Role);
            }

            _userRepository.UpdateUser(level, email, password, username, imageId.Value, role, id);
        }

        public void DeleteUser(int id) {
            _userRepository.DeleteUser(id);
        }

        public void AddUser(string level, string email, string password, string username, int imgid, string role) {

            _userRepository.AddUser(level, email, password, username, imgid, role);
        }


    }
}
