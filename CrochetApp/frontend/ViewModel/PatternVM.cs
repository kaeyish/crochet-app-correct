using CrochetApp.backend.Domain.Model;
using CrochetApp.backend.Service;
using CrochetApp.frontend.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
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


        public ObservableCollection<Category> Categories { get; set; } = new();

        private Category selectedCategory;
        public Category SelectedCategory
        {
            get => selectedCategory;
            set
            {
                selectedCategory = value;
                OnPropertyChanged(nameof(SelectedCategory));
                ApplyFilterCategories(selectedCategory.Id);
            }
        }

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

                if (value != null){
                PatternImages = new ObservableCollection<string>(_patternService.GetImages(selectedPattern.Id));
                ((RelayCommand)UpdateCommand).RaiseCanExecuteChanged();
                ((RelayCommand)DeleteCommand).RaiseCanExecuteChanged();
                }
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

        private CategoryService _categoryService;   

        private readonly YarnService _yarnService;

        private TagService _tagService;

        private HookService _hookService;

        public PatternVM()
        {
            var app = (App)Application.Current;
            _patternService = app.PatternService;
            _categoryService = app.CategoryService;
            _yarnService = app.YarnService;
            _tagService = app.TagService;
            _hookService = app.HookService;

            SelectedLevel = "Level";
            SelectedStatus = "Status";

            AddCommand = new RelayCommand(OpenAddWindow);
            UpdateCommand = new RelayCommand(OpenUpdateWindow, () => SelectedPattern != null);
            DeleteCommand = new RelayCommand(DeletePattern, () => SelectedPattern != null);

            Categories = new ObservableCollection<Category>(_categoryService.GetAllCategories());

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
        }

        public List<Pattern> Reviewable(int projectId) {
            return _patternService.GetReviewable(projectId);
        }

        private void ApplyFilterCategories(int id)
        {
            FilteredPatterns.Clear();
            foreach (var pattern in _categoryService.GetPatternsByCategoryId(id))
                FilteredPatterns.Add(pattern);
        }

        private void OpenAddWindow()
        {
            var window = new AddPatternWindow();
            window.ShowDialog();
            LoadPatterns();
        }

        private void OpenUpdateWindow()
        {
            var window = new UpdatePatternWindow(this);
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

        internal void AddYarn(string name, string type, string material, string weight, string min, string max, string color)
        {
            _yarnService.AddYarn(name, type, material, int.Parse(weight), float.Parse(min, CultureInfo.InvariantCulture), float.Parse(max, CultureInfo.InvariantCulture), color);
        }




        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
