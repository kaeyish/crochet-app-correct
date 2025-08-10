using CrochetApp.backend.Domain.Model;
using CrochetApp.backend.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CrochetApp.backend.Service
{
    public class TechniqueService
    {

        private ITechniqueRepository _techniqueRepository;

        public TechniqueService(ITechniqueRepository techniqueRepository)
        {
            _techniqueRepository = techniqueRepository;
        }

        //sad za sada sve metode nose level kao string
        //jer jos uvek ne znam do kakvih interackcija ce doci sa korisnikom
        //i da li ce biti potrebno da se level prenosi kao string ili kao enum

        public void AddTechnique(string name, string level)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(level))
            {
                throw new ArgumentException("Name and level cannot be empty.");
            }
            // Check if the technique already exists
            var existingTechnique = _techniqueRepository.GetTechniqueByName(name);
            if (existingTechnique != null)
            {
                throw new InvalidOperationException($"Technique with name '{name}' already exists.");
            }
            _techniqueRepository.AddTechnique(name, level);

        }

        public void DeleteTechnique(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name cannot be empty.");
            }
            // Check if the technique exists
            var existingTechnique = _techniqueRepository.GetTechniqueByName(name);
            if (existingTechnique == null)
            {
                throw new InvalidOperationException($"Technique with name '{name}' does not exist.");
            }
            _techniqueRepository.DeleteTechnique(name);
        }

        public void DeleteTechniqueById(int id)
        {
            var existingTechnique = _techniqueRepository.GetTechniqueById(id);
            if (existingTechnique == null)
            {
                throw new InvalidOperationException($"Technique with id '{id}' does not exist.");
            }
            _techniqueRepository.DeleteTechniqueById(id);
        }

        public List<Technique> GetAllTechniques()
        {
            return _techniqueRepository.GetAllTechniques();
        }

        public Technique GetTechniqueById(int id)
        {
            var technique = _techniqueRepository.GetTechniqueById(id);
            if (technique == null)
            {
                throw new InvalidOperationException($"Technique with id '{id}' does not exist.");
            }
            return technique;
        }

        public Technique GetTechniqueByName(string name)
        {
            var technique = _techniqueRepository.GetTechniqueByName(name);
            if (technique == null)
            {
                throw new InvalidOperationException($"Technique with name '{name}' does not exist.");
            }
            return technique;
        }

        public List<Technique> GetTechniquesByLevel(string level)
        {
            List<Technique> techniques = _techniqueRepository.GetTechniquesByLevel(level);
            if (techniques == null || techniques.Count == 0)
            {
                throw new InvalidOperationException($"No techniques found for level '{level}'.");
            }
            return techniques;
        }

        public void UpdateTechnique(int id, string name, string level)
        {
            _techniqueRepository.UpdateTechnique(id, name, level);
        }
    }
}
