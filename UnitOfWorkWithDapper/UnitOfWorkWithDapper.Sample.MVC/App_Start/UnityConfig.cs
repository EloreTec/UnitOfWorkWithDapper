using Microsoft.Practices.Unity;
using System;
using UnitOfWorkWithDapper.Sample.Core.AppService;
using UnitOfWorkWithDapper.Sample.Core.Contexts;
using UnitOfWorkWithDapper.Sample.Core.Data;
using UnitOfWorkWithDapper.Sample.Core.Domain;

namespace UnitOfWorkWithDapper.Sample.MVC
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        #region Unity Container

        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }

        #endregion Unity Container

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // ATTENTION: Set context for lifetime during a http request
            container.RegisterType<MyDbContext>(new PerRequestLifetimeManager()); // per request lifetime

            container.RegisterType<IProductRepository, ProductRepository>();
            container.RegisterType<IProductService, ProductService>();
            container.RegisterType<IProductApp, ProductApp>();
        }
    }
}