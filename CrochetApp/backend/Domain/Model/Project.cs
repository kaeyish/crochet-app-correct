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
        public int? ParentId { get; set; }

        public string Name { get; set; }
        
        public string? Notes { get; set; }

        public ProjectStatus Status { get; set; }

        public DateTime Created { get; set; }

        public DateTime? Completed{ get; set; }

        //osmisliti ovaj deo nesto bolje, note u datamodeleru
        public float Progress { get; set; }

        public Project() { }

        public Project (int id, int?parentId, string name, string? notes, string status, DateTime created, DateTime? completed, float progress)
        {
            Id = id;
            if (parentId.HasValue && parentId.Value > 0)
            {
                ParentId = parentId;
            }
            else
            {
                ParentId = null; 
            }
            Name = name;
            Notes = notes;
            Status = (ProjectStatus)Enum.Parse(typeof(ProjectStatus), status, true);
            Created = created;
            
            if(completed.HasValue && completed.Value > DateTime.MinValue)
            {
                Completed = completed;
            }
            else
                Completed = null;
            
            if (notes == null)
            {
                notes = string.Empty;
            }
            else notes = notes.Trim();

            Progress = progress;
        }
    }
}
