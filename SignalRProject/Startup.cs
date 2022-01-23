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
using Microsoft.AspNetCore.SignalR;

namespace SignalRProject
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

            services.AddControllers();

            // This allows to connect via SignalR with html file from disk - thats why origin = "null"
            services.AddCors(options => options.AddPolicy(name : "MyPolicy",
                builder =>
                        {
                            builder.WithOrigins("null","http://localhost:5000")
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials();
                        }));
            
            services.AddSignalR();
                      
            services.AddSingleton<ParenthesisChecker>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SignalRProject", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SignalRProject v1"));
            }

            // enable redicrection to all but not for Hub connection
            app.UseWhen(context => !context.Request.Path.StartsWithSegments("/path"),
                                    builder => builder.UseHttpsRedirection());

            app.UseRouting();

            app.UseCors("MyPolicy");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapControllers();
                endpoints.MapHub<MyHub>("/path");
            });
        }
    }
}
