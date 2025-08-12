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
    public class TutorialRepository : ITutorialRepository
    {

        private readonly string _connectionString;

        public TutorialRepository(string connectionString)
        {
            _connectionString = connectionString;
        }



        public void AddTutorial(string text, string link, string diff, string title, int user)
        {
            using (var connection = new OracleConnection(_connectionString)) {
                try {
                    connection.Open();
                    using (var command = new OracleCommand("INSERT INTO TUTORIAL VALUES (null, :tutotext, :tutolink, :diff, :tutotitle, :appuser)", connection)) {
                        command.Parameters.Add(new OracleParameter("tutotext", text));
                        command.Parameters.Add(new OracleParameter("tutolink", link));
                        command.Parameters.Add(new OracleParameter("diff", diff));
                        command.Parameters.Add(new OracleParameter("tutotitle", title));
                        command.Parameters.Add(new OracleParameter("appuser", user));
                        command.ExecuteNonQuery();
                    }
                
                
                
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
        }

        public void DeleteTutorial(int id)
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (var command = new OracleCommand("DELETE FROM TUTORIAL WHERE TUTORIALID = :id", connection))
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

        }

        public void UpdateTutorial(int id, string text, string link, string diff, string title)
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (var command = new OracleCommand("UPDATE TUTORIAL SET TUTORIALTXT = :tutotext, TUTORIALVID = :tutolink, ESTDIFF = :diff, TUTORIALTITLE = :tutotitle WHERE TUTORIALID = :id", connection))
                    {
                        command.Parameters.Add(new OracleParameter("tutotext", text));
                        command.Parameters.Add(new OracleParameter("tutolink", link));
                        command.Parameters.Add(new OracleParameter("diff", diff));
                        command.Parameters.Add(new OracleParameter("tutotitle", title));
                        command.Parameters.Add(new OracleParameter("id", id));
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
        }

    

        public List<Tutorial> GetAllTutorials()
        {
            return GetTutorials("SELECT * FROM TUTORIAL", new Dictionary<string, object>());
        }

        public Tutorial GetTutorialById(int id)
        {
            return GetTutorials("SELECT * FROM TUTORIAL WHERE TUTORIALID = :id", new Dictionary<string, object> { { "id", id } }).FirstOrDefault();
        }

        public List<Tutorial> GetTutorialsByDifficulty(string difficulty)
        {
            return GetTutorials("SELECT * FROM TUTORIAL WHERE ESTDIFF = :difficulty", new Dictionary<string, object> { { "difficulty", difficulty } });
        }

        public List<Tutorial> GetTutorialsByTitle(string title)
        {
            return GetTutorials("SELECT * FROM TUTORIAL WHERE TUTORIALTITLE LIKE :title", new Dictionary<string, object> { { "title", "%" + title + "%" } });
        }

        public List<Tutorial> GetTutorialsByUserId(int userId)
        {
            return GetTutorials("SELECT * FROM TUTORIAL WHERE APPUSERID = :userId", new Dictionary<string, object> { { "userId", userId } });
        }


        public List<Tutorial> GetTutorials(string query, Dictionary<string, object> parameters)
        {
            List<Tutorial>result = new();
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
                            while(reader.Read())
                            {
                                result.Add(new Tutorial(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetInt32(5)));
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
