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
    internal class ProjectVM : INotifyPropertyChanged
    {
        private ProjectService _projectService;

        public ProjectVM()
        {
            var app = (App)Application.Current;
            _projectService = app.ProjectService;

            //TESTED
            // _projectService.AddProject(1, "Test Project", "This is a test project", "Ongoing", DateTime.Now, DateTime.Now.AddDays(30), 0.5f);
            //_projectService.AddProject(1, "2ND TO DELTE Test Project", "This is a test project", "Ongoing", DateTime.Now, DateTime.Now.AddDays(30), 0.5f);
            //_projectService.AddProject(1, "2ND TO DELTE Test Project", "This is a test project", "Ongoing", DateTime.Now, DateTime.Now.AddDays(30), 0.5f);
            //_projectService.UpdateProject(3, "Updated Test Project", "This is an updated test project", "Ongoing", DateTime.Now, DateTime.Now.AddDays(30), 0.75f);
            //_projectService.DeleteProject(3);
            //var projectById = _projectService.GetProjectById(3);
            //var allProjects = _projectService.GetAllProjects();
            //var projectsByCompletionDate = _projectService.GetProjectsByCompletionDate(DateTime.Now.AddDays(30));
            //var projectsByCreationDate = _projectService.GetProjectsByCreationDate(DateTime.Now);
            //var projectsByName = _projectService.GetProjectsByName("PLACEHOLDER PROJECT");
            //var projectsByProgress = _projectService.GetProjectsByProgress(0f);
            //var projectsByStatus = _projectService.GetProjectsByStatus("Ongoing");
  




        }









        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string v)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(v));
        }
    }
}
