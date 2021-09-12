using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using VideoGame.DAL.Repository;
using VideoGame.RESTAPI.Services;

namespace VideoGame.RESTAPI
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

            //configure DI for application services
            
            services.AddSingleton<IGamesRepository, GamesRepository>();
            //services.AddSingleton(typeof(IGamesRepository), typeof(GamesRepository));

            services.AddTransient<IUserService, UserService>();
            services.AddControllers();

            //Register Swagger generator
            services.AddSwaggerGen(c=>
            {

                var basicSecurityScheme = new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "basic",
                    Reference = new OpenApiReference { Id = "BasicAuth", Type = ReferenceType.SecurityScheme }
                };

                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Video Game metadata API",
                    Description= "A RESTful API that can be used to manage video game metadata",
                    Contact= new OpenApiContact
                    { 
                      Name="Jyoti Pawar",
                      Email="jyotiypawar@gmail.com",
                    }
                });

               
                c.AddSecurityDefinition(basicSecurityScheme.Reference.Id, basicSecurityScheme);
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {basicSecurityScheme, new string[] { }}
                });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHsts();
            app.UseHttpsRedirection();

            app.UseAuthentication();
            
            app.UseRouting();
            app.UseSwagger(c =>
            {
                c.SerializeAsV2 = true;
            });

            app.UseAuthorization();
            //enable middleware to serve swagger-ui (HTML,JS,CSS, etc)
            //specifying the swagger JSON Endpoint
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Video Game metadata API");
                c.DocumentTitle = "Video Game metadata API";
                c.DefaultModelsExpandDepth(0);
                c.RoutePrefix = string.Empty;
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
