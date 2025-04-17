using CrochetApp.backend.Domain.Model;
using CrochetApp.backend.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrochetApp.backend.Repository
{
    internal class TagRepository : ITagRepository
    {
        public List<Tag> GetAllTags()
        {
            throw new NotImplementedException();
        }

        public Tag GetTagById(int id)
        {
            throw new NotImplementedException();
        }

        public Tag GetTagByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
