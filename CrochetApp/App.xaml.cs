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

            //add services
            services.AddTransient<TagService>();
            services.AddTransient<ImageService>();
            services.AddTransient<UserService>();
            services.AddTransient<HookService>();
            services.AddTransient<YarnService>();
            services.AddTransient<CategoryService>();

            //resolve services

            var serviceProvider = services.BuildServiceProvider();

            TagService = serviceProvider.GetRequiredService<TagService>();
            ImageService = serviceProvider.GetRequiredService<ImageService>();
            UserService = serviceProvider.GetRequiredService<UserService>();
            HookService = serviceProvider.GetRequiredService<HookService>();
            YarnService = serviceProvider.GetRequiredService<YarnService>();
            CategoryService = serviceProvider.GetRequiredService<CategoryService>();
        }

    }

}
