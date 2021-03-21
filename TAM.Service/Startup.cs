
using TAMService.Data;
using TAMService.Data.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace TAMService
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            ConfigureDb(services);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "ActionsContextApi", Version = "1.0" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
           
            app.UseSwagger();
            app.UseHttpsRedirection();
            app.UseSwaggerUI(x => { x.SwaggerEndpoint("/swagger/v1/swagger.json", "ActionsContext V1.0"); });
            app.UseMvc();
        }

        private void ConfigureDb(IServiceCollection services)
        {
            var dbType = Configuration.GetSection("DbConfiguration").GetSection("Type").Value;
            var dbLocation = Configuration.GetSection("DbConfiguration").GetSection("Location").Value;
            if (dbType.Equals("Sql"))
            {
                if (dbLocation.Equals("Azure"))
                {
                    var connectionString = Configuration.GetConnectionString("AzureSqlDb");
                    services.AddDbContext<CoreServiceContext>(optionsBuilder =>
                        optionsBuilder.UseSqlServer(connectionString));
                }
                else
                {
                    services.AddDbContext<CoreServiceContext>(x => x.UseInMemoryDatabase($"{dbType}-{dbLocation}"));
                }
                services.AddScoped<IDataStore, SqlDataStore>();
            }
            
        }

    }
}
