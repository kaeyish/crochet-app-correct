using CrochetApp.backend.Domain.Model;
using CrochetApp.backend.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrochetApp.backend.Service
{
    public class TagService
    {
        private ITagRepository _tagRepository;

        public TagService(ITagRepository repository) { 
            _tagRepository = repository;
        }

        public List<Tag> GetAllTags() {
            return _tagRepository.GetAllTags();
        }

        public Tag GetTagById(int id) {
            return _tagRepository.GetTagById(id);
        }

        public Tag GetTagByName(string name) {
            return _tagRepository.GetTagByName(name);
        }
    }
}
