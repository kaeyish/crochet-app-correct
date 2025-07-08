using CrochetApp.backend.Domain.Model;
using CrochetApp.backend.Domain.RepositoryInterfaces;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using System.Xml.Linq;

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
            using (var connection = new OracleConnection(_connectionString))
                try {
                    connection.Open();
                    using (var command = new OracleCommand("DELETE FROM APPUSER WHERE APPUSERID = :id", connection))
                    {
                        command.Parameters.Add(new OracleParameter("id", id));
                        command.ExecuteNonQuery();
                    }

                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
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

        public AppUser GetById(int id)
        {
            return GetSingle("SELECT * FROM APPUSER WHERE APPUSERID = :id", new Dictionary<string, object> { { "id", id } });
        }

        public AppUser GetByUsername(string name)
        {
            return GetSingle("SELECT * FROM APPUSER WHERE USERNAME = :name", new Dictionary<string, object> { { "name", name } });
        }

        public void UpdateUser(AppUser user)
        {
            throw new NotImplementedException();
        }


        public AppUser GetByEmail(string email)
        {
            return GetSingle("SELECT * FROM APPUSER WHERE EMAIL = :email", new Dictionary<string, object> { { "email", email } });
        }


        public AppUser GetSingle(string query, Dictionary<string, object> parameters)
        {
            AppUser result = new();
            using (var connection = new OracleConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (var command = new OracleCommand(query, connection))
                    {
                        foreach (var param in parameters)
                        {
                            command.Parameters.Add(new OracleParameter(param.Key, param.Value));
                        }

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                result = new AppUser(reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetInt32(5), reader.GetString(6), reader.GetInt32(0));

                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
            return result;
        }



    }
}
