using CrochetApp.backend.Domain.Model;
using CrochetApp.backend.Domain.RepositoryInterfaces;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            using (var connection = new OracleConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (var command = new OracleCommand("SELECT * FROM TAG", connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                if (reader.GetInt32(0) == id) { 
                                    tag = new Tag(reader.GetInt32(0), reader.GetString(1));
                                }
                            }
                        }
                    }
                }
                catch (OracleException e) { }
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
                    using (var command = new OracleCommand("SELECT * FROM TAG", connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                if (reader.GetString(1) == name)
                                {
                                    tag = new Tag(reader.GetInt32(0), reader.GetString(1));
                                }
                            }
                        }
                    }
                }
                catch (OracleException e) { }
            }

            return tag;
        }
    }
}
