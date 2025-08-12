using CrochetApp.backend.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CrochetApp.frontend.ViewModel
{
    public class TutorialVM : INotifyPropertyChanged
    {
        private TutorialService _tutorialService;

        public TutorialVM() {
            var app = (App)Application.Current;
            _tutorialService = app.TutorialService;

            //TESTED
            /*
             _tutorialService.AddTutorial("THIS IS A TUTORIAL", "EMPTY", "Beginner", "NEW", 1);
            _tutorialService.AddTutorial("THIS IS SECOND TUTORIAL I WILL ADD", "EMPTY", "Beginner", "Single Crochet", 2);
            _tutorialService.AddTutorial("THIS IS THIRD TUTORIAL I WILL ADD", "EMPTY", "Beginner", "Double Crochet", 2);
            _tutorialService.UpdateTutorial(1, "THIS IS UPDATED TUTORIAL", "EMPTY", "Beginner", "UPDATED");
            _tutorialService.DeleteTutorial(j);
            var allTutorials = _tutorialService.GetAllTutorials();
            var tutorialById = _tutorialService.GetTutorialById(1);
            var tutorialsByUserId = _tutorialService.GetTutorialsByUserId(1);
            var tutorialsByDifficulty = _tutorialService.GetTutorialsByDifficulty("Beginner");
            var tutorialsByTitle = _tutorialService.GetTutorialsByTitle("UPDATED");

             */


        }


        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string v)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(v));
        }





    }
}
