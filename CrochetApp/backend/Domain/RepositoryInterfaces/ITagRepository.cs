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

        int AddTag(string text);

        void UpdateTag(int id, string text);

        Tag DeleteTag(int id);
        void ConnectToPattern(int tagId,int id);
    }
}
