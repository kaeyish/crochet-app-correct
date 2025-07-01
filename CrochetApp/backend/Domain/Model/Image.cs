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
        

        public string URL{get; set; }

        public Image() {
            URL = string.Empty;
        }

        public Image(int id, string url)
        {
            Id = id;
            URL = url;
        }
    }
}
