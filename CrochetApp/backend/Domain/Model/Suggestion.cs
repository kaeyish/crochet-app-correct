using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrochetApp.backend.Domain.Model
{
    public class Suggestion
    {
        public int Id { get; set; }
        public string Text{ get; set; }

        public int UserId { get; set; }

        public Suggestion() { }

        public Suggestion(int id, string text, int userid)
        {
            Id = id;
            Text = text;
            UserId = userid;
        }

    }
}
