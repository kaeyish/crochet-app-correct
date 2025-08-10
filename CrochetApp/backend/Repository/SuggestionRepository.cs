using CrochetApp.backend.Domain.Model;
using CrochetApp.backend.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrochetApp.backend.Repository
{
    public class SuggestionRepository : ISuggestionRepository
    {

        private string _connectionString;

        public SuggestionRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public void AddSuggestion(int userId, string suggestionText)
        {
            using (var connection = new Oracle.ManagedDataAccess.Client.OracleConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (var command = new Oracle.ManagedDataAccess.Client.OracleCommand("INSERT INTO SUGGESTION VALUES (null, :suggestionText, :userId)", connection))
                    {
                        command.Parameters.Add(new Oracle.ManagedDataAccess.Client.OracleParameter("suggestionText", suggestionText));
                        command.Parameters.Add(new Oracle.ManagedDataAccess.Client.OracleParameter("userId", userId));

                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Error adding suggestion: " + ex.Message);
                }
            }
        }

        public void DeleteSuggestion(int suggestionId)
        {
            using (var connection = new Oracle.ManagedDataAccess.Client.OracleConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (var command = new Oracle.ManagedDataAccess.Client.OracleCommand("DELETE FROM SUGGESTION WHERE SUGGESTIONID = :suggestionId", connection))
                    {
                        command.Parameters.Add(new Oracle.ManagedDataAccess.Client.OracleParameter("suggestionId", suggestionId));
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Error deleting suggestion: " + ex.Message);
                }
            }
        }

        public List<Suggestion> GetAllSuggestions()
        {
            List<Suggestion> suggestions = new List<Suggestion>();

            using (var connection = new Oracle.ManagedDataAccess.Client.OracleConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (var command = new Oracle.ManagedDataAccess.Client.OracleCommand("SELECT * FROM SUGGESTION", connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var suggestion = new Suggestion
                                {
                                    Id = reader.GetInt32(0),
                                    Text = reader.GetString(1),
                                    UserId = reader.GetInt32(2)
                                };
                                suggestions.Add(suggestion);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Error retrieving suggestions: " + ex.Message);
                }
            }
            return suggestions;
        }

        public Suggestion GetSuggestionById(int suggestionId)
        {
            using (var connection = new Oracle.ManagedDataAccess.Client.OracleConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (var command = new Oracle.ManagedDataAccess.Client.OracleCommand("SELECT * FROM SUGGESTION WHERE SUGGESTIONID = :suggestionId", connection))
                    {
                        command.Parameters.Add(new Oracle.ManagedDataAccess.Client.OracleParameter("suggestionId", suggestionId));
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Suggestion
                                {
                                    Id = reader.GetInt32(0),
                                    Text = reader.GetString(1),
                                    UserId = reader.GetInt32(2)
                                };
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Error retrieving suggestion: " + ex.Message);
                }
            }

            return null; // Return null if no suggestion found
        }

        public List<Suggestion> GetSuggestionsByUserId(int userId)
        {
            List<Suggestion> suggestions = new List<Suggestion>();
            using (var connection = new Oracle.ManagedDataAccess.Client.OracleConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (var command = new Oracle.ManagedDataAccess.Client.OracleCommand("SELECT * FROM SUGGESTION WHERE APPUSERID = :userId", connection))
                    {
                        command.Parameters.Add(new Oracle.ManagedDataAccess.Client.OracleParameter("userId", userId));
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var suggestion = new Suggestion
                                {
                                    Id = reader.GetInt32(0),
                                    Text = reader.GetString(1),
                                    UserId = reader.GetInt32(2)
                                };
                                suggestions.Add(suggestion);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle exception (log it, rethrow it, etc.)

                    Debug.WriteLine("Error retrieving suggestions by user ID: " + ex.Message);
                }
            }
            return suggestions;
        }

        public void UpdateSuggestion(int suggestionId, string newText)
        {
            using (var connection = new Oracle.ManagedDataAccess.Client.OracleConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (var command = new Oracle.ManagedDataAccess.Client.OracleCommand("UPDATE SUGGESTION SET SUGGESTIONTEXT = :newText WHERE SUGGESTIONID = :suggestionId", connection))
                    {
                        command.Parameters.Add(new Oracle.ManagedDataAccess.Client.OracleParameter("newText", newText));
                        command.Parameters.Add(new Oracle.ManagedDataAccess.Client.OracleParameter("suggestionId", suggestionId));
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Error updating" + ex.Message);
                }
            }
        }
    }
}
