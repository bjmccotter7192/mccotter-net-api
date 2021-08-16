using System;
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
        private string POSTGRES_HOST = Environment.GetEnvironmentVariable("POSTGRES_HOST");
        private string  POSTGRES_USER = Environment.GetEnvironmentVariable("POSTGRES_USER");
        private string  POSTGRES_PORT = Environment.GetEnvironmentVariable("POSTGRES_PORT");
        private string  POSTGRES_PASSWORD = Environment.GetEnvironmentVariable("POSTGRES_PASSWORD");
        private string  POSTGRES_DATABASE = Environment.GetEnvironmentVariable("POSTGRES_DATABASE");

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            // var sqlConnectionString = Configuration["PostgreSqlConnectionString"];  
            var sqlConnectionString = $"Host={POSTGRES_HOST};User={POSTGRES_USER};Port={POSTGRES_PORT};Password={POSTGRES_PASSWORD};Database={POSTGRES_DATABASE};sslmode=Require;Trust Server Certificate=true;";
  
            services.AddDbContext<PostgreSqlContext>(options => options.UseNpgsql(sqlConnectionString));  
  
            services.AddScoped<IDataAccessProvider, DataAccessProvider>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "mccotter_net_api", Version = "v1" });
            });

            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
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
