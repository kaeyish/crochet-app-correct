using CrochetApp.backend.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CrochetApp.backend.Domain.RepositoryInterfaces
{
    public interface ITechniqueRepository
    {
        List<Technique> GetAllTechniques();
        
        Technique GetTechniqueByName(string name);

        List<Technique> GetTechniquesByLevel(string level);

        void AddTechnique(string name, string level);

        void DeleteTechnique(string name);

        void DeleteTechniqueById(int id);

        Technique GetTechniqueById(int id);

        void UpdateTechnique(int id, string name, string level);

        /// delete all of certain difficulty
    }
}
