using CrochetApp.backend.Domain.Model;
using CrochetApp.backend.Domain.RepositoryInterfaces;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace CrochetApp.backend.Repository
{
    public class HookRepository : IHookRepository
    {
        private string _connectionString;

        public HookRepository(string connectionString)
        {
            _connectionString = connectionString;
        }


        public int AddHook(double size)
        {
            int id = -1;
            using (var connection = new OracleConnection(_connectionString))
            {
                OracleTransaction transaction = null;
                try
                {
                    connection.Open();
                    transaction = connection.BeginTransaction();
                    using (var command = new OracleCommand("INSERT INTO HOOK VALUES (null, :hooksize) RETURNING HOOKID INTO :retId", connection))
                    {
                        command.BindByName = true;
                        command.Transaction = transaction;
                        command.Parameters.Add("hooksize", size);
                        var retIdParam = new OracleParameter("retId", OracleDbType.Int32)
                        {
                            Direction = System.Data.ParameterDirection.Output
                        };
                        command.Parameters.Add(retIdParam); ;
                        command.ExecuteNonQuery();
                        id = Convert.ToInt32(retIdParam.Value.ToString());
                        transaction.Commit(); transaction?.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error adding hook: {ex.Message}");
                    transaction?.Rollback(); transaction?.Dispose(); 
                }
                return id;
            }
        }

        public void DeleteHook(int id)
        {
            using (var connection = new OracleConnection(_connectionString)) {
                OracleTransaction transaction = null;
                try {
                    connection.Open();
                    transaction = connection.BeginTransaction();
                    using (var command = new OracleCommand("DELETE FROM HOOK WHERE HOOKID = :Id", connection))
                    {
                        command.Transaction = transaction;
                        command.Parameters.Add("Id", id);
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected == 0)
                        {
                            Debug.WriteLine($"No hook found with HOOKID {id} to delete.");
                            transaction?.Rollback(); transaction?.Dispose();
                        }
                        else
                        {
                            Debug.WriteLine($"Hook with HOOKID {id} deleted successfully.");
                            transaction.Commit(); transaction?.Dispose();
                        }
                    }

                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error deleting hook with HOOKID {id}: {ex.Message}");
                    transaction?.Rollback(); transaction?.Dispose();
                }
            }
        }

        public List<Hook> GetAll()
        {
            List<Hook> hooks = new List<Hook>();
            using (var connection = new OracleConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (var command = new OracleCommand("SELECT * FROM HOOK", connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Hook hook = new Hook
                                {
                                    Id = reader.GetInt32(0),
                                    Size = reader.GetDouble(1)
                                };
                                hooks.Add(hook);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error retrieving hooks : {ex.Message}");
                }
            }
            return hooks;

        }

        public List<Hook> GetAllBySize(double size)
        {
            List<Hook> hooks = new List<Hook>();
            using (var connection = new OracleConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (var command = new OracleCommand("SELECT * FROM HOOK WHERE HOOKSIZE = :hooksize", connection))
                    {
                        command.Parameters.Add(new OracleParameter("hooksize", size));
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                hooks.Add(new Hook(reader.GetInt32(0), reader.GetDouble(1)));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error retrieving hooks by size {size}: {ex.Message}");
                }
            }
            return hooks;
        }


        public void UpdateHook(double size, int id)
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                OracleTransaction transaction = null;
                try
                {
                    connection.Open();
                    transaction = connection.BeginTransaction();
                    using (var command = new OracleCommand("UPDATE HOOK SET HOOKSIZE = :hooksize WHERE HOOKID = :Id", connection))
                    {
                        command.Transaction = transaction;
                        command.Parameters.Add("hooksize", size);
                        command.Parameters.Add("Id", id); 
                        command.ExecuteNonQuery();
                        transaction.Commit(); transaction?.Dispose();
                    }

                }
                catch (Exception ex) {
                    Debug.WriteLine($"Error updating hook : {ex.Message}");
                    transaction?.Rollback(); transaction?.Dispose();
                }

            }
            
        }

        public Hook GetById(int id)
        {
            Hook hook = new Hook();

            using (var connection = new OracleConnection(_connectionString))
            {

                try
                {
                    connection.Open();
                    using (var command = new OracleCommand("SELECT * FROM HOOK WHERE HOOKID = :Id", connection))
                    {
                        command.Parameters.Add("Id", id);
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                hook.Id = reader.GetInt32(0);
                                hook.Size = reader.GetDouble(1);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error retrieving hook with HOOKID {id}: {ex.Message}");
                }
            }


            return hook;


        }

        public void ConnectToPattern(int id, int patternId)
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                OracleTransaction transaction = null;
                try
                {
                    connection.Open();
                    transaction = connection.BeginTransaction();
                    using (var command = new OracleCommand("INSERT INTO RECOMMENDS VALUES (:patternId, :hookId)", connection))
                    {
                        command.Transaction = transaction;
                        command.Parameters.Add("hookId", id);
                        command.Parameters.Add("patternId", patternId);
                        command.ExecuteNonQuery();
                        transaction.Commit(); transaction?.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error connecting hook with HOOKID {id} to pattern with PATTERNID {patternId}: {ex.Message}");
                    transaction?.Rollback(); transaction?.Dispose();
                }
            }
        }
    }
}
