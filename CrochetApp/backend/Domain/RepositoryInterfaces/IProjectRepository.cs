using CrochetApp.backend.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrochetApp.backend.Domain.RepositoryInterfaces
{
    public interface IProjectRepository
    {
        List<Project> GetAllProjects();

        Project GetProjectById(int projectId);

        void AddProject(int parentId, string name, string notes, string status, string created, string completed, double progress);

        void UpdateProject(int id, string name, string notes, string status, string created, string completed, double progress);

        void DeleteProject(int projectId);

        List<Project> GetProjectsByProgress(double progress);

        List<Project> GetProjectsByStatus(string status);
        List<Project> GetProjectsByCreationDate(string date);

        List<Project> GetProjectsByCompletionDate(string date);

        List<Project> GetProjectsByName(string name);


    }
}
