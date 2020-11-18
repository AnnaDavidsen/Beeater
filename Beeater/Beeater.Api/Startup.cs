using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Beeater.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Beeater.Domain.Entities;

namespace Beeater.Api
{
    public class Startup
    {
        //to re-scaffold: Scaffold-DbContext 'Server=beeaterserver.database.windows.net;Initial Catalog=beeater;User ID=stumpedumpe;Password=~u/Z`4_cC&q8D:u`G*WM;' Microsoft.EntityFrameworkCore.SqlServer -OutputDir Entities  -force
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(opt =>
                opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddDbContext<beeaterContext>(opt => 
                opt.UseSqlServer(Configuration.GetConnectionString("Database")));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
