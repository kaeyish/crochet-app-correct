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


        public void AddProject(int parentId, string name, string notes, string status, string created, string completed, float progress)
        {

            using (var connection = new OracleConnection(_connectionString)) {
                try {
                    connection.Open();
                    using (var command = new OracleCommand("INSERT INTO PROJECT VALUES (null, :pprogress, :pstatus, :pdatestart, :pdateend, :pnotes, :parentid, :pPROJECTTITLE)", connection))
                    {
                            command.Parameters.Add("pprogress", progress);
                            command.Parameters.Add("pstatus", status);
                            command.Parameters.Add("pdatestart", created);
                            command.Parameters.Add("pdateend", completed);
                            command.Parameters.Add("pnotes", notes);
                            command.Parameters.Add("parentid", parentId);
                            command.Parameters.Add("pPROJECTTITLE", name);
                            command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Error connecting to the database / Inserting new project: " + ex.Message);
                }
            }
        }

        public void UpdateProject(int id, string name, string notes, string status, string created, string completed, float progress)
        {
            using (var connection = new OracleConnection(_connectionString)) {
                try
                {
                    connection.Open();
                    using (var command = new OracleCommand("UPDATE PROJECT SET PROGRESS = :pprogress, STATUS = :pstatus, DATESTART = :datestart, DATEEND = :dateend, NOTES = :pnotes, PROJECTTITLE = :pPROJECTTITLE WHERE PROJECTID = :pid", connection))
                    {
                        command.Parameters.Add("pprogress", progress);
                        command.Parameters.Add("pstatus", status);
                        command.Parameters.Add("datestart", created);
                        command.Parameters.Add("dateend", completed);
                        command.Parameters.Add("pnotes", notes);
                        command.Parameters.Add("pPROJECTTITLE", name);
                        command.Parameters.Add("pid", id);
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Error connecting to the database / Updating project: " + ex.Message);
                }


            }                
        }
        public void DeleteProject(int projectId)
        {

            using (var connection = new OracleConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (var command = new OracleCommand("DELETE FROM PROJECT WHERE PROJECTID = :pid", connection))
                    {
                        command.Parameters.Add("pid", projectId);
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Error connecting to the database / Deleting project: " + ex.Message);
                }
            }
        }

        public List<Project> GetAllProjects()
        {
            return GetRequests("SELECT * FROM PROJECT", new Dictionary<string, object> { });
        }

        public Project GetProjectById(int projectId)
        {
            return GetRequests("SELECT * FROM PROJECT WHERE PROJECTID = :pid", new Dictionary<string, object> { { "pid", projectId } }).FirstOrDefault();
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

        public List<Project> GetProjectsByProgress(float progress)
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
                                result.Add(new Project(reader.GetInt32(0), nullableInt, reader.GetString(7), reader.GetString(5), reader.GetString(2), reader.GetDateTime(3), reader.GetDateTime(4), reader.GetFloat(1)));
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
