using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrochetApp.backend.Domain.Model
{
    public class Technique
    {

        public int Id { get; set; }
        
        public string Name { get; set; }

        public Level Level { get; set; }

        public Technique() { }

        public Technique(int id, string name, string level)
        {
            Id = id;
            Name = name;
            Level = (Level)Enum.Parse(typeof(Level), level, true);
        }

    }
}
