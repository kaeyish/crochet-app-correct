using CrochetApp.backend.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrochetApp.backend.Domain.RepositoryInterfaces
{
    public interface ITutorialRepository
    {
        public int AddTutorial(string text, string link, string diff, string title, int user);

        public void UpdateTutorial(int id, string text, string link, string diff, string title);

        public void DeleteTutorial(int id);
        public List<Tutorial> GetAllTutorials();
        public Tutorial GetTutorialById(int id);

        void ConnectTechniqueToTutorial(int techniqueId, int tutorialId, int userId);
        public List<Tutorial> GetTutorialsByUserId(int userId);

        public List<Tutorial> GetTutorialsByDifficulty(string difficulty);

        public List<Tutorial> GetTutorialsByTitle(string title);

    }
}
