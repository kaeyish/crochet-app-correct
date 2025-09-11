using CrochetApp.backend.Domain.Model;
using CrochetApp.backend.Service;
using CrochetApp.frontend.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CrochetApp.frontend.ViewModel
{
    public class TutorialNode
    {
        public Tutorial Tutorial { get; set; }
        public ObservableCollection<Technique> Techniques { get; set; } = new();
    }
    public class TutorialVM : INotifyPropertyChanged
    {
        private readonly TutorialService _tutorialService;
        private readonly TechniqueService _techniqueService;

        public ObservableCollection<TutorialNode> Tutorials { get; set; } = new();
        public ICommand AddTutorialCommand { get; }

        public TutorialVM()
        {
            var app = (App)Application.Current;
            _tutorialService = app.TutorialService;
            _techniqueService = app.TechniqueService;

            AddTutorialCommand = new RelayCommand(OpenAddTutorialWindow);
            LoadTutorials();
        }

        private void LoadTutorials()
        {
            Tutorials.Clear();
            var allTutorials = _tutorialService.GetAllTutorials();
            foreach (var tut in allTutorials)
            {
                var techniques = _techniqueService.GetTechniquesForTutorial(tut.Id);
                Tutorials.Add(new TutorialNode
                {
                    Tutorial = tut,
                    Techniques = new ObservableCollection<Technique>(techniques)
                });
            }

            OnPropertyChanged(nameof(Tutorials));
        }

        private void OpenAddTutorialWindow()
        {
            var window = new AddTutorialWindow(this);
            window.ShowDialog();
        }

        public void CreateTutorial(string title, string difficulty, string videoUrl, string text, List<Technique> selectedTechniques)
        {
            
            int tutorialId = _tutorialService.AddTutorial(text, videoUrl,difficulty,title, 1);

            foreach (var tech in selectedTechniques)
            {
                _tutorialService.ConnectTechniqueToTutorial(tech.Id, tutorialId, 1);
            }

            LoadTutorials();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

}
