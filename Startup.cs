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
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using lapr5_masterdata_viagens.Domain.Vehicles;
using lapr5_masterdata_viagens.Infrastructure;
using lapr5_masterdata_viagens.Infrastructure.Vehicles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using lapr5_masterdata_viagens.Infrastructure.Shared;
using lapr5_masterdata_viagens.Domain.Shared;
using lapr5_masterdata_viagens.Domain.Drivers;
using lapr5_masterdata_viagens.Infrastructure.Drivers;
using lapr5_masterdata_viagens.Domain.Trips;
using lapr5_masterdata_viagens.Infrastructure.Trips;
using lapr5_masterdata_viagens.Domain.VehicleDuties;
using lapr5_masterdata_viagens.Infrastructure.VehicleDuties;
using lapr5_masterdata_viagens.Domain.ImportData;
using lapr5_masterdata_viagens.Domain.DriverDuties;
using lapr5_masterdata_viagens.Infrastructure.DriverDuties;
using lapr5_masterdata_viagens.Infrastructure.MDRHttpClient;

namespace lapr5_masterdata_viagens
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddCors(options =>
                    {
                        options.AddPolicy(name: MyAllowSpecificOrigins,
                                          builder =>
                                          {
                                              builder.WithOrigins("http://localhost:4200");
                                          });
                    });

            services.AddDbContext<ViagensDbContext>(opt =>
                //opt.UseSqlite("Data Source=db/teste.db")
                opt.UseSqlServer(Configuration.GetConnectionString("AzureDb"))
                .ReplaceService<IValueConverterSelector, StronglyEntityIdValueConverterSelector>());

            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IVehicleRepo, VehicleRepo>();
            services.AddTransient<VehicleService>();
            services.AddTransient<IDriverRepo, DriverRepo>();
            services.AddTransient<DriverService>();
            services.AddTransient<ITripRepo, TripRepo>();
            services.AddTransient<TripService>();
            services.AddTransient<IVehicleDutyRepo, VehicleDutyRepo>();
            services.AddTransient<VehicleDutyService>();
            services.AddTransient<IDriverDutyRepo, DriverDutyRepo>();
            services.AddTransient<DriverDutyService>();
            services.AddTransient<ImportDataService>();

            services.AddHttpClient<MDRHttpClientInterface, MDRHttpClientService>();

            services.AddControllers();

            //SWAGGER
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "lapr5_masterdata_viagens", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                //SWAGGER
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "lapr5_masterdata_viagens v1"));
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(MyAllowSpecificOrigins);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
