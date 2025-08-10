using CrochetApp.backend.Domain.Model;
using CrochetApp.backend.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrochetApp.backend.Service
{
    public class SuggestionService
    {
        private readonly ISuggestionRepository _suggestionRepository;
        public SuggestionService(ISuggestionRepository suggestionRepository)
        {
            _suggestionRepository = suggestionRepository;
        }
        public void AddSuggestion(string suggestionText, int userId)
        {
            _suggestionRepository.AddSuggestion(userId, suggestionText);
        }
        public void DeleteSuggestion(int suggestionId)
        {
            _suggestionRepository.DeleteSuggestion(suggestionId);
        }

        public List<Suggestion> GetAllSuggestions()
        {
            return _suggestionRepository.GetAllSuggestions();
        }

        public List<Suggestion> GetSuggestionsByUserId(int userId)
        {
            return _suggestionRepository.GetSuggestionsByUserId(userId);
        }

        public Suggestion GetSuggestionById(int suggestionId)
        {
            return _suggestionRepository.GetSuggestionById(suggestionId);
        }

        public void UpdateSuggestion(int suggestionId, string newText)
        {
            _suggestionRepository.UpdateSuggestion(suggestionId, newText);
        }
    }
}
