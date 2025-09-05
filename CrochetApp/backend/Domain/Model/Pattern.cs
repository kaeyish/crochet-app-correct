using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrochetApp.backend.Domain.Model
{
    public class Pattern
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }
        public string Instructions { get; set; }

        public Level Level { get; set; }

        public DateTime Date { get; set; }

        public double Rating { get; set; }

        public Status Status { get; set; } 


        public Pattern() { }

        public Pattern (int id, string title, string description, string level,  DateTime date, double rating, string instructions, string status)
        {
            Id = id;
            Title = title;
            Description = description;
            Instructions = instructions;
            Level = (Level)Enum.Parse(typeof(Level), level, true);
            Date = date;
            Rating = rating;
            Status = (Status)Enum.Parse(typeof(Status), status, true);
        }
    }
}
