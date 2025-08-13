using CrochetApp.backend.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CrochetApp.frontend.ViewModel
{
    public class RequestVM
    {
        private readonly RequestService _requestService;

        public RequestVM()
        {
            var app = (App)Application.Current;
            _requestService = app.RequestService;

            //tested
            /*
            _requestService.DeleteRequest(21);
            _requestService.AddRequest(DateTime.Now, "Approved", 2);
            _requestService.UpdateRequest(1, DateTime.Now, "Rejected", 3);
            var rez = _requestService.GetRequestsByCreator(1);
            var rez1 = _requestService.GetRequestsByStatus("Approved");
            var rez2 = _requestService.GetRequestsByDate("13-aug-2025");
            var rez3 = _requestService.GetRequestById(1);             
             */
        }


    }
}
