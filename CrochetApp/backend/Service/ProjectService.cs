using CrochetApp.backend.Domain.Model;
using CrochetApp.backend.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrochetApp.backend.Service
{
    public class ProjectService 
    {
        private IProjectRepository _projectRepository;

        public ProjectService(IProjectRepository projectRepository) {
            _projectRepository = projectRepository; 
        }

        public void AddProject(int? parentId, string name, string notes, string status, DateTime created, DateTime completed, float progress)
        {
            _projectRepository.AddProject(parentId.Value, name, notes, status, DateTimeFormatting.FormatSQL(created), DateTimeFormatting.FormatSQL(completed), progress);
        }

        public void DeleteProject(int projectId)
        {
            _projectRepository.DeleteProject(projectId);
        }

        public List<Project> GetAllProjects()
        {
            return _projectRepository.GetAllProjects();
        }

        public Project GetProjectById(int projectId)
        {
            return _projectRepository.GetProjectById(projectId);
        }

        public List<Project> GetProjectsByCompletionDate(DateTime date)
        {
            return _projectRepository.GetProjectsByCompletionDate(DateTimeFormatting.FormatSQL(date));
        }

        public List<Project> GetProjectsByCreationDate(DateTime date)
        {
            return _projectRepository.GetProjectsByCreationDate(DateTimeFormatting.FormatSQL(date));
        }

        public List<Project> GetProjectsByName(string name)
        {
            return _projectRepository.GetProjectsByName(name);
        }

        public List<Project> GetProjectsByProgress(float progress)
        {
            return _projectRepository.GetProjectsByProgress(progress);
        }

        public List<Project> GetProjectsByStatus(string status)
        {
            return _projectRepository.GetProjectsByStatus(status);
        }

        public void UpdateProject(int id, string name, string notes, string status, DateTime created, DateTime completed, float progress)
        {
            _projectRepository.UpdateProject(id, name, notes, status, DateTimeFormatting.FormatSQL(created), DateTimeFormatting.FormatSQL(completed), progress);
        }
    }
}
