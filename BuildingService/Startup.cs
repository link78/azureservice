using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using BuildingService.Abstract;
using BuildingService.Models;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Swagger;
using Newtonsoft.Json.Serialization;
using BuildingService.Services;

namespace BuildingService
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
                

            Configuration = builder.Build();
            //configuration;IConfiguration configuration
        }

        public static IConfiguration Configuration { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            


#if DEBUG

            services.AddTransient<IMailService,LocalMailService>();
#else
            services.AddTransient<IMailService,CloudMailService>();
#endif


            services.AddDbContext<AppDBContext>(options => 
            options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));
            services.AddScoped<IRepoDepartment,RepoDepartment>();





            services.AddMvc();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
            IHostingEnvironment env, ILoggerFactory loggerFactory, AppDBContext context)
        {
            // implementing and configure iloggerFactory
            loggerFactory.AddConsole();
            loggerFactory.AddDebug();


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // seeding the data
            context.EnsuredSeedData();

            // setting auto mapper

            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Department, DepartmentViewModle>();
                cfg.CreateMap<Department, DepartmentDto>();
                cfg.CreateMap<Employees, EmployeeDto>();
                cfg.CreateMap<EmployeeDtoCreation, Employees>();
            });
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseMvc();




           
         
        }
    }
}
