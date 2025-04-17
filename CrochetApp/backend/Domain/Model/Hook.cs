using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrochetApp.backend.Domain.Model
{
    public class Hook
    {
        public int Id { get; set; }
        public float Size{ get; set; }

        public Hook() { }

        public Hook(int id, float size) {  Id = id; Size = size; }
    }
}
