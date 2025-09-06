using CrochetApp.backend.Domain.Model;
using CrochetApp.backend.Domain.RepositoryInterfaces;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CrochetApp.backend.Repository
{
    internal class TagRepository : ITagRepository
    {

        private string _connectionString;

        public TagRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Tag> GetAllTags()
        {
            
            List<Tag> tags = new List<Tag>();

            using (var connection = new OracleConnection(_connectionString)) {
                try
                {
                    connection.Open();
                    using (var command = new OracleCommand("SELECT * FROM TAG", connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read()) { 
                                tags.Add(new Tag(reader.GetInt32(0), reader.GetString(1)));
                            }
                        }
                    }
                }
                catch (OracleException e) { }
            }
            
            return tags;

        }

        public Tag GetTagById(int id)
        {
            Tag tag = new Tag();

            var connection = new OracleConnection(_connectionString);

            using (connection)
            {
                try
                {
                    connection.Open();

                    string query = "SELECT TAGID, TAGTEXT FROM TAG WHERE TAGID = " + id.ToString();
                    using (var command = new OracleCommand(query, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                                tag = new Tag(reader.GetInt32(0), reader.GetString(1));
                        }
                    }
                }
                catch (OracleException e)
                {
                    Debug.WriteLine($"Database error: {e.Message}");
                }
            }
             return tag;

        }

        public Tag GetTagByName(string name)
        {
            Tag tag = new Tag();

            using (var connection = new OracleConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM TAG WHERE TAGTEXT = :name";
                    using (var command = new OracleCommand(query, connection))
                    {
                        command.Parameters.Add(new OracleParameter("name", name));
                        Debug.WriteLine($"Query: {query}, Parameter: {name}");
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                                tag = new Tag(reader.GetInt32(0), reader.GetString(1));
                        }
                    }
                }
                catch (OracleException e)
                {

                    Console.WriteLine($"Database error: {e.Message}");
                }
            }

            return tag;
        }

        void ITagRepository.AddTag(string text)
        {
            using (var connection  = new OracleConnection(_connectionString))
            {
                OracleTransaction transaction = null;
                try
                {
                    connection.Open();
                    transaction = connection.BeginTransaction();
                    string query = "INSERT INTO TAG VALUES (null, :text)";
                    using (var command = new OracleCommand(query, connection))
                    {
                        command.Transaction = transaction;
                        command.Parameters.Add(new OracleParameter("text", text));
                        command.ExecuteNonQuery();
                        transaction.Commit(); transaction?.Dispose();
                    }
                }
                catch (OracleException e)
                {
                    Debug.WriteLine($"Database error: {e.Message}");
                    transaction?.Rollback(); transaction?.Dispose();
                }
            }



        }

        Tag ITagRepository.DeleteTag(int id)
        {

            Tag deleted = GetTagById(id);
            
            if (deleted == null)
            {
                return null; 
            }

            using (var connectiong = new OracleConnection(_connectionString))
            {
                OracleTransaction transaction = null;
                try
                {
                    connectiong.Open();
                    transaction = connectiong.BeginTransaction();
                    string query = "DELETE FROM TAG WHERE TAGID = :id";
                    using (var command = new OracleCommand(query, connectiong))
                    {
                        command.Transaction = transaction;
                        command.Parameters.Add(new OracleParameter("id", id));
                        command.ExecuteNonQuery();
                        transaction.Commit(); transaction?.Dispose();
                    }

                    return deleted;
                }
                catch (OracleException e)
                {
                    Console.WriteLine($"Database error: {e.Message}");
                    transaction?.Rollback(); transaction?.Dispose();
                    return null; 
                }
            }
        }

       

        void ITagRepository.UpdateTag(int id, string text)
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                OracleTransaction transaction = null;
                try
                {
                    connection.Open();
                    transaction = connection.BeginTransaction();
                    string query = "UPDATE TAG SET TAGTEXT = :text WHERE TAGID = :id";
                    using (var command = new OracleCommand(query, connection))
                    {
                        command.Transaction = transaction;
                        command.Parameters.Add(new OracleParameter("text", text));
                        command.Parameters.Add(new OracleParameter("id", id));
                        command.ExecuteNonQuery();
                        transaction.Commit(); transaction?.Dispose();
                    }
                }
                catch (OracleException e)
                {
                    Console.WriteLine($"Database error: {e.Message}");
                    transaction?.Rollback(); transaction?.Dispose();
                }

            }
        }
    }
}
