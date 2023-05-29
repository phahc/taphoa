using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using Microsoft.Owin.Security;
using System.Web;
using System.Diagnostics.CodeAnalysis;
using MSys.Interface;
using MSys.DataAccessLayer;

namespace AspMvcAuth
{
    [ExcludeFromCodeCoverage]
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IAuthenticationManager>(new InjectionFactory(o => HttpContext.Current.GetOwinContext().Authentication));
            container.RegisterType<IAccount, AccountDal>();
            container.RegisterType<IOrder, OrderDal>();
            container.RegisterType<IProducts, ProductsDal>();
            container.RegisterType<IMenu, MenuDal>();
            container.RegisterType<ITopics, TopicsDal>();
            container.RegisterType<ICategories, CategoriesDal>();
            container.RegisterType<ICustomer, CustomerDal>();
            container.RegisterType<IMadeIn, MadeInDal>();
            container.RegisterType<IContact, ContactDal>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}