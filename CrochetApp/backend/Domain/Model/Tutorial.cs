using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrochetApp.backend.Domain.Model
{
    public class Tutorial
    {
        public int Id { get; set; }

        public string Title{ get; set; }

        public string Text { get; set; }

        // string?, proveri verziju c# !!!!!!
        public string VideoLink { get; set; }

        public Level Level { get; set; }

        public int CreatorId { get; set; }

        public Tutorial() { }

        public Tutorial(int id, string title, string text, string videoLink, Level level, int creatorId)
        {
            Id = id;
            Title = title;
            Text = text;
            VideoLink = videoLink;
            Level = level;
            CreatorId = creatorId;
        }


    }
}
