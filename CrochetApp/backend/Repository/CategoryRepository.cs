using CrochetApp.backend.Domain.Model;
using CrochetApp.backend.Domain.RepositoryInterfaces;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Printing.IndexedProperties;
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

        public int AddCategory(string categoryName)
        {
            int returnId = -1;
            using (var connection = new OracleConnection(_connectionString)) {
                OracleTransaction transaction = null;
                try
                {
                    connection.Open();
                    transaction = connection.BeginTransaction();
                        using (var command = new OracleCommand("INSERT INTO CATEGORY VALUES (null, :catname) returning CATEGORYID into :retId", connection))
                        {
                            command.Transaction = transaction;
                            command.Parameters.Add(new OracleParameter("catname", categoryName));
                            command.Parameters.Add(new OracleParameter("retId", OracleDbType.Int32, System.Data.ParameterDirection.Output));
                            command.ExecuteNonQuery();
                            returnId = Convert.ToInt32(command.Parameters["retId"].Value.ToString());
                        transaction.Commit(); transaction?.Dispose(); transaction?.Dispose();
                        }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    transaction?.Rollback(); transaction?.Dispose();
                }
            }
            return returnId;
        }

        public void DeleteCategoryById(int id)
        {
            using (var connection = new OracleConnection(_connectionString)) {
                OracleTransaction transaction = null;
                try {
                    connection.Open();
                    transaction = connection.BeginTransaction();
                    using (var command = new OracleCommand("DELETE FROM CATEGORY WHERE CATEGORYID = :catid", connection)) {
                        command.Transaction = transaction;
                        command.Parameters.Add(new OracleParameter("catid", id));
                        command.ExecuteNonQuery();
                        transaction.Commit(); transaction?.Dispose(); transaction?.Dispose();
                    }
                }
                catch (Exception ex) {
                    transaction?.Rollback(); transaction?.Dispose();
                    Debug.WriteLine(ex.Message);
                }
            
            }
        }

        public void DeleteCategoryByName(string categoryName)
        {
            using(var connection = new OracleConnection(_connectionString)) {
                OracleTransaction transaction = null;   
                try
                {
                    connection.Open();
                    transaction = connection.BeginTransaction();
                    using (var command = new OracleCommand("DELETE FROM CATEGORY WHERE CATEGORYNAME = :catname", connection))
                    {
                        command.Transaction = transaction;
                        command.Parameters.Add(new OracleParameter("catname", categoryName));
                        command.ExecuteNonQuery();
                        transaction.Commit(); transaction?.Dispose(); 
                    }
                }
                catch (Exception ex)
                {
                    transaction?.Rollback(); transaction?.Dispose();
                    Debug.WriteLine(ex.Message);
                }

            }
        }

        public void UpdateCategory(int id, string newCategoryName) {
        
            using (var connection = new OracleConnection(_connectionString)) {
                OracleTransaction transaction = null;
                try
                {
                    connection.Open();
                    transaction = connection.BeginTransaction();
                    using (var command = new OracleCommand("UPDATE CATEGORY SET CATEGORYNAME = :newcatname WHERE CATEGORYID = :catid", connection))
                    {
                        command.Transaction = transaction;
                        command.Parameters.Add(new OracleParameter("newcatname", newCategoryName));
                        command.Parameters.Add(new OracleParameter("catid", id));
                        command.ExecuteNonQuery();
                        transaction.Commit(); transaction?.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    transaction?.Rollback(); transaction?.Dispose();
                    Debug.WriteLine(ex.Message);
                }
            }


        }

        public List<Pattern> GetPatternsByCategoryId(int categoryId) {
            List<Pattern> patterns = new List<Pattern>();

            using (var connection = new OracleConnection(_connectionString)) {
                try
                {
                    connection.Open();
                    using (var command = new OracleCommand("with Patterns as( select pattern_patternId as retId from fallsunder where category_categoryId = :catid ) select * from Patterns inner join pattern on retId = patternId", connection)) {
                        command.Parameters.Add(new OracleParameter("catid", categoryId));
                        using (var reader = command.ExecuteReader()) {
                            while (reader.Read()) {
                                patterns.Add(new Pattern(reader.GetInt32(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetDateTime(5), reader.GetFloat(6), reader.GetString(7), reader.GetString(8)));
                                int check = 1;
                            }
                        }
                    }
                }
                catch (Exception ex) {
                    Debug.WriteLine(ex.Message);
                }
            }
            return patterns;


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

        public void ConnectToPattern(int patternId, int categoryId)
        {
            using (var connection = new OracleConnection(_connectionString)) {
                OracleTransaction transaction = null;
                try
                {
                    connection.Open();
                    transaction = connection.BeginTransaction();
                    using (var command = new OracleCommand("INSERT INTO FALLSUNDER VALUES (:catid, :patid)", connection))
                    {
                        command.Transaction = transaction;
                        command.Parameters.Add(new OracleParameter("catid", categoryId));
                        command.Parameters.Add(new OracleParameter("patid", patternId));
                        command.ExecuteNonQuery();
                        transaction.Commit(); transaction?.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    transaction?.Rollback(); transaction?.Dispose();
                    Debug.WriteLine(ex.Message);
                }
            }
        }
    }
}
