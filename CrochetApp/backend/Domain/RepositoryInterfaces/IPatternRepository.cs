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
        List<Pattern> GetPatterns();

        Pattern GetPatternById(int id);

        Pattern GetPatternByName(string name);

        List<Pattern> GetPatternsByStatus(string statusId);

        List<Pattern> GetPatternsByLevel(string level);

        List<Pattern> GetPatternsByDate(string date);

        List<Pattern> GetPatternsByRating(float rating);

        void AddPattern(string title, string desc, string level, string date, float rating, string inst, string status, int requestId);

        void DeletePattern(int id);

        void UpdatePattern(int id, string title, string desc, string level, string date, float rating, string inst, string status);


    }
}
