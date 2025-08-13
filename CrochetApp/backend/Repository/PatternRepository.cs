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
        public void AddPattern(string title, string desc, string level, string date, float rating, string inst, string status, int requestId)
        {
            using (var connection = new OracleConnection(_connectionString)) {
                try
                {
                    connection.Open();
                    var command = new OracleCommand("INSERT INTO PATTERN VALUES (null, :title, :desc, :level, :date, :rating, :inst, :status, :requestId)", connection);
                    command.Parameters.Add("title", title);
                    command.Parameters.Add("desc", desc);
                    command.Parameters.Add("level", level);
                    command.Parameters.Add("date", date);
                    command.Parameters.Add("rating", rating);
                    command.Parameters.Add("inst", inst);
                    command.Parameters.Add("status", status);
                    command.Parameters.Add("requestId", requestId);
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }

            }
        }

        public void DeletePattern(int id)
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    var command = new OracleCommand("DELETE FROM PATTERN WHERE PATTERNID = :id", connection);
                    command.Parameters.Add("id", id);
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
        }

        public void UpdatePattern(int id, string title, string desc, string level, string date, float rating, string inst, string status)
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    var command = new OracleCommand("UPDATE PATTERN SET TITLE = :title, DESCRIPTION = :desc, LEVEL = :level, DATE = :date, RATING = :rating, INSTRUCTIONS = :inst, PTRNSTATUS = :status WHERE PATTERNID = :id", connection);
                    command.Parameters.Add("id", id);
                    command.Parameters.Add("title", title);
                    command.Parameters.Add("desc", desc);
                    command.Parameters.Add("level", level);
                    command.Parameters.Add("date", date);
                    command.Parameters.Add("rating", rating);
                    command.Parameters.Add("inst", inst);
                    command.Parameters.Add("status", status);
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
        }

        public Pattern GetPatternById(int id)
        {
            return GetPatterns("SELECT * FROM PATTERN WHERE ID = :id", new Dictionary<string, object> { { "id", id } }).FirstOrDefault();
        }

        public Pattern GetPatternByName(string name)
        {
            return GetPatterns("SELECT * FROM PATTERN WHERE TITLE = :name", new Dictionary<string, object> { { "name", name } }).FirstOrDefault();
        }

        public List<Pattern> GetPatterns()
        {
            return GetPatterns("SELECT * FROM PATTERN", new Dictionary<string, object>());
        }

        public List<Pattern> GetPatternsByDate(string date)
        {
            return GetPatterns("SELECT * FROM PATTERN WHERE DATE = :date", new Dictionary<string, object> { { "date", date } });
        }

        public List<Pattern> GetPatternsByLevel(string level)
        {
            return GetPatterns("SELECT * FROM PATTERN WHERE LEVEL = :level", new Dictionary<string, object> { { "level", level } });
        }


        public List<Pattern> GetPatternsByRating(float rating)
        {
            return GetPatterns("SELECT * FROM PATTERN WHERE RATING = :rating", new Dictionary<string, object> { { "rating", rating } });
        }

        public List<Pattern> GetPatternsByStatus(string statusId)
        {
            return GetPatterns("SELECT * FROM PATTERN WHERE PTRNSTATUS = :statusId", new Dictionary<string, object> { { "statusId", statusId } });
        }


        public List<Pattern> GetPatterns(string query, Dictionary<string, object> parameters)
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
                                result.Add( new Pattern(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetDateTime(5), reader.GetFloat(6), reader.GetString(7), reader.GetInt32(8)));
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
