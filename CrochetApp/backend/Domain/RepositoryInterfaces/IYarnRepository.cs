using CrochetApp.backend.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CrochetApp.backend.Domain.RepositoryInterfaces
{
    public interface IYarnRepository
    {
        public List<Yarn> GetAllYarns();
        public Yarn GetYarnById(int id);

        public void AddYarn(string name, string type, string material, int weight, float min, float max, string color);
        public void UpdateYarn(int id, string name, string type, string material, int weight, float min, float max, string color);
        public void DeleteYarn(int id);

        public List<Yarn> GetYarnsByName(string name);
        public List<Yarn> GetYarnsByType(string type);
        public List<Yarn> GetYarnsByMaterial(string material);
        public List<Yarn> GetYarnsByWeight(int weight);
        public List<Yarn> GetYarnsByColor(string color);
        public List<Yarn> GetYarnsBySize(float size);

    }
}
