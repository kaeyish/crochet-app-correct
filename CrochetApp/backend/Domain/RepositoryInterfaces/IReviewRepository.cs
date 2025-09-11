using CrochetApp.backend.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrochetApp.backend.Domain.RepositoryInterfaces
{
    public interface IReviewRepository
    {
        
        List<Review> GetAllReviews();

        void AddReview(int patternId, int userId, string content, DateTime createdAt, int rating);


    }
}
