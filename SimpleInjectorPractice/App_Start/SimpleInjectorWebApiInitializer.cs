[assembly: WebActivator.PostApplicationStartMethod(typeof(SimpleInjectorPractice.App_Start.SimpleInjectorWebApiInitializer), "Initialize")]

namespace SimpleInjectorPractice.App_Start
{
    using System.Net.Http;
    using System;
    using System.Web.Http;
    using SimpleInjector;
    using SimpleInjector.Integration.WebApi;
    using SimpleInjector.Lifestyles;
    using SimpleInjectorPractice.Utils;

    public static class SimpleInjectorWebApiInitializer
    {
        /// <summary>Initialize the container and register it as Web API Dependency Resolver.</summary>
        public static void Initialize()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
            container.EnableHttpRequestMessageTracking(GlobalConfiguration.Configuration);

            InitializeContainer(container);

            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);
       
            container.Verify();
            
            GlobalConfiguration.Configuration.DependencyResolver =
                new SimpleInjectorWebApiDependencyResolver(container);
        }
     
        private static void InitializeContainer(Container container)
        {
            container.Register<HttpClient>(() =>
                {
                    return new HttpClient
                    {
                        BaseAddress = new Uri("https://jsonplaceholder.typicode.com/")
                    };
                },
               Lifestyle.Singleton
               );

            container.Register<IRequestMessageAccessor>(() => new RequestMessageAccessor(container));

            // For instance:
            // container.Register<IUserRepository, SqlUserRepository>(Lifestyle.Scoped);
        }
    }
}