using CrochetApp.backend.Domain.Model;
using CrochetApp.backend.Domain.RepositoryInterfaces;
using CrochetApp.backend.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrochetApp.backend.Service
{
    public class YarnService
    {
        private IYarnRepository _yarnRepository;

        public YarnService(IYarnRepository yarnRepository)
        {
            _yarnRepository = yarnRepository;
        }

        public void AddYarn(string name, string type, string material, int weight, float min, float max, string color)
        {
            _yarnRepository.AddYarn(name, type, material, weight, min, max, color);
        }

        public void DeleteYarn(int id)
        {
            _yarnRepository.DeleteYarn(id);
        }

        public List<Yarn> GetAllYarns()
        {
            return _yarnRepository.GetAllYarns();
        }

        public Yarn GetYarnById(int id)
        {
            return _yarnRepository.GetYarnById(id);
        }

        public List<Yarn> GetYarnsByColor(string color)
        {
            return _yarnRepository.GetYarnsByColor(color);
        }

        public List<Yarn> GetYarnsByMaterial(string material)
        {
            return _yarnRepository.GetYarnsByMaterial(material.ToLower());
        }

        public List<Yarn> GetYarnsByName(string name)
        {
            return _yarnRepository.GetYarnsByName(name);
        }

        public List<Yarn> GetYarnsBySize(float size)
        {
            return _yarnRepository.GetYarnsBySize(size);
        }

        public List<Yarn> GetYarnsByType(string type)
        {
            return _yarnRepository.GetYarnsByType(type);
        }

        public List<Yarn> GetYarnsByWeight(int weight)
        {
            return _yarnRepository.GetYarnsByWeight(weight);
        }

        public void UpdateYarn(int id, string name, string type, string material, int weight, float min, float max, string color)
        {
            _yarnRepository.UpdateYarn(id, name, type, material, weight, min, max, color);
        }


    }
}
