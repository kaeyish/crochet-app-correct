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
    public class YarnRepository : IYarnRepository
    {

        private string _connectionString;

        public YarnRepository(string connectionString)
        {
            _connectionString = connectionString;
        }



        public void AddYarn(string name, string type, string material, int weight, float min, float max, string color)
        {
        
            using (var connection = new OracleConnection(_connectionString))
            {
                try{
                connection.Open();
                using (var command = new OracleCommand("INSERT INTO YARN VALUES (null, :YarnName, :YarnType, :YarnMaterial, :Weight, :MinSize, :MaxSize, :Color)", connection))
                {                        
                    command.Parameters.Add(new OracleParameter("YarnName", name));
                    command.Parameters.Add(new OracleParameter("YarnType", type));
                    command.Parameters.Add(new OracleParameter("YarnMaterial", material));
                    command.Parameters.Add(new OracleParameter("Weight", weight));
                    command.Parameters.Add(new OracleParameter("MinSize", min));
                    command.Parameters.Add(new OracleParameter("MaxSize", max));
                    command.Parameters.Add(new OracleParameter("Color", color));
                    command.ExecuteNonQuery();
                }
            }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error adding yarn: {ex.Message}");
                }
            }



        }

        public void DeleteYarn(int id)
        {

            using (var connection = new OracleConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (var command = new OracleCommand("DELETE FROM YARN WHERE YARNID = :id", connection))
                    {
                        command.Parameters.Add(new OracleParameter("id", id));
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error deleting yarn: {ex.Message}");
                }
            }


        }

        public List<Yarn> GetAllYarns()
        {
            List<Yarn> yarns = new List<Yarn>();

            using (var connection = new OracleConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (var command = new OracleCommand("SELECT * FROM YARN", connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Yarn yarn = new Yarn
                                {
                                    Id = reader.GetInt32(0),
                                    Name = reader.GetString(1),
                                    Type = reader.GetString(2),
                                    Material = reader.GetString(3),
                                    Weight = reader.GetInt32(4),
                                    MinSize = reader.GetFloat(5),
                                    MaxSize = reader.GetFloat(6),
                                    Color = reader.GetString(7)
                                };
                                yarns.Add(yarn);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error retrieving yarns: {ex.Message}");
                }
            }

            return yarns;

        }

        public Yarn GetYarnById(int id)
        {
            Yarn yarn = null;

            using (var connection = new OracleConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (var command = new OracleCommand("SELECT * FROM YARN WHERE YARNID = :id", connection))
                    {
                        command.Parameters.Add(new OracleParameter("id", id));
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                yarn = new Yarn
                                {
                                    Id = reader.GetInt32(0),
                                    Name = reader.GetString(1),
                                    Type = reader.GetString(2),
                                    Material = reader.GetString(3),
                                    Weight = reader.GetInt32(4),
                                    MinSize = reader.GetFloat(5),
                                    MaxSize = reader.GetFloat(6),
                                    Color = reader.GetString(7)
                                };
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error retrieving yarn by ID: {ex.Message}");
                }
            }

            return yarn;

        }

        public List<Yarn> GetYarnsByColor(string color)
        {
            List<Yarn> yarns = new List<Yarn>();

            using (var connection = new OracleConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (var command = new OracleCommand("SELECT * FROM YARN WHERE COLOR = :color", connection))
                    {
                        command.Parameters.Add(new OracleParameter("color", color));
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Yarn yarn = new Yarn
                                {
                                    Id = reader.GetInt32(0),
                                    Name = reader.GetString(1),
                                    Type = reader.GetString(2),
                                    Material = reader.GetString(3),
                                    Weight = reader.GetInt32(4),
                                    MinSize = reader.GetFloat(5),
                                    MaxSize = reader.GetFloat(6),
                                    Color = reader.GetString(7)
                                };
                                yarns.Add(yarn);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error retrieving yarns by color: {ex.Message}");
                }
            }
            return yarns;
        }

        public List<Yarn> GetYarnsByMaterial(string material)
        {
            List<Yarn> yarns = new List<Yarn>();

            using (var connection = new OracleConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (var command = new OracleCommand("SELECT * FROM YARN WHERE YARNMATERIAL = :material", connection))
                    {
                        command.Parameters.Add(new OracleParameter("material", material));
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Yarn yarn = new Yarn
                                {
                                    Id = reader.GetInt32(0),
                                    Name = reader.GetString(1),
                                    Type = reader.GetString(2),
                                    Material = reader.GetString(3),
                                    Weight = reader.GetInt32(4),
                                    MinSize = reader.GetFloat(5),
                                    MaxSize = reader.GetFloat(6),
                                    Color = reader.GetString(7)
                                };
                                yarns.Add(yarn);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error retrieving yarns by material: {ex.Message}");
                }
            }

            return yarns;
        }

        public List<Yarn> GetYarnsByName(string name)
        {
            List<Yarn> yarns = new List<Yarn>();

            using (var connection = new OracleConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (var command = new OracleCommand("SELECT * FROM YARN WHERE YARNNAME LIKE :name", connection))
                    {
                        command.Parameters.Add(new OracleParameter("name", "%" + name + "%"));
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Yarn yarn = new Yarn
                                {
                                    Id = reader.GetInt32(0),
                                    Name = reader.GetString(1),
                                    Type = reader.GetString(2),
                                    Material = reader.GetString(3),
                                    Weight = reader.GetInt32(4),
                                    MinSize = reader.GetFloat(5),
                                    MaxSize = reader.GetFloat(6),
                                    Color = reader.GetString(7)
                                };
                                yarns.Add(yarn);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error retrieving yarns by name: {ex.Message}");
                }
            }

            return yarns;
        }

        public List<Yarn> GetYarnsBySize(float size)
        {
            List<Yarn> yarns = new List<Yarn>();

            using (var connection = new OracleConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (var command = new OracleCommand("SELECT * FROM YARN WHERE :yarnsize BETWEEN MINSIZE AND MAXSIZE", connection))
                    {
                        command.Parameters.Add(new OracleParameter("yarnsize", size));
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Yarn yarn = new Yarn
                                {
                                    Id = reader.GetInt32(0),
                                    Name = reader.GetString(1),
                                    Type = reader.GetString(2),
                                    Material = reader.GetString(3),
                                    Weight = reader.GetInt32(4),
                                    MinSize = reader.GetFloat(5),
                                    MaxSize = reader.GetFloat(6),
                                    Color = reader.GetString(7)
                                };
                                yarns.Add(yarn);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error retrieving yarns by size: {ex.Message}");
                }
            }

            return yarns;

        }

        public List<Yarn> GetYarnsByType(string type)
        {
            List<Yarn> yarns = new List<Yarn>();

            using (var connection = new OracleConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (var command = new OracleCommand("SELECT * FROM YARN WHERE YARNTYPE = :type", connection))
                    {
                        command.Parameters.Add(new OracleParameter("type", type));
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Yarn yarn = new Yarn
                                {
                                    Id = reader.GetInt32(0),
                                    Name = reader.GetString(1),
                                    Type = reader.GetString(2),
                                    Material = reader.GetString(3),
                                    Weight = reader.GetInt32(4),
                                    MinSize = reader.GetFloat(5),
                                    MaxSize = reader.GetFloat(6),
                                    Color = reader.GetString(7)
                                };
                                yarns.Add(yarn);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error retrieving yarns by type: {ex.Message}");
                }
            }

            return yarns;
        }

        public List<Yarn> GetYarnsByWeight(int weight)
        {
            List<Yarn> yarns = new List<Yarn>();

            using (var connection = new OracleConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (var command = new OracleCommand("SELECT * FROM YARN WHERE WEIGHT = :weight", connection))
                    {
                        command.Parameters.Add(new OracleParameter("weight", weight));
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Yarn yarn = new Yarn
                                {
                                    Id = reader.GetInt32(0),
                                    Name = reader.GetString(1),
                                    Type = reader.GetString(2),
                                    Material = reader.GetString(3),
                                    Weight = reader.GetInt32(4),
                                    MinSize = reader.GetFloat(5),
                                    MaxSize = reader.GetFloat(6),
                                    Color = reader.GetString(7)
                                };
                                yarns.Add(yarn);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error retrieving yarns by weight: {ex.Message}");
                }
            }

            return yarns;
        }

        public void UpdateYarn(int id, string name, string type, string material, int weight, float min, float max, string color)
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (var command = new OracleCommand("UPDATE YARN SET YARNNAME = :YarnName, YARNTYPE = :YarnType, YARNMATERIAL = :YarnMaterial, WEIGHT = :Weight, MINSIZE = :MinSize, MAXSIZE = :MaxSize, COLOR = :Color WHERE YARNID = :id", connection))
                    {
                        command.Parameters.Add(new OracleParameter("YarnName", name));
                        command.Parameters.Add(new OracleParameter("YarnType", type));
                        command.Parameters.Add(new OracleParameter("YarnMaterial", material));
                        command.Parameters.Add(new OracleParameter("Weight", weight));
                        command.Parameters.Add(new OracleParameter("MinSize", min));
                        command.Parameters.Add(new OracleParameter("MaxSize", max));
                        command.Parameters.Add(new OracleParameter("Color", color));
                        command.Parameters.Add(new OracleParameter("id", id));
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error updating yarn: {ex.Message}");
                }
            }
        }
    }
}
