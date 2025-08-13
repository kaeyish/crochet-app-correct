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
    public class RequestRepository : IRequestRepository
    {
        private readonly string _connectionString;

        public RequestRepository(string connectionString)
        {
            _connectionString = connectionString;
        }


        public void AddRequest(string date, string status, int creatorId)
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    var command = new OracleCommand("INSERT INTO Request VALUES (null, :rdate, :rstatus, null, :creatorId)", connection);
                    command.Parameters.Add("rdate", date);
                    command.Parameters.Add("rstatus", status);
                    command.Parameters.Add("creatorId", creatorId);
                    command.ExecuteNonQuery();
                }
                catch (Exception ex) {
                    Debug.WriteLine(ex.Message);
                }
                }
        }

        public void DeleteRequest(int id)
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    var command = new OracleCommand("DELETE FROM Request WHERE RequestId = :rid", connection);
                    command.Parameters.Add("rid", id);
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
                }
        }

        public void UpdateRequest(int id, string date, string status, int adminId)
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    var command = new OracleCommand("UPDATE Request SET RequestDate = :rdate, RequestStatus = :rstatus, AdminId = :adminId WHERE RequestId = :rid", connection);
                    command.Parameters.Add("rdate", date);
                    command.Parameters.Add("rstatus", status);
                    command.Parameters.Add("adminId", adminId);
                    command.Parameters.Add("rid", id);
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
        }


        public Request GetRequestById(int id)
        {
            return GetRequests("SELECT * FROM REQUEST WHERE REQUESTID = :rid", new Dictionary<string, object> { { "rid", id } }).FirstOrDefault();
        }

        public List<Request> GetRequestsByCreator(int creatorIduserId)
        {
            return GetRequests("SELECT * FROM REQUEST WHERE CREATORID = :creatorId", new Dictionary<string, object> { { "creatorId", creatorIduserId } });
        }

        public List<Request> GetRequestsByDate(string date)
        {
            return GetRequests("SELECT * FROM REQUEST WHERE REQUESTDATE = :rdate", new Dictionary<string, object> { { "rdate", date } });
        }

        public List<Request> GetRequestsByStatus(string status)
        {
            return GetRequests("SELECT * FROM REQUEST WHERE REQUESTSTATUS = :rstatus", new Dictionary<string, object> { { "rstatus", status } });
        }

        public List<Request> GetRequests(string query, Dictionary<string, object> parameters)
        {
            List<Request> result = new();
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
                                result.Add(new Request(reader.GetInt32(0), reader.GetDateTime(1), reader.GetString(2), reader.GetInt32(3), reader.GetInt32(4)));
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
