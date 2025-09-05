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

        Review GetReviewById(int reviewId);

        void AddReview(int patternId, int userId, string content, DateTime createdAt, int rating);

        void UpdateReview(int id, string content, DateTime createdAt, int rating);


    }
}
