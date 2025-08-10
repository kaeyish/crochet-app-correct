using CrochetApp.backend.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CrochetApp.frontend.ViewModel
{
    public class SuggestionVM
    {
        private SuggestionService _suggestionService;
        public SuggestionVM() {
            var app = (App)Application.Current;
            _suggestionService = app.SuggestionService;


            //testing

            //add
            _suggestionService.AddSuggestion("This is a test suggestion", 1);

            //update
            _suggestionService.UpdateSuggestion(1, "This is an updated test suggestion");

            //getters
            var result = _suggestionService.GetAllSuggestions();
            var res2 = _suggestionService.GetSuggestionById(1);
            var res1 = _suggestionService.GetSuggestionsByUserId(3);

            //delete
            _suggestionService.DeleteSuggestion(1);
            
            i = 2;

            //TESTED
            /*
             
             
             
             */



        }

    }
}
