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
    public class TechniqueRepository : ITechniqueRepository
    {
        private readonly string _connectionString;

        public TechniqueRepository(string connectionString) {
            _connectionString = connectionString;
        }

        public void AddTechnique(string name, string level)
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (var command = new OracleCommand("INSERT INTO TECHNIQUE VALUES (null, :techName, :techLevel)", connection))
                    {
                        command.Parameters.Add(new OracleParameter("techName", name));
                        command.Parameters.Add(new OracleParameter("techLevel", level));
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error adding technique: {ex.Message}");
                }
            }
        }

        public void DeleteTechnique(string name)
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (var command = new OracleCommand("DELETE FROM TECHNIQUE WHERE TECHNIQUENAME = :techName", connection))
                    {
                        command.Parameters.Add(new OracleParameter("techName", name));
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected == 0)
                        {
                            Debug.WriteLine($"No technique found with name {name} to delete.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error deleting technique: {ex.Message}");
                }
            }
        }

        public void DeleteTechniqueById(int id)
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (var command = new OracleCommand("DELETE FROM TECHNIQUE WHERE TECHNIQUEID = :techId", connection))
                    {
                        command.Parameters.Add(new OracleParameter("techId", id));
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected == 0)
                        {
                            Debug.WriteLine($"No technique found with ID {id} to delete.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error deleting technique: {ex.Message}");
                }
            }
        }

        public List<Technique> GetAllTechniques()
        {
            List<Technique> techniques = new List<Technique>();

            using (var connection = new OracleConnection(_connectionString)) {
                try { 
                    connection.Open();
                    using (var command = new OracleCommand("SELECT * FROM TECHNIQUE", connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Technique technique = new Technique(reader.GetInt32(0), reader.GetString(1), reader.GetString(2));
                                techniques.Add(technique);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error fetching all: {ex.Message}");
                    return []; // Return empty list on error
                }

            }
            return techniques;
        }


        public List<Technique> GetTechniquesByLevel(string level)
        {
            List<Technique> techniques = new List<Technique>();
            using (var connection = new OracleConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (var command = new OracleCommand("SELECT * FROM TECHNIQUE WHERE TECHNIQUEDIFF = :techlevel", connection))
                    {
                        command.Parameters.Add(new OracleParameter("techlevel", level));
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Technique technique = new Technique(reader.GetInt32(0), reader.GetString(1), reader.GetString(2));
                                techniques.Add(technique);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error fetching by level: {ex.Message}");
                    return []; // Return empty list on error
                }
            }
            return techniques;



        }


        public Technique GetTechniqueById(int id)
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (var command = new OracleCommand("SELECT * FROM TECHNIQUE WHERE TECHNIQUEID = :techId", connection))
                    {
                        command.Parameters.Add(new OracleParameter("techId", id));
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Technique(reader.GetInt32(0), reader.GetString(1), reader.GetString(2));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error fetching by id: {ex.Message}");
                }
                return null; // Return null if not found or on error
            }
        }

        public Technique GetTechniqueByName(string name)
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (var command = new OracleCommand("SELECT * FROM TECHNIQUE WHERE TECHNIQUENAME = :techName", connection))
                    {
                        command.Parameters.Add(new OracleParameter("techName", name));
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Technique(reader.GetInt32(0), reader.GetString(1), reader.GetString(2));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error fetching by name: {ex.Message}");
                }
                return null; // Return null if not found or on error
            }

        }

        public void UpdateTechnique(int id, string name, string level)
        {
            using (var connection = new OracleConnection(_connectionString)) {
                try { 
                    connection.Open();



                    using (var command = new OracleCommand("UPDATE TECHNIQUE SET TECHNIQUENAME = :techName, TECHNIQUEDIFF = :techLevel WHERE TECHNIQUEID = :techId", connection))
                    {
                        command.Parameters.Add(new OracleParameter("techName", name));
                        command.Parameters.Add(new OracleParameter("techLevel", level));
                        command.Parameters.Add(new OracleParameter("techId", id));
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected == 0)
                        {
                            Debug.WriteLine($"No technique found with ID {id} to update.");
                        }
                    }

                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error updating technique: {ex.Message}");
                }
            }

        }
    }
}
