using CrochetApp.backend.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrochetApp.backend.Domain.RepositoryInterfaces
{
    public interface ITagRepository
    {
        List<Tag> GetAllTags();

        Tag GetTagById(int id);

        Tag GetTagByName(string name);

        void AddTag(string text);

        void UpdateTag(int id, string text);

        Tag DeleteTag(int id);
    }
}
