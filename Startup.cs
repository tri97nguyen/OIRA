using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using parser.Data;
using MySqlConnector;

namespace parser
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Environment = env;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; set; }
        public static string GetDatabasePassword(IConfiguration config)
        {
            return System.Environment.GetEnvironmentVariable("MySqlPassword");
        }
        public static string GetDatabaseUsername(IConfiguration config)
        {
            return System.Environment.GetEnvironmentVariable("MySqlUsername");
        }
        public static string GetDatabaseConnectionString(IConfiguration config)
        {
            return String.Format(config["ConnectionStrings:OIRAMySql"], GetDatabaseUsername(config), GetDatabasePassword(config));
        }
        internal string DatabasePassword { get { return GetDatabasePassword(Configuration); } }
        public string DatabaseConnectionString { get { return GetDatabaseConnectionString(Configuration); } }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            Console.WriteLine($"connection string is {DatabaseConnectionString}");
            services.AddRazorPages();
            services.AddDbContextPool<AppDbContext>(
            //options => options.UseSqlServer(Configuration.GetConnectionString("OIRA")));
                options =>
                    options.UseMySQL(DatabaseConnectionString)

            );
            services.AddScoped<RubricService>();
            services.AddScoped<CourseAndFacultyService>();
            services.AddScoped<RubricAndFacultyService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action}",
                    defaults: new { controller = "Upload", action = "Index" }
                );
            });
        }
    }
}
