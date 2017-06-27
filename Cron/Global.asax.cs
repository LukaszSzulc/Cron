using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using FluentScheduler;

namespace Cron
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            var builder = new ContainerBuilder();

            // Register your MVC controllers. (MvcApplication is the name of
            // the class in Global.asax.)
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterType<SampleInteraface>().As<ISampleInteraface>().InstancePerLifetimeScope();
            builder.RegisterType<DatabaseFake>().As<IDatabaseFake>();
            builder.RegisterType<SampleJob>();
            builder.RegisterModelBinders(typeof(MvcApplication).Assembly);
            builder.RegisterModelBinderProvider();


            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            JobManager.JobFactory = new MyFactory(container.BeginLifetimeScope());
            JobManager.Initialize(new MyRegistry());
        }
    }


    public class MyFactory : IJobFactory
    {
        private readonly ILifetimeScope _lifetimeScope;

        public MyFactory(ILifetimeScope lifetimeScope)
        {
            _lifetimeScope = lifetimeScope;
        }

        public IJob GetJobInstance<T>() where T : IJob
        {
            using (var lifeTime = _lifetimeScope.BeginLifetimeScope())
            {
                return lifeTime.Resolve<T>();
            }
        }
    }

    public class MyRegistry : Registry
    {
        public MyRegistry()
        {
            Schedule<SampleJob>().ToRunNow().AndEvery(10).Seconds();
        }
    }

    public class SampleJob : IJob
    {
        private readonly ISampleInteraface _interaface;

        public SampleJob(ISampleInteraface interaface)
        {
            _interaface = interaface;
        }
        public void Execute()
        {
            _interaface.DoSomethingUseful();
        }
    }
}
