using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using NetTopologySuite;
using PeliculasAPI.Helpers;
using Microsoft.Extensions.DependencyInjection;

namespace PeliculasAPI.Tests
{
    public class BasePruebas<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {

        protected WebApplicationFactory<Program> BuildWebApplicationFactory(string nameDB)
        {
            var factory = new WebApplicationFactory<Program>();
            factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    var descriptorDBContext = services.SingleOrDefault(d =>
                    d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

                    if (descriptorDBContext != null) services.Remove(descriptorDBContext);

                    services.AddDbContextPool<ApplicationDbContext>(options =>
                        options.UseInMemoryDatabase(nameDB));
                    var sp = services.BuildServiceProvider();

                    using (var scope = sp.CreateScope())
                    {
                        var scopedServices = scope.ServiceProvider;
                        var db = scopedServices.GetRequiredService<ApplicationDbContext>();
                        db.Database.EnsureCreated();
                    }
                        
                });
            });

            return factory;
        }

    }
}
