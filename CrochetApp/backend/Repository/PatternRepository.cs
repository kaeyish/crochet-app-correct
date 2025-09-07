using CrochetApp.backend.Domain.Model;
using CrochetApp.backend.Domain.RepositoryInterfaces;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrochetApp.backend.Repository
{
    public class PatternRepository : IPatternRepository
    {

        private readonly string _connectionString;
        public PatternRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public void AddPattern(string title, string desc, string level, string date, double rating, string inst, string status)
        {
            using (var connection = new OracleConnection(_connectionString)) {
                OracleTransaction transaction = null;
                try
                {
                    connection.Open();
                    transaction = connection.BeginTransaction();
                    using(var command = new OracleCommand("INSERT INTO PATTERN VALUES (null, :ptitle, :pdesc, :plevel, :pdate, :prating, :pinst, :pstatus)", connection)){
                        command.Transaction = transaction;
                        command.Parameters.Add("ptitle", title);
                        command.Parameters.Add("pdesc", desc);
                        command.Parameters.Add("plevel", level);
                        command.Parameters.Add("pdate", date);
                        command.Parameters.Add("prating", rating);
                        command.Parameters.Add("pinst", inst);
                        command.Parameters.Add("pstatus", status);
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

        public void DeletePattern(int id)
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                OracleTransaction transaction = null;
                try
                {
                    connection.Open();
                    transaction = connection.BeginTransaction();
                    using(var command = new OracleCommand("DELETE FROM PATTERN WHERE PATTERNID = :pid", connection)){
                        command.Transaction = transaction;
                        command.Parameters.Add("pid", id);
                        command.ExecuteNonQuery();
                        transaction.Commit(); transaction?.Dispose();  

                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    transaction?.Commit();
                }
            }
        }

        public void UpdatePattern(int id, string title, string desc, string level, string date, double rating, string inst, string status)
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                OracleTransaction transaction = null;
                try
                {
                    connection.Open();
                    transaction = connection.BeginTransaction();
                    var command = new OracleCommand("UPDATE PATTERN SET TITLE = :ptitle, DESCRIPTION= :pdesc, PATTERNLEVEL = :plevel, PATTERNDATE = :pdate, RATING = :prating, INSTRUCTIONS= :pinst, PATTERNSTATUS = :pstatus WHERE PATTERNID = :pid", connection);
                    command.Transaction = transaction;
                    command.Parameters.Add("pid", id);
                    command.Parameters.Add("ptitle", title);
                    command.Parameters.Add("pdesc", desc);
                    command.Parameters.Add("plevel", level);
                    command.Parameters.Add("pdate", date);
                    command.Parameters.Add("prating", rating);
                    command.Parameters.Add("pinst", inst);
                    command.Parameters.Add("pstatus", status);
                    command.ExecuteNonQuery();
                    transaction.Commit(); transaction?.Dispose();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    transaction?.Rollback(); transaction?.Dispose();
                }
            }
        }

        public Pattern GetPatternById(int id)
        {
            return GetPatterns("SELECT * FROM PATTERN WHERE PATTERNID = :pid", new Dictionary<string, object> { { "pid", id } }).FirstOrDefault();
        }

        public Pattern GetPatternByName(string name)
        {
            return GetPatterns("SELECT * FROM PATTERN WHERE TITLE = :pname", new Dictionary<string, object> { { "pname", name } }).FirstOrDefault();
        }

        public List<Pattern> GetAllPatterns()
        {
            return GetPatterns("SELECT * FROM PATTERN", new Dictionary<string, object>());
        }

        public List<Pattern> GetPatternsByDate(string date)
        {
            return GetPatterns("SELECT * FROM PATTERN WHERE DATE = :pdate", new Dictionary<string, object> { { "pdate", date } });
        }

        public List<Pattern> GetPatternsByLevel(string level)
        {
            return GetPatterns("SELECT * FROM PATTERN WHERE PATTERNLEVEL = :plevel", new Dictionary<string, object> { { "plevel", level } });
        }


        public List<Pattern> GetPatternsByRating(double rating)
        {
            return GetPatterns("SELECT * FROM PATTERN WHERE RATING = :rating", new Dictionary<string, object> { { "rating", rating } });
        }

        public List<Pattern> GetPatternsByStatus(string status)
        {
            return GetPatterns("SELECT * FROM PATTERN WHERE PTRNSTATUS = :pstatus", new Dictionary<string, object> { { "pstatus", status} });
        }


        public List<string> GetImages(int id) {
        
            List<string> images = new();

            using (var connection = new OracleConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (var command = new OracleCommand("with Images as ( select image_imageId as retId from has where pattern_patternid = :patternId) select url from images inner join image on retId = imageId", connection))
                    {
                        command.Parameters.Add(new OracleParameter("patternId", id));
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                images.Add(reader.GetString(0));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }


            return images;

        }


        private List<Pattern> GetPatterns(string query, Dictionary<string, object> parameters)
        {
            List<Pattern> result = new();
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
                                result.Add( new Pattern(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetDateTime(4), reader.GetDouble(5), reader.GetString(6), reader.GetString(7)));
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
