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
    public class HookVM : INotifyPropertyChanged
    {

        HookService _hookService;

        public HookVM()
        {
            var app = (App)Application.Current;
            _hookService = app.HookService;

            /*TESTED
            var AllHooks = _hookService.GetAll();
            var HookById = _hookService.GetHookById(1);
            var HooksBySize = _hookService.GetAllBySize(6f);
            _hookService.AddHook(5.0f);
            _hookService.AddHook(5.0f);
            _hookService.UpdateHook(3.5f, 1);
            _hookService.DeleteHook(22);
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
