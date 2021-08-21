using System;
using System.IO;
using System.Reflection;
using mccotter_net_api.DataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace mccotter_net_api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            var sqlConnectionString = "Host=" + Environment.GetEnvironmentVariable("DB_HOST") + 
                                      ";Username=" + Environment.GetEnvironmentVariable("DB_USER") + 
                                      ";Password=" + Environment.GetEnvironmentVariable("DB_PASSWORD") + 
                                      ";Port=" + Environment.GetEnvironmentVariable("DB_PORT") + 
                                      ";Database=" + Environment.GetEnvironmentVariable("DB_DATABASE") + 
                                      ";sslmode=Require;Trust Server Certificate=true"; 
  
            services.AddDbContext<PostgreSqlContext>(options => options.UseNpgsql(sqlConnectionString));  
  
            services.AddScoped<IDataAccessProvider, DataAccessProvider>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo 
                { 
                    Title = "mccotter_net_api", 
                    Version = "v1",
                    Contact = new OpenApiContact
                    {
                        Name = "BJ McCotter",
                        Email = "bjmccotter7192@gmail.com",
                        Url = new Uri("https://mccotter.net")
                    }
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger(c =>
                {
                    c.SerializeAsV2 = true;
                });
                app.UseSwaggerUI(c => 
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "mccotter_net_api v1");
                    c.RoutePrefix = string.Empty;
                    c.InjectStylesheet("/swagger-ui/custom.css");
                });
            }
            
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
