using CrochetApp.backend.Domain.Model;
using CrochetApp.backend.Service;
using CrochetApp.frontend.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CrochetApp.frontend.ViewModel
{

    public class LibraryNode
    {
        public Library Library { get; set; }
        public ObservableCollection<Pattern> Patterns { get; set; } = new();
    }

    public class LibraryVM : INotifyPropertyChanged
    {
        private readonly LibraryService _libraryService;
        private readonly PatternService _patternService;

        public ObservableCollection<LibraryNode> Libraries { get; set; } = new();
        public ICommand AddLibraryCommand { get; }
        public ICommand AddPatternToLibraryCommand { get; }

        private LibraryNode? _selectedLibrary;
        public LibraryNode? SelectedLibrary
        {
            get => _selectedLibrary;
            set
            {
                if (_selectedLibrary != value)
                {
                    _selectedLibrary = value;
                    OnPropertyChanged(nameof(SelectedLibrary));
                    ((RelayCommand)AddPatternToLibraryCommand).RaiseCanExecuteChanged();
                }
            }
        }

        private const int DefaultUserId = 3; 

        public LibraryVM()
        {
            var app = (App)Application.Current;
            _libraryService = app.LibraryService;
            _patternService = app.PatternService;

            AddLibraryCommand = new RelayCommand(OpenAddLibraryWindow);
            AddPatternToLibraryCommand = new RelayCommand(OpenAddPatternWindow, () => SelectedLibrary != null);

            LoadLibraries();
        }

        private void LoadLibraries()
        {
            Libraries.Clear();
            var libs = _libraryService.GetLibrariesByUser(DefaultUserId);
            foreach (var lib in libs)
            {
                var patterns = _patternService.GetPatternsInLibrary(lib.Id);
                Libraries.Add(new LibraryNode
                {
                    Library = lib,
                    Patterns = new ObservableCollection<Pattern>(patterns)
                });
            }

            OnPropertyChanged(nameof(Libraries));
        }

        private void OpenAddLibraryWindow()
        {
            var window = new AddLibraryWindow(this);
            window.ShowDialog();
        }

        private void OpenAddPatternWindow()
        {
            var window = new AddPatternToLibraryWindow(this, SelectedLibrary!.Library.Id);
            window.ShowDialog();
        }

        public void CreateLibrary(string name, string description, List<Pattern> selectedPatterns)
        {
            if (string.IsNullOrWhiteSpace(name)) return;
            
            int libraryId = _libraryService.AddLibrary(name, description, DefaultUserId);

            LinkPatternsToLibrary(libraryId, selectedPatterns);
        }

        public void LinkPatternsToLibrary(int libraryId, List<Pattern> selectedPatterns)
        {
            foreach (var pattern in selectedPatterns)
            {
                _libraryService.ConnectPatternToLibrary(pattern.Id, libraryId, DefaultUserId);
            }
            LoadLibraries();
        }



        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

}
