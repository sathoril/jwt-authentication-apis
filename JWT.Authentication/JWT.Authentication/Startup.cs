using JWT.Authentication.FakeDataAccessLayer;
using JWT.Authentication.JwtTokenConfiguration.ServiceConfiguration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

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

            // Configures JWT Authentication
            JwtServiceConfiguration.ConfigureAuthenticationServices(services, this.Configuration);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSwaggerGen(gen => 
            gen.SwaggerDoc("v1", new Info
            {
                Title = ".NET Core API with JWT authentication and OpenAPI specification.",
                Version = "v1",
            });

            // Configure Swagger to use the xml documentation file
            var xmlFile = Path.ChangeExtension(typeof(Startup).Assembly.Location, ".xml");
            c.IncludeXmlComments(xmlFile);

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
            app.UseMvc();
        }
    }
}
