using CrochetApp.backend.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CrochetApp.frontend.ViewModel
{
    internal class YarnVM : INotifyPropertyChanged
    {
        private YarnService _yarnService;

        public YarnVM()
        {
            var app = (App)Application.Current;
            _yarnService = app.YarnService;

            //'type' means weight cateogry, for example: lace to bulky including sport, DK, worsted, and bulky. 
            

            //testing
            int breakpoint = 1;
            /*
             MAKE UPDATE WORK FOR ANY PARAMETER AND ALL COMBOS OF PARAMS
            */


            //TESTED
            /*
            var testresult = _yarnService.GetYarnsByType("bulky");
            var testresult1 = _yarnService.GetYarnsByWeight(100);
            _yarnService.AddYarn("alize vellutto", "bulky", "chenile", 100, 6, 8, "red");
            _yarnService.DeleteYarn(1);
            var testresult = _yarnService.GetAllYarns();
            var testresult1 = _yarnService.GetYarnById(2);
            var test = _yarnService.GetYarnsByColor("red");
            var testresult = _yarnService.GetYarnsByMaterial("Chenile");
            var test1 = _yarnService.GetYarnsByName("alize vellutto");
            var test2 = _yarnService.GetYarnsBySize(7.5f);
             */
        }


        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string v)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(v));
        }

    }
}
