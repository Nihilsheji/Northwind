using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Northwind.DbContexts;
using Northwind.Services;
using Northwind.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Northwind
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

            services.AddDbContext<INorthwindDbContext, NorthwindDbContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("NorthwindDb")));

            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<ICategoriesService, CategoriesService>();
            services.AddScoped<ICustomersService, CustomersService>();
            services.AddScoped<IDemographicsService, DemographicsService>();
            services.AddScoped<IEmployeesService, EmployeesService>();
            services.AddScoped<IOrdersService, OrdersService>();
            services.AddScoped<IProductsService, ProductsService>();
            services.AddScoped<IRegionsService, RegionsService>();
            services.AddScoped<IShippersService, ShippersService>();
            services.AddScoped<ISuppliersService, SuppliersService>();
            services.AddScoped<ITerritoriesService, TerritoriesService>();
            services.AddScoped<IOrderDetailsService, OrderDetailsService>();
            services.AddScoped<IReportsService, ReportsService>();


            services.AddControllers().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Northwind", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Northwind v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
