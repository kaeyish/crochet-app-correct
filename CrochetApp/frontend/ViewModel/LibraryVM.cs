using CrochetApp.backend.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CrochetApp.frontend.ViewModel
{

    public class LibraryVM
    {
        private readonly LibraryService _libraryService;

        public LibraryVM() {
            var app = (App)Application.Current;
            _libraryService = app.LibraryService;
            

            //TESTED
            /* 
            _libraryService.UpdateLibrary(1, "THIS IS UPDATED LIBRARY", "THIS IS UPDATE DESC", DateTime.Now);
            var rez = _libraryService.GetAllLibraries();
            _libraryService.AddLibrary("THIS IS TEST LIBRARY", "THIS IS DESC", DateTime.Now, 2);
            _libraryService.AddLibrary("THIS IS FSLKFDSJDKSJD LIBRARY", "THIS IS DESC", DateTime.Now, 2);
            _libraryService.DeleteLibrary(1);
            var rezultat = _libraryService.GetLibraryById(1);
            var rezultat1 = _libraryService.GetLibraryByName("THIS IS UPDATED LIBRARY");
             */

        }

    }
}
