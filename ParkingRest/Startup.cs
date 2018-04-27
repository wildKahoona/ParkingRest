using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ParkingRest.DataAccess;
using ParkingRest.Models;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;

namespace ParkingRest
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("TestDatabase"));
            services.AddMvc();

            services.ConfigureSwaggerGen(c =>
            {
                var xmlCommentsPath = Path.Combine(AppContext.BaseDirectory, "ParkingRest.xml");
                c.IncludeXmlComments(xmlCommentsPath);
            });

            // Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "PassPark App",
                    Description = "My First ASP.NET Core 2.0 Web API",
                    TermsOfService = "None",
                    Contact = new Contact() { Name = "Team ABC", Email = "birgit.sturzenegger@gmx.ch" }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var context = serviceProvider.GetService<ApplicationDbContext>();
            AddTestData(context);

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "PassPark App V1");
            });

            app.UseMvc();
            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
        }

        private static void AddTestData(ApplicationDbContext context)
        {
            var carPark1 = new CarPark
            {
                Id = 100,
                Nummer = 21,
                Name = "Parkplatz 1",
                Strasse = "Bahnhofstrasse 1",
                Plz = 8001,
                Ort = "Zürich"
            };

            context.CarParks.Add(carPark1);

            var carPark2 = new CarPark
            {
                Id = 101,
                Nummer = 22,
                Name = "Parkplatz 2",
                Strasse = "Universitätsstrasse 3",
                Plz = 8003,
                Ort = "Zürich"
            };

            context.CarParks.Add(carPark2);

            context.SaveChanges();
        }
    }
}
