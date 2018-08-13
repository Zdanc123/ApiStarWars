using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StarWars.Mapper;
using StarWars.Models;
using StarWars.Repository;
using Swashbuckle.AspNetCore.Swagger;

namespace StarWars
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1); 
            services.AddDbContext<StarWarsContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("StarWarsContext")));

            services.AddScoped<IRepository<Character>, Repository<Character>>();
    
            services.AddScoped<IRepository<Episode>, Repository<Episode>>();
            services.AddScoped<IRepository<Friend>, Repository<Friend>>();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddScoped<IUrlHelper>(factory =>
            {
                var actionContext = factory.GetService<IActionContextAccessor>()
                                           .ActionContext;
                return new UrlHelper(actionContext);
            });


            services.AddMvc().AddJsonOptions(options => {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });

            services.AddSwaggerGen(cfg=> {
                cfg.SwaggerDoc("v1", new Info { Title = "StarWarsApi Swagger", Version = "v1" });
                });

            var config = new MapperConfiguration(cfg =>
            {

                cfg.AddProfile(new MaperProfile());
            });
           
            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);

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
                app.UseHsts();
            }
            app.UseSwagger();
            app.UseSwaggerUI(confg=>
            {
                confg.SwaggerEndpoint("/swagger/v1/swagger.json", "StarWarsApi Swagger");
            });
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
