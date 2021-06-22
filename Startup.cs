using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelMVC2.Data;
using Microsoft.EntityFrameworkCore;



namespace HotelMVC2
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
                services.AddMvc();
            }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        [Obsolete]
        public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
            {
                if (env.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                    //app.UseBrowserLink();

                    // Seed the database
                    var options = new DbContextOptionsBuilder<HotelBookingContext>()
                        .UseInMemoryDatabase("HotelBookingDb")
                        .Options;
                    var dbContext = new HotelBookingContext(options);
                    DbInitializer.Initialize(dbContext);
                }
                else
                {
                    app.UseExceptionHandler("/Home/Error");
                }

                app.UseStaticFiles();
                

                app.UseMvc(routes =>
                {
                    routes.MapRoute(
                        name: "default",
                        template: "{controller=Bookings}/{action=Index}/{id?}");
                });
            }
     }
    
}
