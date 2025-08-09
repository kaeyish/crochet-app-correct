using CrochetApp.backend.Domain.Model;
using CrochetApp.backend.Domain.RepositoryInterfaces;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrochetApp.backend.Repository
{
    public class CategoryRepository : ICategoryRepository
    {

        private string _connectionString;

        public CategoryRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddCategory(string categoryName)
        {

            using (var connection = new OracleConnection(_connectionString)) {
                try {
                    connection.Open();
                    using (var command = new OracleCommand("INSERT INTO CATEGORY VALUES (null, :catname)", connection)) {
                        command.Parameters.Add(new OracleParameter("catname", categoryName));
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }

            }

        }

        public void DeleteCategoryById(int id)
        {
            using (var connection = new OracleConnection(_connectionString)) {
                try {
                    connection.Open();
                    using (var command = new OracleCommand("DELETE FROM CATEGORY WHERE CATEGORYID = :catid", connection)) {
                        command.Parameters.Add(new OracleParameter("catid", id));
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex) {
                    Debug.WriteLine(ex.Message);
                }
            
            }
        }

        public void DeleteCategoryByName(string categoryName)
        {
            using(var connection = new OracleConnection(_connectionString)) {
                try
                {
                    connection.Open();
                    using (var command = new OracleCommand("DELETE FROM CATEGORY WHERE CATEGORYNAME = :catname", connection))
                    {
                        command.Parameters.Add(new OracleParameter("catname", categoryName));
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }

            }
        }

        public List<Category> GetAllCategories()
        {
            List<Category> categories = new List<Category>();

            using (var connection = new OracleConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (var command = new OracleCommand("SELECT * FROM CATEGORY", connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id = reader.GetInt32(0);
                                string name = reader.GetString(1);
                                categories.Add(new Category(id, name));
                            }
                        }
                    }
                }
                catch (Exception ex) {
                    Debug.WriteLine(ex.Message);
                }
            }



            return categories;


        }

        public Category GetCategoryById(int id)
        {
            Category category = new Category();

            using (var connection = new OracleConnection(_connectionString)) {
                try
                {
                    connection.Open();
                    using (var command = new OracleCommand("SELECT * FROM CATEGORY WHERE CATEGORYID = :catid", connection)) {
                        command.Parameters.Add(new OracleParameter("catid", id));
                        using (var reader = command.ExecuteReader()) {
                            if (reader.Read())
                                category = new Category(reader.GetInt32(0), reader.GetString(1));
                        }
                    }
                }
                catch (Exception ex) {
                    Debug.WriteLine(ex.Message);
                }
            
            }
            return category;
        }

        public Category GetCategoryByName(string categoryName)
        {
            Category category = new Category();

            using (var connection = new OracleConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (var command = new OracleCommand("SELECT * FROM CATEGORY WHERE CATEGORYNAME = :catname", connection))
                    {
                        command.Parameters.Add(new OracleParameter("catname", categoryName));
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read()){
                                category = new Category(reader.GetInt32(0), reader.GetString(1));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }

            }
            return category;

        }
    }
}
