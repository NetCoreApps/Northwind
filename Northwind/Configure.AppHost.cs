using Funq;
using Northwind.ServiceInterface;
using ServiceStack;
using ServiceStack.Data;
using ServiceStack.OrmLite;

[assembly: HostingStartup(typeof(Northwind.AppHost))]
namespace Northwind;

public class AppHost : AppHostBase, IHostingStartup
{
    public AppHost() : base("Northwind Web Services", typeof(CustomersService).Assembly) { }

    public void Configure(IWebHostBuilder builder) => builder
        .ConfigureServices((context, services) =>
        {
            // Register Database Connection, see: https://docs.servicestack.net/ormlite/
            services.AddSingleton<IDbConnectionFactory>(c => new OrmLiteConnectionFactory(
                context.HostingEnvironment.ContentRootPath.CombineWith("App_Data/Northwind.sqlite"),
                SqliteDialect.Provider));
        });

    public override void Configure(Container container)
    {
        // container.Register<IDbConnectionFactory>(
        //     new OrmLiteConnectionFactory(MapProjectPath("~/App_Data/Northwind.sqlite"), SqliteDialect.Provider));

        //Use Redis Cache
        //container.Register<ICacheClient>(new PooledRedisClientManager());

        VCardFormat.Register(this);

        Plugins.Add(new AutoQueryFeature { MaxLimit = 100 });

        Plugins.Add(new CorsFeature());
    }
}
