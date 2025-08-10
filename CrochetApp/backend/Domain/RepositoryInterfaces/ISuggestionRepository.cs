using CrochetApp.backend.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrochetApp.backend.Domain.RepositoryInterfaces
{
    public interface ISuggestionRepository
    {
        void AddSuggestion(int userId, string suggestionText);
        void DeleteSuggestion(int suggestionId);
        List<Suggestion> GetAllSuggestions();
        List<Suggestion> GetSuggestionsByUserId(int userId);
        Suggestion GetSuggestionById(int suggestionId);
        void UpdateSuggestion(int suggestionId, string newText);

    }
}
