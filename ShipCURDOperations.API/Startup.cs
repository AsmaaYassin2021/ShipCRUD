using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Microsoft.EntityFrameworkCore;
using ShipCURDOperations.Data.Repository;
using ShipCURDOperations.Business.Interfaces;
using ShipCURDOperations.Business;
using ShipCURDOperations.Data.Interfaces;
using ShipCURDOperations.API.Model;
using Serilog;
namespace ShipCURDOperations.API
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


            services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy",
                builder => builder
                 .WithOrigins("http://localhost:4200/")
                 .SetIsOriginAllowed((host) => true)
                .AllowAnyMethod()
                .AllowAnyHeader());
        });
            services.AddControllers().AddNewtonsoftJson();
            services.AddScoped<IShipService, ShipService>();
            services.AddScoped<IShipRepository, ShipRepository>();
            services.AddDbContext<MemoryDBContext>(opt => opt.UseInMemoryDatabase(databaseName: "Ships"), ServiceLifetime.Scoped);
            //
            services.Configure<ApiBehaviorOptions>(o =>
                       {
                           o.InvalidModelStateResponseFactory = actionContext =>
                           {
                               CustomBadRequest customBadRequest = new CustomBadRequest(actionContext.ModelState);

                               Log.Error("Invalid ship model for " + string.Join(", ", customBadRequest.errors));
                               return new BadRequestObjectResult(customBadRequest);
                           };

                       });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("CorsPolicy");
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseSerilogRequestLogging();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
