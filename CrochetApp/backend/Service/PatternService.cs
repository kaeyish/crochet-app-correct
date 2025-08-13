using CrochetApp.backend.Domain.Model;
using CrochetApp.backend.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrochetApp.backend.Service
{
    public class PatternService
    {
        private readonly IPatternRepository _patternRepository;
        public PatternService(IPatternRepository patternRepository)
        {
            _patternRepository = patternRepository;
        }
        public void AddPattern(string title, string desc, string level, DateTime date, float rating, string inst, string status, int requestId)
        {
            _patternRepository.AddPattern(title, desc, level, DateTimeFormatting.FormatSQL(date), rating, inst, status, requestId);
        }
        public void DeletePattern(int id)
        {
            _patternRepository.DeletePattern(id);
        }
        public void UpdatePattern(int id, string title, string desc, string level, DateTime date, float rating, string inst, string status)
        {
            _patternRepository.UpdatePattern(id, title, desc, level, DateTimeFormatting.FormatSQL(date), rating, inst, status);
        }

        public List<Pattern> GetPatterns()
        {
            return _patternRepository.GetPatterns();
        }

        public Pattern GetPatternById(int id)
        {
            return _patternRepository.GetPatternById(id);
        }

        public Pattern GetPatternByName(string name)
        {
            return _patternRepository.GetPatternByName(name);
        }

        public List<Pattern> GetPatternsByStatus(string statusId)
        {
            return _patternRepository.GetPatternsByStatus(statusId);
        }

        public List<Pattern> GetPatternsByLevel(string level)
        {
            return _patternRepository.GetPatternsByLevel(level);
        }

        public List<Pattern> GetPatternsByDate(DateTime date)
        {
            return _patternRepository.GetPatternsByDate(DateTimeFormatting.FormatSQL(date));
        }

        public List<Pattern> GetPatternsByRating(float rating)
        {
            return _patternRepository.GetPatternsByRating(rating);
        }
    }
}
