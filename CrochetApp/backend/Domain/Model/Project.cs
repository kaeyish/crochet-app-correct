using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrochetApp.backend.Domain.Model
{
    public class Project
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        public string Notes { get; set; }

        public ProjectStatus Status { get; set; }

        public DateTime Created { get; set; }

        public DateTime Completed{ get; set; }

        //osmisliti ovaj deo nesto bolje, note u datamodeleru
        public float Progress { get; set; }

        public Project() { }

        public Project (int id, string name, string notes, string status, DateTime created, DateTime completed, float progress)
        {
            Id = id;
            Name = name;
            Notes = notes;
            Status = (ProjectStatus)Enum.Parse(typeof(ProjectStatus), status, true);
            Created = created;
            Completed = completed;
            Progress = progress;
        }
    }
}
