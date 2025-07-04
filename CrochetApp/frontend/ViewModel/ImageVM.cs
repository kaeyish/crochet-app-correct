using CrochetApp.backend.Domain.Model;
using CrochetApp.backend.Service;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CrochetApp.frontend.ViewModel
{
    public class ImageVM : INotifyPropertyChanged
    {

        private Image _idResult { get; set; }

        public Image IdResult
        {
            get { return _idResult; }

            set
            {
                _idResult = value;
                OnPropertyChanged("IdResult");
            }
        }
        private List<Image> _allResult { get; set; }

        public List<Image> AllResult
        {
            get { return _allResult; }

            set
            {
                _allResult = value;
                OnPropertyChanged("AllResult");
            }
        }

        private Image _urlResult { get; set; }

        public Image URLResult
        {
            get { return _urlResult; }
            set
            {
                _urlResult = value;
                OnPropertyChanged("URLResult");
            }
        }

        private ImageService _service;

        public ImageVM()
        {
            var app = (App)Application.Current;
            _service = app.ImageService;
            IdResult = _service.GetImageById(2);
            AllResult = _service.GetAllImages();
            URLResult = _service.GetImageByURL("slikazabrisanje.jpg");
            //_service.UpdateImage(1, "newlink.jpg");
            //_service.AddImage("newlink2.jpg");
            //var deleted = _service.DeleteImage(8);
        }









        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string v)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(v));
        }

    }
}
