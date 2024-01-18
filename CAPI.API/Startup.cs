using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CAPI.BAL;
using CAPI.CONTRACT;
using CAPI.DAL.DBConnection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CAPI.API
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddScoped<ISubEmp, SubEmpBO>();
            services.AddScoped<IDapperHelper, DapperHelper>();
            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                builder =>

                //builder.WithOrigins(
                //  "http://localhost:44358"));


                builder.WithOrigins("http://localhost", "http://localhost/", "http://localhost:44358/api/"
                , "http://localhost:44358/List"
                , "http://localhost:44358"
                , "http://localhost:44358/api/SubEmp/GetSubEmpList"
                    ).AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            //app.UseCors(MyAllowSpecificOrigins);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();

            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(
            //        name: "Default",
            //        template: "{Controller}/{Action}/{id?}");
            //});
        }
    }
}
