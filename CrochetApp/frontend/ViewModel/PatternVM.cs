using CrochetApp.backend.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CrochetApp.frontend.ViewModel
{
    public class PatternVM
    {
        private PatternService _patternService;

        public PatternVM()
        {
            var app = (App)Application.Current;
            _patternService = app.PatternService;

            int i = 1;
            //TESTING 

            
            //update
            //_patternService.UpdatePattern(1, "Updated Test Pattern ", "This is an updated test pattern description", "Intermediate", DateTime.Now.AddDays(1), 4.8f, "Updated instructions for the test pattern", "Approved");

//            //getters
            //var patternsByDate = _patternService.GetPatternsByDate(DateTime.Now);
            /*var patternsByRating = _patternService.GetPatternsByRating(4.5f);

     */       i++;



            //tested
            /*            
             _patternService.DeletePattern(1);
             _patternService.AddPattern("Test Pattern ", "This is a test pattern description", "Beginner", DateTime.Now, 4.5f, "Instructions for the test pattern", "Rejected", 2);
            _patternService.AddPattern("Test Pattern ", "This is a test pattern description", "Beginner", DateTime.Now, 4.5f, "Instructions for the test pattern", "Rejected", 3);
            var patternById = _patternService.GetPatternById(5);
            var patternByName = _patternService.GetPatternByName("PLACEHOLDER PATTERN");
            var patternsByStatus = _patternService.GetPatternsByStatus("Rejected")
            var patternsByLevel = _patternService.GetPatternsByLevel("Beginner");;

            var rez = _patternService.GetAllPatterns();
             
             */
        }

    }
}
