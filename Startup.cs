using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using mvc_album_browser.Abstract;
using mvc_album_browser.Entities;
using mvc_album_browser.Services;

namespace mvc_album_browser
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            const string USERS_URL = "http://jsonplaceholder.typicode.com/users";
            const string ALBUMS_URL = "http://jsonplaceholder.typicode.com/albums";
            const string PHOTOS_URL = "http://jsonplaceholder.typicode.com/photos";
            const string POSTS_URL = "http://jsonplaceholder.typicode.com/posts";

            // Add framework services.
            services
                .AddTransient(typeof(IHttpClient<user>), x => new HttpClientBase<user>(USERS_URL))
                .AddTransient(typeof(IHttpClient<album>), x => new HttpClientBase<album>(ALBUMS_URL))
                .AddTransient(typeof(IHttpClient<photo>), x => new HttpClientBase<photo>(PHOTOS_URL))
                .AddTransient(typeof(IHttpClient<post>), x => new HttpClientBase<post>(POSTS_URL))
                .AddTransient(typeof(IJsonSerializer<>), typeof(NewtonsoftHttpJsonSerializer<>))
                .AddTransient(typeof(IRepository<user>), typeof(HttpJsonRepository<user>))
                .AddTransient(typeof(IRepository<album>), typeof(HttpJsonRepository<album>))
                .AddTransient(typeof(IRepository<photo>), typeof(HttpJsonRepository<photo>))
                .AddTransient(typeof(IRepository<post>), typeof(HttpJsonRepository<post>))
                .AddTransient<UsersService, UsersService>()
                .AddTransient<AlbumsService, AlbumsService>()
                .AddTransient<PhotosService, PhotosService>()
                .AddTransient<PostsService, PostsService>()
                .AddTransient<CompleteAlbumsService, CompleteAlbumsService>()
                .AddMvc();
            
            new MapperConfigurationStartupTask().OnStartup();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
