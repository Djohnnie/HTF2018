using HTF2018.Backend.Api.Middleware;
using HTF2018.Backend.Api.Swagger;
using HTF2018.Backend.Common;
using HTF2018.Backend.DataAccess;
using HTF2018.Backend.Logic;
using HTF2018.Backend.Logic.Challenges;
using HTF2018.Backend.Logic.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using HTF2018.Backend.Api.Filters;

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
            services.AddMemoryCache();
            services.AddDbContext<TheArtifactDbContext>();
            services.AddTransient<IChallengeLogic, ChallengeLogic>();
            services.AddTransient<ITeamLogic, TeamLogic>();
            services.AddTransient<IDashboardLogic, DashboardLogic>();
            services.AddTransient<IHistoryLogic, HistoryLogic>();
            services.AddTransient<IImageLogic, ImageLogic>();
            services.AddTransient<IChallengeEngine, ChallengeEngine>();
            services.AddScoped<IHtfContext, HtfContext>();
            services.AddTransient<IChallenge01, Challenge01>();
            services.AddTransient<IChallenge02, Challenge02>();
            services.AddTransient<IChallenge03, Challenge03>();
            services.AddTransient<IChallenge04, Challenge04>();
            services.AddTransient<IChallenge05, Challenge05>();
            services.AddTransient<IChallenge06, Challenge06>();
            services.AddTransient<IChallenge07, Challenge07>();
            services.AddTransient<IChallenge08, Challenge08>();
            services.AddTransient<IChallenge09, Challenge09>();
            services.AddTransient<IChallenge10, Challenge10>();
            services.AddTransient<IChallenge11, Challenge11>();
            services.AddTransient<IChallenge12, Challenge12>();
            services.AddTransient<IChallenge13, Challenge13>();
            services.AddTransient<IChallenge14, Challenge14>();
            services.AddTransient<IChallenge15, Challenge15>();
            services.AddTransient<IChallenge16, Challenge16>();
            services.AddTransient<IChallenge17, Challenge17>();
            services.AddTransient<IChallenge18, Challenge18>();
            services.AddTransient<IChallenge19, Challenge19>();
            services.AddTransient<IChallenge20, Challenge20>();
            services.AddScoped<IdentificationFilter>();
            services.AddScoped<AdministratorFilter>();

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                    options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                });

            String xmlFile1 = "HTF2018.Backend.Api.xml";
            String xmlFile2 = "HTF2018.Backend.Common.xml";

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v2018", new Info
                {
                    Version = "v2018.1",
                    Title = "HTF2018: The Artifact",
                    Description = "This page provides some documentation on the available endpoints for the Hack The Future 2018 .NET Challenge.",
                });
                c.OperationFilter<IdentificationOperationFilter>();
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

            app.UseMiddleware<RequestLoggingMiddleware>();
            app.UseMiddleware<IdentificationMiddleware>();
            app.UseMiddleware<ThrottlingMiddleware>();
            app.UseMiddleware<RequestUriMiddleware>();
            app.UseMiddleware<ExceptionMiddleware>();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v2018/swagger.json", "HTF2018: The Artifact");
            });

            app.UseDefaultFiles();
            app.UseStaticFiles(new StaticFileOptions
            {
                ServeUnknownFileTypes = true,
                DefaultContentType = "text/plain"
            });
            app.UseMvc();
        }
    }
}