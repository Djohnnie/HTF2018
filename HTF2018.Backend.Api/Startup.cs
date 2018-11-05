using HTF2018.Backend.Api.Middleware;
using HTF2018.Backend.Common;
using HTF2018.Backend.Logic;
using HTF2018.Backend.Logic.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using HTF2018.Backend.ChallengeEngine;

namespace HTF2018.Backend.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IChallengeLogic, ChallengeLogic>();
            services.AddTransient<IChallengeEngine, ChallengeEngine.ChallengeEngine>();
            services.AddScoped<IHtfContext, HtfContext>();

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                    options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                });

            String xmlFile1 = "HTF2018.Backend.Api.XML";
            String xmlFile2 = "HTF2018.Backend.ChallengeEngine.XML";

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v2018", new Info
                {
                    Version = "v2018.1",
                    Title = "HTF2018: The Artifact",
                    Description = "Description",
                    Contact = new Contact
                    {
                        Email = "EMAIL",
                        Name = "NAME",
                        Url = "URL"
                    },
                    License = new License
                    {
                        Name = "NAME",
                        Url = "URL"
                    },
                    TermsOfService = "TermsOfService"
                });
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFile1));
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFile2));
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<RequestUriMiddleware>();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v2018/swagger.json", "HTF2018: The Artifact");
            });

            app.UseMvc();
        }
    }
}