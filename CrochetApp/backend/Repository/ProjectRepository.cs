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
    public class ProjectRepository : IProjectRepository
    {

        private readonly string _connectionString;

        public ProjectRepository(string connectionString)
        {
            _connectionString = connectionString;
        }


        public int AddProject(int? parentId, string name, string notes, string status, string created, string completed, double progress)
        {
            int newId = -1;
            using (var connection = new OracleConnection(_connectionString)) {
                OracleTransaction transaction = null;
                try {
                    connection.Open();
                    transaction = connection.BeginTransaction();
                    using (var command = new OracleCommand("INSERT INTO PROJECT VALUES (null, :pprogress, :pstatus, :pdatestart, :pdateend, :pnotes, :parentid, :pPROJECTTITLE) RETURNING PROJECTID INTO :newId", connection))
                    {
                            command.Transaction = transaction;
                            command.BindByName = true;
                            command.Parameters.Add("pprogress", progress);
                            command.Parameters.Add("pstatus", status);
                            command.Parameters.Add("pdatestart", created);
                            command.Parameters.Add("pdateend", completed);
                            command.Parameters.Add("pnotes", notes);
                            command.Parameters.Add("parentid", parentId);
                            command.Parameters.Add("pPROJECTTITLE", name);
                            var outputIdParam = new OracleParameter("newId", OracleDbType.Int32)
                            {
                                Direction = System.Data.ParameterDirection.Output
                            };
                            command.Parameters.Add(outputIdParam);
                            command.ExecuteNonQuery();
                            newId = Convert.ToInt32(command.Parameters["newId"].Value.ToString());
                            transaction.Commit(); transaction?.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Error connecting to the database / Inserting new project: " + ex.Message);
                    transaction?.Rollback(); transaction?.Dispose();
                }
                return newId;
            }
        }

        public void UpdateProject(int id, string name, string notes, string status, string created, string completed, double progress)
        {
            using (var connection = new OracleConnection(_connectionString)) {
                OracleTransaction transaction = null;
                try
                {
                    connection.Open();
                    transaction = connection.BeginTransaction();
                    using (var command = new OracleCommand("UPDATE PROJECT SET PROGRESS = :pprogress, STATUS = :pstatus, DATESTART = :datestart, DATEEND = :dateend, NOTES = :pnotes, PROJECTTITLE = :pPROJECTTITLE WHERE PROJECTID = :pid", connection))
                    {
                        command.Transaction = transaction;
                        command.Parameters.Add("pprogress", progress);
                        command.Parameters.Add("pstatus", status);
                        command.Parameters.Add("datestart", created);
                        command.Parameters.Add("dateend", completed);
                        command.Parameters.Add("pnotes", notes);
                        command.Parameters.Add("pPROJECTTITLE", name);
                        command.Parameters.Add("pid", id);
                        command.ExecuteNonQuery();
                        transaction.Commit(); transaction?.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Error connecting to the database / Updating project: " + ex.Message);
                    transaction?.Rollback(); transaction?.Dispose();
                }
            }                
        }
        public void DeleteProject(int projectId)
        {

            using (var connection = new OracleConnection(_connectionString))
            {
                OracleTransaction transaction = null;
                try
                {
                    connection.Open();
                    transaction = connection.BeginTransaction();
                    using (var command = new OracleCommand("DELETE FROM PROJECT WHERE PROJECTID = :pid", connection))
                    {
                        command.Transaction = transaction;
                        command.Parameters.Add("pid", projectId);
                        command.ExecuteNonQuery();
                        transaction.Commit(); transaction?.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Error connecting to the database / Deleting project: " + ex.Message);
                    transaction?.Rollback(); transaction?.Dispose();
                }
            }
        }

        public void ConnectToPattern(int patternId, int projectId)
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                OracleTransaction transaction = null;
                try
                {
                    connection.Open();
                    transaction = connection.BeginTransaction();
                    using (var command = new OracleCommand("INSERT INTO UTILIZES VALUES (:pprojectId,:ppatternid)", connection))
                    {
                        command.BindByName = true;
                        command.Transaction = transaction;
                        command.Parameters.Add("pprojectid", projectId);
                        command.Parameters.Add("ppatternid", patternId);
                        command.ExecuteNonQuery();
                        transaction.Commit(); transaction?.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Error connecting to the database / Connecting project to pattern: " + ex.Message);
                    transaction?.Rollback(); transaction?.Dispose();
                }
            }
        }

        public List<Project> GetAllProjects()
        {
            return GetRequests("SELECT * FROM PROJECT", new Dictionary<string, object> { });
        }
        
        public List<Project> GetAllBaseProjects()
        {
            return GetRequests("SELECT * FROM PROJECT WHERE PARENTID IS NULL ORDER BY PROJECTID", new Dictionary<string, object> { });
        }

        public Project GetProjectById(int projectId)
        {
            return GetRequests("SELECT * FROM PROJECT WHERE PROJECTID = :pid", new Dictionary<string, object> { { "pid", projectId } }).FirstOrDefault();
        }
        
        public List<Project>GetAllChildren(int projectId)
        {
            return GetRequests("SELECT * FROM PROJECT WHERE PARENTID = :parentId ORDER BY PROJECTID", new Dictionary<string, object> { { "parentId", projectId } });
        }
        
        public List<Project> GetProjectsByCompletionDate(string date)
        {
            return GetRequests("SELECT * FROM PROJECT WHERE DATEEND = :pdate", new Dictionary<string, object> { { "pdate", date } });
        }

        public List<Project> GetProjectsByCreationDate(string date)
        {
            return GetRequests("SELECT * FROM PROJECT WHERE DATESTART = :pdate", new Dictionary<string, object> { { "pdate", date } });
        }

        public List<Project> GetProjectsByName(string name)
        {
            return GetRequests("SELECT * FROM PROJECT WHERE PROJECTTITLE LIKE :pname", new Dictionary<string, object> { { "pname", name } });
        }

        public List<Project> GetProjectsByProgress(double progress)
        {
            return GetRequests("SELECT * FROM PROJECT WHERE PROGRESS = :pprogress", new Dictionary<string, object> { { "pprogress", progress } });
        }

        public List<Project> GetProjectsByStatus(string status)
        {
            return GetRequests("SELECT * FROM PROJECT WHERE STATUS = :pstatus", new Dictionary<string, object> { { "pstatus", status } });
        }



        private List<Project> GetRequests(string query, Dictionary<string, object> parameters)
        {
            List<Project> result = new();
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
                                int? nullableInt = reader.IsDBNull(6) ? (int?)null : reader.GetInt32(6);
                                result.Add(new Project(reader.GetInt32(0), nullableInt, reader.GetString(7), reader.GetString(5), reader.GetString(2), reader.GetDateTime(3), reader.GetDateTime(4), reader.GetDouble(1)));
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
