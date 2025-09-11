using CrochetApp.backend.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrochetApp.backend.Domain.RepositoryInterfaces
{
    public interface IPatternRepository
    {
        List<Pattern> GetAllPatterns();

        Pattern GetPatternById(int id);

        Pattern GetPatternByName(string name);

        List<Pattern> GetPatternsByStatus(string statusId);

        List<Pattern> GetPatternsByLevel(string level);

        List<Pattern> GetPatternsByDate(string date);

        List<Pattern> GetPatternsByRating(double rating);

        List<Pattern> GetPatternsInLibrary(int libraryId);

        List<Pattern> GetReviewable(int projectId, int userId);

        int AddPattern(string title, string desc, string level, string date, double rating, string inst, string status);

        void DeletePattern(int id);

        void UpdatePattern(int id, string title, string desc, string level, string date, double rating, string inst, string status);

        List<string> GetImages(int id);


    }
}
