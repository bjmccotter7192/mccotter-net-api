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
        private string _postgresHost = null;
        private string _postgresUser = null;
        private string _postgresDatabase = null;
        private string _postgresPassword = null;
        private string _postgresPort = null;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            _postgresHost = Configuration["Postgres:Host"];
            _postgresUser = Configuration["Postgres:User"];
            _postgresPort = Configuration["Postgres:Port"];
            _postgresPassword = Configuration["Postgres:Password"];
            _postgresDatabase = Configuration["Postgres:Database"];

            services.AddControllers();

            var sqlConnectionString = "Host=" + _postgresHost + 
                                      ";Username=" + _postgresUser + 
                                      ";Password=" + _postgresPassword + 
                                      ";Port=" + _postgresPort + 
                                      ";Database=" + _postgresDatabase + 
                                      ";sslmode=Require;Trust Server Certificate=true"; 
  
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
