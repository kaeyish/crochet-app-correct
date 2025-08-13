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

        public void AddLibrary(string name, string desc, string date, int user)
        {
            using (var connection = new OracleConnection(_connectionString)) {
                try {
                    connection.Open();

                    using (var command = new OracleCommand("INSERT INTO LIBRARY VALUES (NULL, :libraryname, :librarydesc, :librarydate, :userid)", connection)) {
                        command.Parameters.Add("libraryname", name);
                        command.Parameters.Add("librarydesc", desc);
                        command.Parameters.Add("librarydate", date);
                        command.Parameters.Add("userid", user);
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex){
                    Debug.WriteLine(ex.Message);
                }
            }
        }

        public void DeleteLibrary(int id)
        {
            using (var connection = new OracleConnection(_connectionString)) {
                try {
                    connection.Open();

                    using (var command = new OracleCommand("DELETE FROM LIBRARY WHERE LIBRARYID = :lid", connection)) {
                        command.Parameters.Add("lid", id);

                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex) {
                    Debug.WriteLine(ex.Message);
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

        public Library GetLibraryByName(string name)
        {
            return GetLibraries("SELECT * FROM LIBRARY WHERE LIBRARYNAME = :libname", new Dictionary<string, object> { { "libname", name } }).FirstOrDefault();
        }

        public void UpdateLibrary(int id, string name, string desc, string date)
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                try
                {
                    connection.Open();

                    using (var command = new OracleCommand("UPDATE LIBRARY SET LIBRARYNAME = :libname, LIBRARYDESC = :libdesc, LIBRARYCREATEDATE = :ldate WHERE LIBRARYID = :lid", connection))
                    {
                        command.Parameters.Add("libname", name);
                        command.Parameters.Add("libdesc", desc);
                        command.Parameters.Add("ldate", date);
                        command.Parameters.Add("lid", id);
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
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
