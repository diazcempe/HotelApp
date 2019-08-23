using AutoMapper;
using GraphQL;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using HotelApp.Api.Constants;
using HotelApp.Api.GraphQL;
using HotelApp.Api.MapperConfig;
using HotelApp.Core.Data;
using HotelApp.Core.DTOs;
using HotelApp.Core.Models;
using HotelApp.Data.Contexts;
using HotelApp.Data.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleInjector;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

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
            // Register DbContext
            // TODO to figure out how to properly register these
            //var dbOptionBuilder = new DbContextOptionsBuilder<HotelAppDbContext>()
            //    .UseSqlServer(Configuration.GetConnectionString(AppConstants.SqlServerConnectionStringName));
            //_container.Register(() => new HotelAppDbContext(dbOptionBuilder.Options), Lifestyle.Scoped);

            //_container.Register(typeof(IReservationRepository), typeof(ReservationRepository), Lifestyle.Scoped);

            #region AutoMapper
            //ConfigureAutoMapper();
            #endregion
        }
        #endregion

        #region AutoMapper
        private void ConfigureAutoMapper()
        {
            //_container.RegisterSingleton(() => _container.GetInstance<MapperProvider>().GetMapper());
            _container.RegisterSingleton(() => GetMapper(_container));
        }

        private static IMapper GetMapper(Container container)
        {
            var mp = container.GetInstance<MapperProvider>();
            return mp.GetMapper();
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

            #region NETCORE Dependency Injection
            // TODO to implement this in SimpleInjector
            services.AddDbContext<HotelAppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString(AppConstants.SqlServerConnectionStringName)));
            services.AddScoped(typeof(IReservationRepository), typeof(ReservationRepository));
            #endregion

            #region GraphQL
            services.AddScoped<IDependencyResolver>(s => new FuncDependencyResolver(s.GetRequiredService));
            services.AddScoped<HotelAppSchema>();

            services.AddGraphQL(s =>
                {
                    s.ExposeExceptions = true; //set true only in development mode. make it switchable.
                })
                .AddGraphTypes(ServiceLifetime.Scoped)
                .AddUserContextBuilder(httpContext => httpContext.User)
                .AddDataLoader();
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

            #region GraphQL
            app.UseGraphQL<HotelAppSchema>();
            app.UseGraphQLPlayground(new GraphQLPlaygroundOptions()); // to explorer API navigate https://*DOMAIN*/ui/playground
            #endregion

            app.UseMvc();

            InitializeMapper();
        }

        private static void InitializeMapper()
        {
            Mapper.Initialize(x =>
            {
                x.CreateMap<Guest, GuestDto>();
                x.CreateMap<Room, RoomDto>();
                x.CreateMap<Reservation, ReservationDto>();
            });
        }
    }
}
