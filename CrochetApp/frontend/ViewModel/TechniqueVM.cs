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
    public class TechniqueVM :INotifyPropertyChanged
    {

        private TechniqueService _techniqueService;

        public TechniqueVM()
        {
            var app = (App)Application.Current;
            _techniqueService = app.TechniqueService;
            
            
            //TESTED
            /*
             var testresult = _techniqueService.GetTechniqueByName("single crochet");
            var testresult3 = _techniqueService.GetTechniqueById(1);
            var testresult1 = _techniqueService.GetAllTechniques();
            var testresult2 = _techniqueService.GetTechniquesByLevel("Beginner");
            _techniqueService.AddTechnique("Increase", "Beginner");
            _techniqueService.DeleteTechnique("Chain stitch");
            _techniqueService.DeleteTechniqueById(1);
            _techniqueService.UpdateTechnique(2, "Chain stitch", "Beginner");
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
