using CrochetApp.backend.Domain.Model;
using CrochetApp.backend.Domain.RepositoryInterfaces;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CrochetApp.backend.Repository
{
    public class UserRepository : IUserRepository
    {
        private string _connectionString;

        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddUser(string level, string email, string pass, string username, int imageId, string role)
        {
            using (var connection = new OracleConnection(_connectionString))
                try
                {
                    connection.Open();
                    using (var command = new OracleCommand("INSERT INTO APPUSER VALUES (null, :userlevel, :useremail, :userpass, :username, :imageId, :userrole)", connection))
                    {
                        command.Parameters.Add(new OracleParameter("userlevel", level));
                        command.Parameters.Add(new OracleParameter("useremail", email));
                        command.Parameters.Add(new OracleParameter("userpass", pass));
                        command.Parameters.Add(new OracleParameter("username", username));
                        command.Parameters.Add(new OracleParameter("userimageId", imageId));
                        command.Parameters.Add(new OracleParameter("userrole", role));
                        command.ExecuteNonQuery();
                    }

                }
                catch (Exception ex) {
                    Debug.WriteLine(ex.Message);
                }





        }

        public void DeleteUser(int id)
        {
            throw new NotImplementedException();
        }

        public List<AppUser> GetAllUsers()
        {
            List<AppUser> users = new List<AppUser>();

            using (var connection = new OracleConnection(_connectionString))
                try
                {
                    connection.Open();
                    using (var command = new OracleCommand("SELECT * FROM APPUSER", connection))
                    {
                        using (var reader = command.ExecuteReader()) {
                            while (reader.Read()) {
                                users.Add(new AppUser(reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetInt32(5), reader.GetString(6), reader.GetInt32(0)));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            return users;

        }

        public AppUser GetByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public AppUser GetById(int id)
        {
            throw new NotImplementedException();
        }

        public AppUser GetByUsername(string name)
        {
            throw new NotImplementedException();
        }

        public void UpdateUser(AppUser user)
        {
            throw new NotImplementedException();
        }
    }
}
