using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Primitives;

namespace smart_local
{
    public class Startup
    {
        private static IServerAddressesFeature _addresses = null;

        /// <summary>
        /// List of the webserver addresses in use.
        /// </summary>
        public static IServerAddressesFeature Addresseses => _addresses;


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            _addresses = app.ServerFeatures.Get<IServerAddressesFeature>();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    string code = string.Empty;
                    string state = string.Empty;

                    IQueryCollection query = context.Request.Query;

                    foreach (KeyValuePair<string, StringValues> kvp in query)
                    {
                        switch (kvp.Key)
                        {
                            case "code":
                                code = kvp.Value.FirstOrDefault();
                                break;
                            
                            case "state":
                                state = kvp.Value.FirstOrDefault();
                                break;
                        }
                    }

                    Task.Run(() => Program.SetAuthCode(code, state));

                    await context.Response.WriteAsync("Code received, you may close this window.");
                });
            });
        }
    }
}
