using CrochetApp.backend.Domain.Model;
using CrochetApp.backend.Service;
using CrochetApp.frontend.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CrochetApp.frontend.ViewModel
{

 public class CategoryVM : INotifyPropertyChanged
{
    private readonly CategoryService _categoryService;

    public ObservableCollection<Category> Categories { get; set; } = new();

    public ICommand AddCommand { get; }
    public ICommand UpdateCommand { get; }
    public ICommand DeleteCommand { get; }

    private Category selectedCategory;
    public Category SelectedCategory
    {
        get => selectedCategory;
        set
        {
            selectedCategory = value;
            OnPropertyChanged(nameof(SelectedCategory));
            ((RelayCommand)UpdateCommand).RaiseCanExecuteChanged();
            ((RelayCommand)DeleteCommand).RaiseCanExecuteChanged();
        }
    }

        private ObservableCollection<Pattern> patterns;

        public ObservableCollection<Pattern> Patterns
        {
            get => patterns;
            set
            {
                patterns = value;
                OnPropertyChanged(nameof(Patterns));
            }
        }


        public CategoryVM()
    {
        var app = (App)Application.Current;
        _categoryService = app.CategoryService;

        AddCommand = new RelayCommand(OpenAddWindow);
        UpdateCommand = new RelayCommand(OpenUpdateWindow, () => SelectedCategory != null);
        DeleteCommand = new RelayCommand(DeleteCategory, () => SelectedCategory != null);

        LoadCategories();
    }

    private void LoadCategories()
    {
        Categories.Clear();
        foreach (var category in _categoryService.GetAllCategories())
            Categories.Add(category);
    }

    private void OpenAddWindow()
    {
        var window = new AddCategoryWindow();
        window.ShowDialog();
        LoadCategories();
    }

    private void OpenUpdateWindow()
    {
        if (SelectedCategory == null) return;
        var window = new UpdateCategoryWindow(SelectedCategory);
        window.ShowDialog();
        LoadCategories();
    }

    private void DeleteCategory()
    {
        if (SelectedCategory == null) return;
        _categoryService.DeleteCategoryById(SelectedCategory.Id);
        SelectedCategory = null;
        LoadCategories();
    }


    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged(string name) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
}
