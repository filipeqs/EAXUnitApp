using EATestFramework.Driver;
using EATestFramework.Extensions;
using EATestProject.Pages;
using Microsoft.Extensions.DependencyInjection;

namespace EATestProject
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.UseWebDriverInitializer();
            services.AddScoped<IBrowserDriver, BrowserDriver>();
            services.AddScoped<IDriverFixture, DriverFixture>();

            services.AddScoped<IHomePage, HomePage>();
            services.AddScoped<ICreateProductPage, CreateProductPage>();
        }
    }
}
