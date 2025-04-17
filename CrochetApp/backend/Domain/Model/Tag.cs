using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrochetApp.backend.Domain.Model
{
    public class Tag
    {

        public int Id { get; set; }
        public string Text { get; set; }

        public Tag() { }

        public Tag(int id, string text)
        {
            Id = id;
            Text = text;
        }
    }
}
