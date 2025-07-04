using CrochetApp.backend.Domain.RepositoryInterfaces;
using CrochetApp.backend.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Image = CrochetApp.backend.Domain.Model.Image;
using Oracle.ManagedDataAccess.Client;
using System.Diagnostics;

namespace CrochetApp.backend.Repository
{
    public class ImageRepository : Domain.RepositoryInterfaces.IImageRepository
    {

        private string _connectionString;

        public ImageRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Image> GetAllImages()
        {
           List<Image> images = new List<Image>();
           
            using (var connection = new Oracle.ManagedDataAccess.Client.OracleConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (var command = new Oracle.ManagedDataAccess.Client.OracleCommand("SELECT * FROM IMAGE", connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                images.Add(new Image(reader.GetInt32(0), reader.GetString(1)));
                                Debug.WriteLine($"Row: {reader.GetInt32(0)} - {reader.GetString(1)}");

                            }
                        }
                    }
                }
                catch (Oracle.ManagedDataAccess.Client.OracleException e) {
                    Debug.WriteLine($"Database error: {e.Message}");

                }
                catch (Exception e)
                {
                    Debug.WriteLine($"Other exceptionerror: {e.Message}");
                }
            }


            return images;
        }

        public Domain.Model.Image GetImageById(int id)
        {
            Image image = new Image();

            using (var connection = new Oracle.ManagedDataAccess.Client.OracleConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM IMAGE WHERE IMAGEID = :id";
                    using (var command = new Oracle.ManagedDataAccess.Client.OracleCommand(query, connection))
                    {
                        command.Parameters.Add(new Oracle.ManagedDataAccess.Client.OracleParameter("id", id));
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                image.Id = reader.GetInt32(0);
                                image.URL = reader.GetString(1);
                            }
                        }
                    }
                }
                catch (Oracle.ManagedDataAccess.Client.OracleException e) {
                    Debug.WriteLine($"Database error: {e.Message}");
                }
                catch (Exception e)
                {
                    Debug.WriteLine($"Other exceptionerror: {e.Message}");
                }
            }

            return image;
        }

        public Domain.Model.Image GetImageByURL(string url)
        {
            Image image = new Image();

            using (var connection = new Oracle.ManagedDataAccess.Client.OracleConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT IMAGEID, URL FROM IMAGE WHERE URL= :url";
                    using (var command = new Oracle.ManagedDataAccess.Client.OracleCommand(query, connection))
                    {
                        command.Parameters.Add(new Oracle.ManagedDataAccess.Client.OracleParameter("url", url));
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                image.Id = reader.GetInt32(0);
                                image.URL = reader.GetString(1);
                            }
                        }
                    }
                }
                catch (Oracle.ManagedDataAccess.Client.OracleException e)
                {
                    Debug.WriteLine($"Database error: {e.Message}");
                }
                catch (Exception e) {
                    Debug.WriteLine($"Other exceptionerror: {e.Message}");
                }
            }

            return image;
        }

        public void AddImage(string url)
        {
            using (var connection = new OracleConnection(_connectionString)) {
                try {
                    connection.Open();
                    string query = "INSERT INTO IMAGE VALUES (null, :url)";
                    using (var command = new OracleCommand(query, connection))
                    {
                        command.Parameters.Add(new OracleParameter("url", url));
                        command.ExecuteNonQuery();
                    }
                }
                catch (Oracle.ManagedDataAccess.Client.OracleException e)
                {
                    Debug.WriteLine($"Database error: {e.Message}");
                }
                catch (Exception e)
                {
                    Debug.WriteLine($"Other exceptionerror: {e.Message}");
                }

            }

        }
         

        public Image DeleteImage(int id)
        {
            Image deleted = GetImageById(id); // Get the image before deleting it

            if (deleted == null) {
                return null;
            }

            using (var connection = new OracleConnection(_connectionString)) {
                try {
                    connection.Open();
                    string query = "DELETE FROM IMAGE WHERE IMAGEID = :id";
                    using (var command = new OracleCommand(query, connection))
                    {
                        command.Parameters.Add(new OracleParameter("id", id));
                        command.ExecuteNonQuery();
                    }
                    return deleted; 
                }
                catch (Oracle.ManagedDataAccess.Client.OracleException e)
                {
                    Debug.WriteLine($"Database error: {e.Message}");
                    return null; 
                }
                catch (Exception e)
                {
                    Debug.WriteLine($"Other exceptionerror: {e.Message}");
                    return null;
                }

            }

        }

        public void UpdateImage(int id, string url)
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "UPDATE IMAGE SET URL = :url WHERE IMAGEID = :id";
                    using (var command = new OracleCommand(query, connection))
                    {
                        command.Parameters.Add(new OracleParameter("url", url));
                        command.Parameters.Add(new OracleParameter("id", id));
                        command.ExecuteNonQuery();
                    }
                }
                catch (Oracle.ManagedDataAccess.Client.OracleException e)
                {
                    Console.WriteLine($"Database error: {e.Message}");
                }
            }
        }
    }
}
