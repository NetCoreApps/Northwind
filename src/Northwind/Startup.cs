using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Funq;
using Microsoft.Extensions.Hosting;
using ServiceStack;
using ServiceStack.Data;
using ServiceStack.Admin;
using ServiceStack.OrmLite;
using Northwind.ServiceInterface;

namespace Northwind
{
    public class Startup
    {
        IConfiguration Configuration { get; set; }
        public Startup(IConfiguration configuration) => Configuration = configuration;

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
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

            app.UseServiceStack(new AppHost {
                AppSettings = new NetCoreAppSettings(Configuration)
            });
        }
    }

    public class AppHost : AppHostBase
    {
        public AppHost() : base("Northwind Web Services", typeof(CustomersService).Assembly) { }

        public override void Configure(Container container)
        {
            container.Register<IDbConnectionFactory>(
                new OrmLiteConnectionFactory(MapProjectPath("~/App_Data/Northwind.sqlite"), SqliteDialect.Provider));

            //Use Redis Cache
            //container.Register<ICacheClient>(new PooledRedisClientManager());

            VCardFormat.Register(this);

            Plugins.Add(new AutoQueryFeature { MaxLimit = 100 });
            Plugins.Add(new AdminFeature());

            Plugins.Add(new CorsFeature());
        }
    }

}
