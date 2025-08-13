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

        public PatternVM(PatternService patternService)
        {
            var app = (App)Application.Current;
            _patternService = app.PatternService;

            int i = 1;
            //TESTING 


        }

    }
}
