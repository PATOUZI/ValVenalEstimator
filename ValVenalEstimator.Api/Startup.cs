using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using ValVenalEstimator.Api.Data;
using ValVenalEstimator.Api.Mapping;
using ValVenalEstimator.Api.Repositories;
using ValVenalEstimator.Api.Contracts;
using System.Text.Json.Serialization;

namespace ValVenalEstimator.Api
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
            services.AddCors(options => options.AddDefaultPolicy(b => b.AllowAnyOrigin()
                                                                        .AllowAnyHeader()
                                                                        .AllowAnyMethod()
                                                                )
                            );
            services.AddDbContext<ValVenalEstimatorDbContext>(opt => opt.UseMySQL("server=localhost;database=ValVenalEstimator2;user=root;password="));
            services.AddScoped<IPlaceRepository, PlaceRepository>();
            services.AddScoped<IPrefectureRepository, PrefectureRepository>();
            services.AddScoped<IZoneRepository, ZoneRepository>();   
            services.AddScoped<ICsvParserService, CsvParserService>();   

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ModelToResourceProfile());   
            });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddMvc(); 
            
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ValVenalEstimator.Api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ValVenalEstimator.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
