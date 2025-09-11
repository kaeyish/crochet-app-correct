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

namespace CrochetApp.frontend.ViewModel
{
    public class AddPatternVM
    {
        private readonly PatternService _patternService;
        private readonly CategoryService _categoryService;
        private readonly TagService _tagService;
        private readonly HookService _hookService;
        private readonly YarnService _yarnService;

        public List<string> Levels { get; } = new() { "Beginner", "Intermediate", "Advanced" };

        public List<string> ExistingTags { get; set; } = new();
        public List<string> ExistingCategories { get; set; } = new();
        public List<string> ExistingYarns { get; set; } = new();

        public ObservableCollection<Tag> SelectedTags { get; set; } = new();
        public ObservableCollection<Category> SelectedCategories { get; set; } = new();
        public ObservableCollection<Hook> SelectedHooks { get; set; } = new();
        public ObservableCollection<Yarn> SelectedYarns { get; set; } = new();

        public AddPatternVM()
        {
            var app = (App)Application.Current;
            _patternService = app.PatternService;
            _categoryService = app.CategoryService;
            _tagService = app.TagService;
            _hookService = app.HookService;
            _yarnService = app.YarnService;

            ExistingTags = _tagService.GetAllTags().Select(t => t.Text).ToList();
            ExistingCategories = _categoryService.GetAllCategories().Select(c => c.Name).ToList();
            ExistingYarns = _yarnService.GetAllYarns().Select(y => y.Name).ToList();
            var check = 1;
        }


        public void AddTag(string tagText)
        {
            if (string.IsNullOrWhiteSpace(tagText)) return;
            SelectedTags.Add(new Tag(0, tagText)); 

            OnPropertyChanged(nameof(SelectedTags));
        }

        public void AddCategory(string categoryName)
        {
            SelectedCategories.Add(new Category(0, categoryName));
            OnPropertyChanged(nameof(SelectedCategories));
        }

        public void AddHook(string sizeText)
        {
            if (string.IsNullOrWhiteSpace(sizeText)) return;
            if (!float.TryParse(sizeText, NumberStyles.Float, CultureInfo.InvariantCulture, out var size)) return;

            var hook = new Hook { Size = size };
            SelectedHooks.Add(hook);
            OnPropertyChanged(nameof(SelectedHooks));
        }

        public void SelectYarn(string yarn)
        {

            if (yarn == null) return;
            SelectedYarns.Add(_yarnService.GetYarnsByName(yarn).FirstOrDefault());

            OnPropertyChanged(nameof(SelectedYarns));
        }

        internal void OpenAddYarnWindow()
        {
            var addYarnWindow = new AddYarnWindow(this);
            addYarnWindow.ShowDialog();
        }

        public void SavePattern(string title, string desc, string level, DateTime date, string instructions) {
            int patternId = _patternService.AddPattern(title, desc, level, date,instructions);

            var pattern = _patternService.GetPatternByName(title);


            //filling M:N tables and checking for new data

            //prebaciti u name
            foreach (var category in SelectedCategories)
            {
                var existingCategory = category.Id;
                if (existingCategory == 0)
                {
                    existingCategory = _categoryService.AddCategory(category.Name);
                }
                _categoryService.ConnectToPattern(pattern.Id, existingCategory);
            }

            //prebaciti u text
            foreach (var tag in SelectedTags)
            {
                var existingTag = tag.Id;
                if (existingTag == 0)
                {
                    existingTag = _tagService.AddTag(tag.Text);
                }
                _tagService.ConnectToPattern(existingTag, pattern.Id);
            }

            //prebaciti toString u name + color idk
            foreach (var Yarn in SelectedYarns) {
                _yarnService.ConnectToPattern(Yarn.Id, pattern.Id);
            }

            foreach (var hook in SelectedHooks)
            {
                var existingHook = _hookService.GetAllBySize(hook.Size).FirstOrDefault();
                if (existingHook == null)
                {
                    existingHook.Id = _hookService.AddHook(hook.Size);
                }
                _hookService.ConnectToPattern(existingHook.Id, pattern.Id);
            }
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        internal void AddYarn(string name, string type, string material, string weight, string min, string max, string color)
        {
            _yarnService.AddYarn(name, type, material, int.Parse(weight), float.Parse(min, CultureInfo.InvariantCulture), float.Parse(max, CultureInfo.InvariantCulture), color);
        }


    }
}
