using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelApp.Api.AutoMapper;
using HotelApp.Core.Data;
using HotelApp.Data.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using SimpleInjector.Integration.AspNetCore.Mvc;

namespace HotelApp.Api
{
    /// <summary>
    /// References:
    /// Simple Injector: https://simpleinjector.readthedocs.io/en/latest/aspnetintegration.html
    /// Auto Mapper: http://docs.automapper.org/en/stable/Dependency-injection.html#simple-injector
    /// GraphQL: https://medium.com/volosoft/building-graphql-apis-with-asp-net-core-419b32a5305b
    /// </summary> 
    public class Startup
    {
        private Container _container = new Container();

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        #region SimpleInjector
        private void InitializeContainer()
        {
            _container.Register(typeof(IReservationRepository), typeof(ReservationRepository), Lifestyle.Scoped);

            #region AutoMapper
            ConfigureAutoMapper();
            #endregion
        }
        #endregion

        #region AutoMapper
        private void ConfigureAutoMapper()
        {
            _container.RegisterSingleton(() => _container.GetInstance<MapperProvider>().GetMapper());
        }
        #endregion

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            #region SimpleInjector
            services.AddSimpleInjector(_container, options =>
            {
                // AddAspNetCore() wraps web requests in a Simple Injector scope.
                options.AddAspNetCore()
                    // Ensure activation of a specific framework type to be created by
                    // Simple Injector instead of the built-in configuration system.
                    .AddControllerActivation()
                    .AddViewComponentActivation()
                    .AddPageModelActivation()
                    .AddTagHelperActivation();
            });
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            #region SimpleInjector
            // UseSimpleInjector() enables framework services to be injected into
            // application components, resolved by Simple Injector.
            app.UseSimpleInjector(_container, options =>
            {
                // Add custom Simple Injector-created middleware to the ASP.NET pipeline.

                // Optionally, allow application components to depend on the
                // non-generic Microsoft.Extensions.Logging.ILogger abstraction.
                options.UseLogging();
            });

            InitializeContainer();
            #endregion

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            #region SimpleInjector
            _container.Verify();
            #endregion

            app.UseMvc();
        }
    }
}
