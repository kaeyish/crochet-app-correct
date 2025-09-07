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
    public class PatternVM : INotifyPropertyChanged
    {
        private readonly PatternService _patternService;

        public ObservableCollection<Pattern> AllPatterns { get; set; } = new();
        public ObservableCollection<Pattern> FilteredPatterns { get; set; } = new();

        public List<string> Levels { get; } = new() { "Level", "Beginner", "Intermediate", "Advanced" };
        public List<string> Statuses { get; } = new() { "Status", "Approved", "Rejected", "Pending" };

        public ICommand AddCommand { get; }
        public ICommand UpdateCommand { get; }
        public ICommand DeleteCommand { get; }

        private string selectedLevel = "All";
        public string SelectedLevel
        {
            get => selectedLevel;
            set { selectedLevel = value; OnPropertyChanged(nameof(SelectedLevel)); ApplyFilter(); }
        }

        private string selectedStatus = "All";
        public string SelectedStatus
        {
            get => selectedStatus;
            set { selectedStatus = value; OnPropertyChanged(nameof(SelectedStatus)); ApplyFilter(); }
        }

        private Pattern selectedPattern;
        public Pattern SelectedPattern
        {
            get => selectedPattern;
            set
            {
                selectedPattern = value;
                OnPropertyChanged(nameof(SelectedPattern));
                PatternImages = new ObservableCollection<string>(_patternService.GetImages(selectedPattern.Id));
                ((RelayCommand)UpdateCommand).RaiseCanExecuteChanged();
                ((RelayCommand)DeleteCommand).RaiseCanExecuteChanged();
            }
        }
        private ObservableCollection<string> _patternImages = new();

        public ObservableCollection<string> PatternImages
        {
            get => _patternImages;
            set
            {
                _patternImages = value;
                OnPropertyChanged(nameof(PatternImages));
            }
        }


        public PatternVM()
        {
            var app = (App)Application.Current;
            _patternService = app.PatternService;

            SelectedLevel = "Level";
            SelectedStatus = "Status";

            AddCommand = new RelayCommand(OpenAddWindow);
            UpdateCommand = new RelayCommand(OpenUpdateWindow, () => SelectedPattern != null);
            DeleteCommand = new RelayCommand(DeletePattern, () => SelectedPattern != null);

            LoadPatterns();
        }

        private void LoadPatterns()
        {
            AllPatterns.Clear();
            foreach (var pattern in _patternService.GetAllPatterns())
                AllPatterns.Add(pattern);

            ApplyFilter();
        }

        private void ApplyFilter()
        {
            var filtered = AllPatterns.Where(p =>
                (SelectedLevel == "Level" || p.Level.ToString() == SelectedLevel) &&
                (SelectedStatus == "Status" || p.Status.ToString() == SelectedStatus)).ToList();

            FilteredPatterns.Clear();
            foreach (var pattern in filtered)
                FilteredPatterns.Add(pattern);
            int check = 0;
        }

        private void OpenAddWindow()
        {
            var window = new AddPatternWindow();
            window.ShowDialog();
            LoadPatterns();
        }

        private void OpenUpdateWindow()
        {
            var window = new UpdatePatternWindow(SelectedPattern);
            window.ShowDialog();
            LoadPatterns();
        }

        private void DeletePattern()
        {
            if (SelectedPattern == null) return;
            _patternService.DeletePattern(SelectedPattern.Id);
            SelectedPattern = null;
            LoadPatterns();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
