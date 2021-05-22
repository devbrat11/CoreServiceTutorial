using TAMService.Data.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TAMService.Data.DataStore;
using Microsoft.Extensions.Hosting;

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
            ConfigureDb(services);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "TAM", Version = "1.0" });
            });

            services.AddControllers();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, TAMServiceContext dbContext)
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

            dbContext.Database.Migrate();
            app.UseSwagger();
            app.UseHttpsRedirection();
            app.UseSwaggerUI(x => { x.SwaggerEndpoint("/swagger/v1/swagger.json", "TAM V1.0"); });

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void ConfigureDb(IServiceCollection services)
        {
            var dbType = Configuration.GetSection("DbConfiguration").GetSection("Type").Value;
            var dbLocation = Configuration.GetSection("DbConfiguration").GetSection("Location").Value;
            if (dbType.Equals("Sql"))
            {
                var connectionString = Configuration.GetConnectionString("SqlDb");
                services.AddDbContext<TAMServiceContext>(x => x.UseSqlServer(connectionString));
                services.AddScoped<IDataStore, SqlDataStore>();
            }
        }

    }
}
