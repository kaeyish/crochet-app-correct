using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrochetApp.backend.Domain.Model
{
    public class Library
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public DateTime CreatedDate { get; set; }

        //WIP for now, maybe better to get lazy loading instead 
        public int UserId { get; set; }

        public Library() { }
    
        public Library(int id, string name, string description, DateTime createdDate, int userId)
        {
            Id = id;
            Name = name;
            Description = description;
            CreatedDate = createdDate;
            UserId = userId;
        }
    }
}
