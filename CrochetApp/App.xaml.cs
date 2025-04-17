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

        public TagService TagService;
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var builder = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appsettings.json");
            
            Config= builder.Build();

            var services = new ServiceCollection();

            //initiate repos
            services.AddSingleton<ITagRepository>(provider => new TagRepository(App.Config.GetSection("ConnectionStrings:DefaultConnection").Value));

            //add services
            services.AddTransient<TagService>();

            //resolve services

            var serviceProvider = services.BuildServiceProvider();

            TagService = serviceProvider.GetRequiredService<TagService>();

        }

    }

}
