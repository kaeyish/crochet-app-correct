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
    public class HookRepository : IHookRepository
    {
        private string _connectionString;

        public HookRepository(string connectionString)
        {
            _connectionString = connectionString;
        }


        public void AddHook(float size)
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (var command = new OracleCommand("INSERT INTO HOOK VALUES (null, :hooksize)", connection))
                    {
                        command.Parameters.Add("hooksize", size);
                        command.ExecuteNonQuery();
                        Debug.WriteLine($"Hook with size {size} added successfully.");
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error adding hook: {ex.Message}");
                }
            }
        }

        public void DeleteHook(int id)
        {
            using (var connection = new OracleConnection(_connectionString)) {
                try {
                    connection.Open();
                    using (var command = new OracleCommand("DELETE FROM HOOK WHERE HOOKID = :Id", connection))
                    {
                        command.Parameters.Add("Id", id);
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected == 0)
                        {
                            Debug.WriteLine($"No hook found with HOOKID {id} to delete.");
                        }
                        else
                        {
                            Debug.WriteLine($"Hook with HOOKID {id} deleted successfully.");
                        }
                    }

                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error deleting hook with HOOKID {id}: {ex.Message}");
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
                                    Size = reader.GetFloat(1)
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

        public List<Hook> GetAllBySize(float size)
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
                                hooks.Add(new Hook(reader.GetInt32(0), reader.GetFloat(1)));
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


        public void UpdateHook(float size, int id)
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (var command = new OracleCommand("UPDATE HOOK SET HOOKSIZE = :hooksize WHERE HOOKID = :Id", connection))
                    {
                        command.Parameters.Add("hooksize", size);
                        command.Parameters.Add("Id", id); 
                        command.ExecuteNonQuery();
                    }

                }
                catch (Exception ex) {
                    Debug.WriteLine($"Error updating hook : {ex.Message}");
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
                                hook.Size = reader.GetFloat(1);
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

    }
}
