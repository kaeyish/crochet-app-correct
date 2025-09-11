using CrochetApp.backend.Domain.Model;
using CrochetApp.backend.Domain.RepositoryInterfaces;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrochetApp.backend.Repository
{
    public class LibraryRepository : ILibraryRepository
    {
        private readonly string _connectionString;

        public LibraryRepository(string connectionString) {
            _connectionString = connectionString;
        }

        public int AddLibrary(string name, string desc, string date, int user)
        {
            int newId = -1;
            using (var connection = new OracleConnection(_connectionString)) {
                OracleTransaction transaction = null;   
                try {
                    connection.Open();
                    transaction = connection.BeginTransaction();
                    using (var command = new OracleCommand("INSERT INTO LIBRARY VALUES (NULL, :libraryname, :librarydesc, :librarydate, :userid) RETURNING LIBRARYID INTO :newId", connection)) {
                        command.Transaction = transaction;
                        command.BindByName = true;
                        command.Parameters.Add("libraryname", name);
                        command.Parameters.Add("librarydesc", desc);
                        command.Parameters.Add("librarydate", date);
                        command.Parameters.Add("userid", user);
                        var outputIdParam = new OracleParameter("newId", OracleDbType.Int32) {
                            Direction = System.Data.ParameterDirection.Output
                        };
                        command.Parameters.Add(outputIdParam);
                        command.ExecuteNonQuery();
                        newId = Convert.ToInt32(outputIdParam.Value.ToString());
                        transaction.Commit(); transaction?.Dispose();
                    }
                }
                catch (Exception ex){
                    Debug.WriteLine(ex.Message);
                    transaction?.Rollback(); transaction?.Dispose();
                }
            }
            return newId;
        }

        public void DeleteLibrary(int id)
        {
            using (var connection = new OracleConnection(_connectionString)) {
                OracleTransaction transaction = null;
                try {
                    connection.Open();
                    transaction = connection.BeginTransaction();
                    using (var command = new OracleCommand("DELETE FROM LIBRARY WHERE LIBRARYID = :lid", connection)) {
                        command.Transaction = transaction;
                        command.Parameters.Add("lid", id);
                        command.ExecuteNonQuery();
                        transaction.Commit(); transaction?.Dispose();
                    }
                }
                catch (Exception ex) {
                    Debug.WriteLine(ex.Message);
                    transaction?.Rollback(); transaction?.Dispose();
                }
            }
        }

        public void ConnectPatternToLibrary(int patternId, int libraryId, int userId)
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                OracleTransaction transaction = null;
                try
                {
                    connection.Open();
                    transaction = connection.BeginTransaction();
                    using (var command = new OracleCommand("INSERT INTO consistsof VALUES (:patid, :libId, :userId)", connection))
                    {
                        command.Transaction = transaction;
                        command.BindByName = true;
                        command.Parameters.Add("patid", patternId);
                        command.Parameters.Add("libid", libraryId);
                        command.Parameters.Add("userId", userId);
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

        public List<Library> GetAllLibraries()
        {
            return GetLibraries("SELECT * FROM LIBRARY", new());
        }

        public Library GetLibraryById(int id)
        {
            return GetLibraries("SELECT * FROM LIBRARY WHERE LIBRARYID = :libid", new Dictionary<string, object> {{"libid", id }} ).FirstOrDefault();
        }
        public List<Library>GetLibraryByUser(int id)
        {
            return GetLibraries("SELECT * FROM LIBRARY WHERE APPUSERID = :userid", new Dictionary<string, object> {{ "userid", id }} );
        }

        public Library GetLibraryByName(string name)
        {
            return GetLibraries("SELECT * FROM LIBRARY WHERE LIBRARYNAME = :libname", new Dictionary<string, object> { { "libname", name } }).FirstOrDefault();
        }

        public void UpdateLibrary(int id, string name, string desc, string date)
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                OracleTransaction transaction = null;
                try
                {
                    connection.Open();
                    transaction = connection.BeginTransaction();
                    using (var command = new OracleCommand("UPDATE LIBRARY SET LIBRARYNAME = :libname, LIBRARYDESC = :libdesc, LIBRARYCREATEDATE = :ldate WHERE LIBRARYID = :lid", connection))
                    {
                        command.Transaction = transaction;
                        command.Parameters.Add("libname", name);
                        command.Parameters.Add("libdesc", desc);
                        command.Parameters.Add("ldate", date);
                        command.Parameters.Add("lid", id);
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


        private List<Library> GetLibraries(string query, Dictionary<string, object> parameters) {
            List<Library> result = new();
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
                                result.Add(new Library(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetDateTime(3),  reader.GetInt32(4)));
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
