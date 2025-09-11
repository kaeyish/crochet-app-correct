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

        int AddProject(int? parentId, string name, string notes, string status, string created, string completed, double progress);

        void UpdateProject(int id, string name, string notes, string status, string created, string completed, double progress);

        void DeleteProject(int projectId);

        void ConnectToPattern(int patternId, int projectId);

        List<Project> GetAllProjects();

        List<Project> GetAllBaseProjects();
        Project GetProjectById(int projectId);

        List<Project> GetAllChildren(int projectId);
        List<Project> GetProjectsByProgress(double progress);

        List<Project> GetProjectsByStatus(string status);
        List<Project> GetProjectsByCreationDate(string date);

        List<Project> GetProjectsByCompletionDate(string date);

        List<Project> GetProjectsByName(string name);


    }
}
