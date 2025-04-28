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
                    connection.Open();

                    string query = "SELECT TAGID, TAGTEXT FROM TAG WHERE TAGID = " + id.ToString();
                    using (var command = new OracleCommand(query, connection)){ 
                        using (var reader = command.ExecuteReader()){
                            while (reader.Read())
                            tag = new Tag(reader.GetInt32(0), reader.GetString(1));
                        }
                    }
                    
                }


             return tag;

        }
        //im going to give up this in the database and just assure everything is unique in the service
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
                    // Handle exception (e.g., log it)
                    Console.WriteLine($"Database error: {e.Message}");
                }
            }

            return tag;
        }
    }
}
