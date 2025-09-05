using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrochetApp.backend.Domain.Model
{
    public class Review
    {

        //other ids are only for linking purposes and validation thus not included
        public int Id { get; set; }

        public int PatternId { get; set; }

        public int UserId { get; set; }

        public string Content { get; set; }


        public DateTime CreatedAt { get; set; }
        public int Rating { get; set; }

        public Review() { }

        public Review(int id, int patternId, int userId, string content, DateTime createdAt, int rating)
        {
            Id = id;
            PatternId = patternId;
            UserId = userId;
            Content = content;
            CreatedAt = createdAt;
            Rating = rating;
        }

    }
}
