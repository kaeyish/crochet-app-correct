using CrochetApp.backend.Domain.Model;
using CrochetApp.backend.Service;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CrochetApp.frontend.ViewModel
{
    public class TagVM : INotifyPropertyChanged
    {

        private Tag _idResult { get; set; }
        
        public Tag IdResult {
            get { return _idResult; }

            set { 
                _idResult = value;
                OnPropertyChanged("IdResult");
            }
        }
        private List<Tag> _allResult { get; set; }

        public List<Tag> AllResult {
            get { return _allResult; }

            set { 
                _allResult = value;
                OnPropertyChanged("AllResult");
            }
        }

        private Tag _nameResult { get; set; }

        public Tag NameResult {
            get { return _nameResult; }
            set {
                _nameResult = value;
                OnPropertyChanged("NameResult");
            }
        }

        private TagService _service;

        public TagVM() {
            var app = (App)Application.Current;
            _service = app.TagService;
            IdResult = _service.GetTagById(1);
            AllResult = _service.GetAllTags();
            NameResult = _service.GetTagByName("spring");
         //   _service.AddTag("Folk");
         //   _service.UpdateTag(2, "New year");
         //   _service.DeleteTag(4);
         //   AllResult = _service.GetAllTags();
        }





        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string v)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(v));
        }

    }
}
