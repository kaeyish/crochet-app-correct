using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrochetApp.backend.Domain.Model
{
    public class Yarn
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Type { get; set; }

        public string Material { get; set; }

        public string Color { get; set; }

        public int Weight { get; set; }

        public float MinSize{ get; set; }

        public float MaxSize { get; set; }

        public Yarn() { }

        public Yarn(int id, string name, string type, string material, string color, int weight, float minSize, float maxSize)
        {
            Id = id;
            Name = name;
            Type = type;
            Material = material;
            Color = color;
            Weight = weight;
            MinSize = minSize;
            MaxSize = maxSize;
        }
    }
}
