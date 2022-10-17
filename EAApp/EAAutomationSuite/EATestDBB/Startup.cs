using EATestFramework.Driver;
using EATestFramework.Extensions;
using EATestDBB.Pages;
using Microsoft.Extensions.DependencyInjection;
using SolidToken.SpecFlow.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProductAPI.Data;
using ProductAPI.Repository;

namespace EATestDBB
{
    public static class Startup
    {
        [ScenarioDependencies]
        public static IServiceCollection CreateServices()
        {
            var services = new ServiceCollection();

            string projectPath = AppDomain.CurrentDomain.BaseDirectory.Split(new String[] { @"bin\" }, StringSplitOptions.None)[0];

            IConfigurationRoot configuration = new ConfigurationBuilder()
                         .SetBasePath(projectPath)
                         .AddJsonFile("appsettings.json")
                         .Build();

            string connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ProductDbContext>(
                                option => option
                                          .UseSqlServer(connectionString));

            services.AddTransient<IProductRepository, ProductRepository>();
            services.UseWebDriverInitializer();
            services.AddScoped<IBrowserDriver, BrowserDriver>();
            services.AddScoped<IDriverFixture, DriverFixture>();

            services.AddScoped<IHomePage, HomePage>();
            services.AddScoped<IProductPage, ProductPage>();

            return services;
        }
    }
}
