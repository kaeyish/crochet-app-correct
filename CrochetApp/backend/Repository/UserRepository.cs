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
            using (var connection = new OracleConnection(_connectionString)){
                OracleTransaction transaction = null;
                try
                {
                    connection.Open();
                    transaction = connection.BeginTransaction();
                    using (var command = new OracleCommand("INSERT INTO APPUSER VALUES (null, :userlevel, :useremail, :userpass, :username, :imageId, :userrole)", connection))
                    {
                        command.Transaction = transaction;
                        command.Parameters.Add(new OracleParameter("userlevel", level));
                        command.Parameters.Add(new OracleParameter("useremail", email));
                        command.Parameters.Add(new OracleParameter("userpass", pass));
                        command.Parameters.Add(new OracleParameter("username", username));
                        command.Parameters.Add(new OracleParameter("userimageId", imageId));
                        command.Parameters.Add(new OracleParameter("userrole", role));
                        command.ExecuteNonQuery();
                        transaction.Commit(); transaction?.Dispose();
                    }

                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    transaction?.Rollback(); transaction?.Dispose();
                }
            }
        }

        public void UpdateUser(string level, string password, string username, int imageId, string role, int id)
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                OracleTransaction transaction = null;
                try
                {
                    connection.Open();
                    transaction = connection.BeginTransaction();
                    using (var command = new OracleCommand("UPDATE APPUSER SET USERLVL= :ulevel, \"Password\" = :upassword, USERNAME = :uusername, IMAGEID = :iimage, \"Role\" = :urole WHERE APPUSERID = :userid", connection))
                    {
                        command.Transaction = transaction;
                        command.BindByName = true;
                        command.Parameters.Add("ulevel", level);
                        command.Parameters.Add("upassword", password);
                        command.Parameters.Add("uusername", username);
                        command.Parameters.Add("iimage", imageId);
                        command.Parameters.Add("urole", role);
                        command.Parameters.Add("userid", id);
                        command.ExecuteNonQuery();
                        transaction.Commit(); transaction?.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    transaction?.Rollback(); transaction?.Dispose();
                }
            }
        }

        public void DeleteUser(int id)
        {
            using (var connection = new OracleConnection(_connectionString)){
                OracleTransaction transaction = null;
                try
                {
                    connection.Open();
                    transaction = connection.BeginTransaction();
                    using (var command = new OracleCommand("DELETE FROM APPUSER WHERE APPUSERID = :id", connection))
                    {
                        command.Transaction = transaction;
                        command.Parameters.Add(new OracleParameter("id", id));
                        command.ExecuteNonQuery();
                        transaction.Commit(); transaction?.Dispose();
                    }

                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    transaction?.Rollback(); transaction?.Dispose();
                }
            }
        }

        public List<User> GetAllUsers()
        {
            return GetUsers("SELECT * FROM APPUSER", new Dictionary<string, object>());

        }

        public User GetById(int id)
        {
            return GetUsers("SELECT * FROM APPUSER WHERE APPUSERID = :id", new Dictionary<string, object> { { "id", id } }).FirstOrDefault();
        }

        public User GetByUsername(string name)
        {
            return GetUsers("SELECT * FROM APPUSER WHERE USERNAME = :name", new Dictionary<string, object> { { "name", name } }).FirstOrDefault();
        }


        List<User> IUserRepository.GetByLevel(string level) {
            return GetUsers("SELECT * FROM APPUSER WHERE USERLVL = :userlevel", new Dictionary<string, object> { { "userlevel", level} });
        }
        List<User> IUserRepository.GetByRole(string role) {
            return GetUsers("SELECT * FROM APPUSER WHERE ROLE= :userrole", new Dictionary<string, object> { { "userrole", role} });
        }



        public User GetByEmail(string email)
        {
            return GetUsers("SELECT * FROM APPUSER WHERE EMAIL = :email", new Dictionary<string, object> { { "email", email } }).FirstOrDefault();
        }


        public List<User> GetUsers(string query, Dictionary<string, object> parameters)
        {
            List<User> result = new();
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
                            while (reader.Read())
                            {
                                result.Add(new User(reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetInt32(5), reader.GetString(6), reader.GetInt32(0)));
                            }
                        }
                    }
                } catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
        }
            return result;
        }



    }
}
