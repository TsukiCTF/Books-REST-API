using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Books.Data;
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
using Newtonsoft.Json.Serialization;

namespace Books
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // set up SQL server connection
            services.AddDbContext<BooksContext>(option => option.UseSqlServer
                (Configuration.GetConnectionString("BooksConnection")));
            // set up routes and add json conversion support
            services.AddControllers().AddNewtonsoftJson(s => {
                s.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });
            // set up swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Books", Version = "v1" });
            });
            // set up auto mapper for DTOs
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            // set up repository
            services.AddScoped<IBooksRepo, SqlBooksRepo>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // set up middlewares
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Books v1"));

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
