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
    public class ProjectNode
    {
        public Project Project { get; set; }
        public ObservableCollection<Project> Children { get; set; } = new();
    }

    public class ProjectVM : INotifyPropertyChanged
    {

        public ICommand AddBaseProjectCommand { get; }
        public ICommand AddChildProjectCommand { get; }
        public ICommand AddReviewCommand { get; }

        private ProjectNode selectedProjectNode;
        public ProjectNode SelectedProjectNode
        {
            get => selectedProjectNode;
            set
            {
                selectedProjectNode = value;
                OnPropertyChanged(nameof(SelectedProjectNode));
                ((RelayCommand)AddChildProjectCommand).RaiseCanExecuteChanged();
                ((RelayCommand)AddReviewCommand).RaiseCanExecuteChanged();
            }
        }


        private readonly ProjectService _projectService;

        public ObservableCollection<ProjectNode> ProjectHierarchy { get; set; } = new();

        public ProjectVM()
        {
            var app = (App)Application.Current;
            _projectService = app.ProjectService;
            AddBaseProjectCommand = new RelayCommand(() => OpenAddProjectWindow(null));
            AddChildProjectCommand = new RelayCommand(() => OpenAddProjectWindow(SelectedProjectNode?.Project.Id), () => SelectedProjectNode != null);
            AddReviewCommand = new RelayCommand(() => OpenReviewableWindow(SelectedProjectNode?.Project.Id), () => SelectedProjectNode != null);

            LoadHierarchy();
        }

        private void OpenAddProjectWindow(int? parentId)
        {
            var window = new AddProjectWindow(this, parentId);
            window.ShowDialog();
            LoadHierarchy();
        }
        private void OpenReviewableWindow(int? projectId)
        {
            var window = new ReviewableWindow(projectId.Value);
            window.ShowDialog();
        }

        public void CreateProject(string title, int? parentId, List<Pattern> selectedPatterns)
        {
            if (string.IsNullOrWhiteSpace(title)) return;

            int projectId = _projectService.AddProject(parentId, title);

            foreach (var pattern in selectedPatterns)
            {
                _projectService.ConnectToPattern(pattern.Id, projectId);
            }

            LoadHierarchy();
        }


        private void LoadHierarchy()
        {
            ProjectHierarchy.Clear();

            var mainProjects = _projectService.GetAllBaseProjects();
            foreach (var main in mainProjects)
            {
                var children = _projectService.GetAllChildren(main.Id);
                ProjectHierarchy.Add(new ProjectNode
                {
                    Project = main,
                    Children = new ObservableCollection<Project>(children)
                });
            }

            OnPropertyChanged(nameof(ProjectHierarchy));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
