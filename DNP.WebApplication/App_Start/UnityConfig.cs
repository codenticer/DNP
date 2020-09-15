using DNP.Business.Account;
using DNP.Business.Paykum;
using DNP.Business.ProductDetails;
using DNP.Repository.AccountRepositry;
using DNP.Repository.PaykumRepositry;
using DNP.Repository.ProductDetailsRepositry;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace DNP.WebApplication
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            container.RegisterType<IAccountService,AccountService>();
            container.RegisterType<IProductDetailsService, ProductDetailsService>();
            container.RegisterType<IPaykumService, PaykumService>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}