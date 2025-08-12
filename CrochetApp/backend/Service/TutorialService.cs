using CrochetApp.backend.Domain.RepositoryInterfaces;
using CrochetApp.backend.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrochetApp.backend.Service
{
    public class TutorialService
    {
        private readonly ITutorialRepository _tutorialRepository;
        public TutorialService(ITutorialRepository tutorialRepository)
        {
            _tutorialRepository = tutorialRepository;
        }
        public List<Tutorial> GetAllTutorials()
        {
            return _tutorialRepository.GetAllTutorials();
        }
        public Tutorial GetTutorialById(int id)
        {
            return _tutorialRepository.GetTutorialById(id);
        }
        public void AddTutorial(string text, string link, string diff, string title, int user)
        {
            _tutorialRepository.AddTutorial(text, link, diff, title, user);
        }
        public void UpdateTutorial(int id, string text, string link, string diff, string title)
        {
            _tutorialRepository.UpdateTutorial(id, text, link, diff, title);
        }
        public void DeleteTutorial(int id)
        {
            _tutorialRepository.DeleteTutorial(id);
        }
        public List<Tutorial> GetTutorialsByUserId(int userId)
        {
            return _tutorialRepository.GetTutorialsByUserId(userId);
        }
        public List<Tutorial> GetTutorialsByDifficulty(string difficulty)
        {
            return _tutorialRepository.GetTutorialsByDifficulty(difficulty);
        }
        public List<Tutorial> GetTutorialsByTitle(string title)
        {
            return _tutorialRepository.GetTutorialsByTitle(title);
        }



    }
}
