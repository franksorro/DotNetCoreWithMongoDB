using DotNetCoreWithMongoDB.Middlewares;
using FS.Interfaces;
using FS.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;

namespace DotNetCoreWithMongoDB
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        private readonly IConfiguration configuration;
        private readonly IMongoDatabase mongoDB;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="env"></param>
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
                .AddEnvironmentVariables()
            ;

            if (env.IsDevelopment())
                builder.AddUserSecrets<Program>();

            configuration = builder.Build();

            string mongoDBConnection = string.Format(configuration.GetValue<string>("MongoDB:Url"), configuration.GetValue<string>("MongoDB:Usr"), configuration.GetValue<string>("MongoDB:Pwd"), configuration.GetValue<string>("MongoDB:Database"));

            MongoClient client = new(mongoDBConnection);
            mongoDB = client.GetDatabase(configuration.GetValue<string>("MongoDB:Database"));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .Configure<AppSettings>(configuration)
                .AddControllers();
            services.AddSingleton<IMongoDBService>(new MongoDBService(mongoDB));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app
                .UseHttpsRedirection()
                .UseRouting()
                .UseAuthorization()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                    endpoints.MapControllerRoute(
                            name: "api",
                            pattern: "api/{controller}/{id?}");
                    endpoints.MapControllerRoute(
                            name: "default",
                            pattern: "{controller=Home}/{action=Index}/{id?}");
                })
            ;
        }
    }
}
