using System;
using System.Net;
using System.Text;
using AutoMapper;
using DryIoc;
using DryIoc.Microsoft.DependencyInjection;
using Inception.Repository.Utility.Extensions;
using Inception.Utility.ModelBinding;
using Inception.Utility.ModelBinding.ActionConstraint;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;

namespace Inception
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            ConfigureMiscellaneous();
        }



        private void ConfigureMiscellaneous()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            ServicePointManager.DefaultConnectionLimit = 1;
        }



        public IConfiguration Configuration { get; }



        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            var container = new Container(
                rules => rules.With(propertiesAndFields: PropertiesAndFields.Of)
                )
                .WithDependencyInjectionAdapter(
                    throwIfUnresolved: type => true
                );

            container.RegisterCompositionRoot<CompositionRoot>();

            services.AddMvc(mvcOptions =>
                {
                    mvcOptions.Conventions.Add(container.Resolve<ICustomActionModelConvention>());

                    mvcOptions.Filters.Add(container.Resolve<IExceptionFilter>());

                    mvcOptions.ModelBinderProviders.Insert(0, container.Resolve<ICustomModelBinderProvider>());
                })
                .AddControllersAsServices()
                .AddJsonOptions
                (
                options => options.SerializerSettings.ContractResolver = new DefaultContractResolver()
                );


            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
                {
                    configuration.RootPath = "ClientApp/dist";
                });


            services.AddAutoMapper();


            container.Populate(services);

            return container.Resolve<IServiceProvider>();
        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseMvc(routes =>
                {
                    routes.MapRoute(
                        name: "default",
                        template: "api/{controller}/{action=Index}/{id?}");
                });

            app.UseSpa(spa =>
                {
                    // To learn more about options for serving an Angular SPA from ASP.NET Core,
                    // see https://go.microsoft.com/fwlink/?linkid=864501

                    spa.Options.SourcePath = "ClientApp";

                    if (env.IsDevelopment())
                    {
                        spa.UseAngularCliServer(npmScript: "start");
                    }
                });
        }
    }
}
