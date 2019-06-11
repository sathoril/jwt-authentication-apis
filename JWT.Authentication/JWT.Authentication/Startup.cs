using JWT.Authentication.FakeDataAccessLayer;
using JWT.Authentication.JwtTokenConfiguration.ServiceConfiguration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace JWT.Authentication
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
            services.AddTransient<FakeUserRepository>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // Configures JWT Authentication
            JwtServiceConfiguration.ConfigureAuthenticationServices(services, this.Configuration);

            // Swagger Configuration
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(
                    "v1",
                    new Info
                    {
                        Title = ".Net Core API with JWT Authentication.",
                        Version = "1.0.0",
                        Contact = new Contact
                        {
                            Name = "Bruno Fernando Corrêa de Abreu",
                            Email = "bruno.feabreu@gmail.com"
                        }
                    });

                c.AddSecurityDefinition("Bearer", new BasicAuthScheme { Type = "http",  );

                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>> {
                    { "Bearer", Enumerable.Empty<string>() },
                });
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

            app.UseHttpsRedirection();

            // Configures Swagger on Application Startup
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", ".Net Core API with JWT Authentication.");
            });

            app.UseMvc();


        }
    }
}
