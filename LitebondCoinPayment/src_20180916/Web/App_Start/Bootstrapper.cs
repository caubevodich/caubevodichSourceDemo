using System.Linq;
using Autofac;
using Autofac.Integration.Mvc;


using System.Reflection;
using System.Web.Mvc;
using Core.Data;
using WebUI;
using Core.Services;
using Core.Domain.Entities;

public class Bootstrapper
{
    public static void Run()
    {
        SetAutofacContainer();

    }
    private static void SetAutofacContainer()
    {
        var builder = new ContainerBuilder();


        builder.Register<IDbContext>(c => new MyDataContext("CryptoIODataContext")).InstancePerLifetimeScope();
        builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();

        builder.RegisterType<EntityService<LoginHistory>>().As<IEntityService<LoginHistory>>().InstancePerLifetimeScope();
        
        builder.RegisterType<WalletService>().As<IWalletService>().InstancePerLifetimeScope();
        
        

        builder.RegisterAssemblyTypes(Assembly.Load("Core"))
                   .Where(t => t.Name.EndsWith("Service"))
                   .AsImplementedInterfaces()
                   .InstancePerLifetimeScope();

        builder.RegisterControllers(typeof(MvcApplication).Assembly).PropertiesAutowired();

        var container = builder.Build();

        DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        // GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
    }
}
 