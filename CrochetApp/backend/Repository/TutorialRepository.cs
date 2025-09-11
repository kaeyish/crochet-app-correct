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



        public int AddTutorial(string text, string link, string diff, string title, int user)
        {
            int newId = -1;
            using (var connection = new OracleConnection(_connectionString)) {
                OracleTransaction transaction = null;
                try {
                    connection.Open();
                    using (transaction = connection.BeginTransaction()){ 
                        using (var command = new OracleCommand("INSERT INTO TUTORIAL VALUES (null, :tutotext, :tutolink, :diff, :tutotitle, :appuser) returning TutorialId into :newId", connection))
                        {
                            command.Transaction = transaction;
                            command.BindByName = true;
                            command.Parameters.Add(new OracleParameter("tutotext", text));
                            command.Parameters.Add(new OracleParameter("tutolink", link));
                            command.Parameters.Add(new OracleParameter("diff", diff));
                            command.Parameters.Add(new OracleParameter("tutotitle", title));
                            command.Parameters.Add(new OracleParameter("appuser", user));
                            var outputIdParam = new OracleParameter("newId", OracleDbType.Int32)
                            {
                                Direction = System.Data.ParameterDirection.Output
                            };
                            command.Parameters.Add(outputIdParam);
                            command.ExecuteNonQuery();
                            newId = Convert.ToInt32(outputIdParam.Value.ToString());
                            transaction.Commit(); transaction?.Dispose();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    transaction?.Rollback(); transaction?.Dispose();
                }
                return newId;
            }
        }

        public void ConnectTechniqueToTutorial(int techniqueId, int tutorialId,  int userId)
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                OracleTransaction transaction = null;
                try
                {
                    connection.Open();
                    transaction = connection.BeginTransaction();
                    using (var command = new OracleCommand("INSERT INTO EXPLAINS VALUES (:techniqueId, :tutorialId,  :userId)", connection))
                    {
                        command.Transaction = transaction;
                        command.BindByName = true;
                        command.Parameters.Add(new OracleParameter("techniqueId", techniqueId));
                        command.Parameters.Add(new OracleParameter("tutorialId", tutorialId));
                        command.Parameters.Add(new OracleParameter("userId", userId));
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

        public void DeleteTutorial(int id)
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                OracleTransaction transaction = null;
                try
                {
                    connection.Open();
                    transaction = connection.BeginTransaction();
                    using (var command = new OracleCommand("DELETE FROM TUTORIAL WHERE TUTORIALID = :id", connection))
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

        public void UpdateTutorial(int id, string text, string link, string diff, string title)
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                OracleTransaction transaction = null;
                try
                {
                    connection.Open();
                    transaction = connection.BeginTransaction();
                    using (var command = new OracleCommand("UPDATE TUTORIAL SET TUTORIALTEXT = :tutotext, VIDEOURL= :tutolink, DIFFICULTY= :diff, TUTORIALTITLE = :tutotitle WHERE TUTORIALID = :id", connection))
                    {
                        command.Transaction = transaction;
                        command.Parameters.Add(new OracleParameter("tutotext", text));
                        command.Parameters.Add(new OracleParameter("tutolink", link));
                        command.Parameters.Add(new OracleParameter("diff", diff));
                        command.Parameters.Add(new OracleParameter("tutotitle", title));
                        command.Parameters.Add(new OracleParameter("id", id));
                        command.ExecuteNonQuery();
                        transaction.Commit(); transaction?.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    transaction?.Rollback();
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
            return GetTutorials("SELECT * FROM TUTORIAL WHERE DIFFICULTY = :difficulty", new Dictionary<string, object> { { "difficulty", difficulty } });
        }

        public List<Tutorial> GetTutorialsByTitle(string title)
        {
            return GetTutorials("SELECT * FROM TUTORIAL WHERE TUTORIALTITLE LIKE :title", new Dictionary<string, object> { { "title", "%" + title + "%" } });
        }

        public List<Tutorial> GetTutorialsByUserId(int userId)
        {
            return GetTutorials("SELECT * FROM TUTORIAL WHERE CREATORID = :userId", new Dictionary<string, object> { { "userId", userId } });
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
