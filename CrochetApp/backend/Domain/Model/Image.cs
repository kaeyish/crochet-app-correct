using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrochetApp.backend.Domain.Model
{
    public class Image
    {

        public int Id {  get; set; }
        

        public string Link {get; set; }

        public Image() { }

        public Image(int id, string link)
        {
            Id = id;
            Link = link;
        }
    }
}
