using System;
using System.IO;
using BookServer.Models;
using BookServer.Middleware;
using BookServer.Utilities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;

namespace BookServer
{
    // ContactsApi
    // https://github.com/mithunvp/ContactsAPI
    // Securing ASP.NET Core 2.0 Applications with JWT
    // https://auth0.com/blog/securing-asp-dot-net-core-2-applications-with-jwts/
    // https://jonhilton.net/2017/10/11/secure-your-asp.net-core-2.0-api-part-1---issuing-a-jwt/
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IHostingEnvironment CurrentEnvironment { get; }

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder();

            CurrentEnvironment = env;

            builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
                //.AddJsonFile($"connections.{env.EnvironmentName}.json", optional: false, reloadOnChange: true);

            if (env.IsDevelopment())
            {
                // Using Secret Manager tool
                builder.AddUserSecrets<Startup>();
            }
            else
            {
                // Requires ApplicationPool instance to have LoadUserProfile=true
                // builder.AddEnvironmentVariables("BookServer");
            }

            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string connDefault = string.Empty;
            string passPhrase = string.Empty;

            passPhrase = Configuration.GetConnectionString("PassPhrase");  // appsettings.Environment.json

            if (CurrentEnvironment.IsDevelopment())
            {
                connDefault = Configuration.GetConnectionString("DefaultConnection");
            }
            else
            {
                string connDefaultEncrypted = Configuration.GetConnectionString("DefaultConnection");
                passPhrase = Configuration.GetConnectionString("PassPhrase");
                // passPhrase = Configuration["PassPhrase"];  // Environment variable
                connDefault = Encryptor.DecryptString(connDefaultEncrypted, passPhrase);
            }

            // string connProduction = connDefault = Configuration.GetConnectionString("DefaultProduction");
            // Console.WriteLine("connProduction: " + Encryptor.EncryptString(connProduction, passPhrase));

            services.AddDbContext<BookContext>(options => options.UseSqlServer(connDefault));

            // Add MVC framework
            services.AddMvc()
                .AddJsonOptions(a => a.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver())
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_0);

            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });

            // Using Dependency Injection
            services.AddScoped<IBookRepository, BookRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // The order that middleware components are added in the Configure method
        // defines the order in which they're invoked on requests, and the reverse order for the response.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IBookRepository repo)
        {
            app.UseForwardedHeaders();

            // Enable logging
            // app.Use(async (context, next) =>
            // {
            //     // Request method, scheme, and path
            //     _logger.LogDebug("Request Method: {Method}", context.Request.Method);
            //     _logger.LogDebug("Request Scheme: {Scheme}", context.Request.Scheme);
            //     _logger.LogDebug("Request Path: {Path}", context.Request.Path);
            //
            //     // Headers
            //     foreach (var header in context.Request.Headers)
            //     {
            //         _logger.LogDebug("Header: {Key}: {Value}", header.Key, header.Value);
            //     }
            //
            //     // Connection: RemoteIp
            //     _logger.LogDebug("Request RemoteIp: {RemoteIpAddress}",
            //         context.Connection.RemoteIpAddress);
            //
            //     await next();
            // });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseMvc();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            // app.UseAuthentication();
            // app.UseHttpsRedirection();
            app.ApplyApiKeyValidation();
        }
    }
}
