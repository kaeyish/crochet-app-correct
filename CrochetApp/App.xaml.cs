using CrochetApp.backend.Domain.RepositoryInterfaces;
using CrochetApp.backend.Repository;
using CrochetApp.backend.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.Windows;

namespace CrochetApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IConfiguration Config;

        //services
        public ImageService ImageService;
        
        public TagService TagService;

        public UserService UserService;

        public HookService HookService;

        public YarnService YarnService;

        public CategoryService CategoryService;

        public TechniqueService TechniqueService;

        public SuggestionService SuggestionService;

        public TutorialService TutorialService;

        public LibraryService LibraryService;

        public RequestService RequestService;

        public PatternService PatternService;

        public ProjectService ProjectService;

        private string _connectionString;
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var builder = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appconfig.json");
            
            Config= builder.Build();

            _connectionString = App.Config.GetSection("ConnectionStrings:DefaultConnection").Value;

            var services = new ServiceCollection();

            //initiate repos
            services.AddSingleton<ITagRepository>(provider => new TagRepository(_connectionString));
            services.AddSingleton<IImageRepository>(provider => new ImageRepository(_connectionString));
            services.AddSingleton<IUserRepository>(provider => new UserRepository(_connectionString));
            services.AddSingleton<IHookRepository>(provider => new HookRepository(_connectionString));
            services.AddSingleton<IYarnRepository>(provider => new YarnRepository(_connectionString));
            services.AddSingleton<ICategoryRepository>(provider => new CategoryRepository(_connectionString));
            services.AddSingleton<ITechniqueRepository>(provider => new TechniqueRepository(_connectionString));
            services.AddSingleton<ISuggestionRepository>(provider => new SuggestionRepository(_connectionString));
            services.AddSingleton<ITutorialRepository>(provider => new TutorialRepository(_connectionString));
            services.AddSingleton<ILibraryRepository>(provider => new LibraryRepository(_connectionString));
            services.AddSingleton<IRequestRepository>(provider => new RequestRepository(_connectionString));
            services.AddSingleton<IPatternRepository>(provider => new PatternRepository(_connectionString));
            services.AddSingleton<IProjectRepository>(provider => new ProjectRepository(_connectionString));

            //add services
            services.AddTransient<TagService>();
            services.AddTransient<ImageService>();
            services.AddTransient<UserService>();
            services.AddTransient<HookService>();
            services.AddTransient<YarnService>();
            services.AddTransient<CategoryService>();
            services.AddTransient<TechniqueService>();
            services.AddTransient<SuggestionService>();
            services.AddTransient<TutorialService>();
            services.AddTransient<LibraryService>();
            services.AddTransient<RequestService>();
            services.AddTransient<PatternService>();
            services.AddTransient<ProjectService>();

            //resolve services

            var serviceProvider = services.BuildServiceProvider();

            TagService = serviceProvider.GetRequiredService<TagService>();
            ImageService = serviceProvider.GetRequiredService<ImageService>();
            UserService = serviceProvider.GetRequiredService<UserService>();
            HookService = serviceProvider.GetRequiredService<HookService>();
            YarnService = serviceProvider.GetRequiredService<YarnService>();
            CategoryService = serviceProvider.GetRequiredService<CategoryService>();
            TechniqueService = serviceProvider.GetRequiredService<TechniqueService>();
            SuggestionService = serviceProvider.GetRequiredService<SuggestionService>();  
            TutorialService = serviceProvider.GetRequiredService<TutorialService>();  
            LibraryService = serviceProvider.GetRequiredService<LibraryService>();
            RequestService = serviceProvider.GetRequiredService<RequestService>();  
            PatternService = serviceProvider.GetRequiredService<PatternService>();  
            ProjectService = serviceProvider.GetRequiredService<ProjectService>();  
            
        }
    }

}
