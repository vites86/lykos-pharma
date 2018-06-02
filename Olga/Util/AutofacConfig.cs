using System.Configuration;
using Autofac;
using Autofac.Core;
using Autofac.Integration.Mvc;
using System.Web.Mvc;
using Olga.BLL.Interfaces;
using Olga.BLL.Services;
using Olga.DAL.EF;
using Olga.DAL.Entities;
using Olga.DAL.Repositories;

namespace Olga.Util
{
    public class AutofacConfig
    {
        public static void ConfigureContainer()
        {
            // получаем экземпляр контейнера
            var builder = new ContainerBuilder();

            // регистрируем контроллер в текущей сборке
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            // регистрируем споставление типов
            string cs = ConfigurationManager.ConnectionStrings["DefaultConectionString"].ConnectionString;
            builder.RegisterType<ProductService>().As<IProductService>().WithParameter("context", new ProductContext(cs));

            builder.RegisterType<ApprDocsTypeService>().As<IApprDocsType>().WithParameter("context", new ProductContext(cs));
            builder.RegisterType<ArtworkService>().As<IArtwork>().WithParameter("context", new ProductContext(cs));
            builder.RegisterType<CountryService>().As<ICountry>().WithParameter("context", new ProductContext(cs));
            builder.RegisterType<ManufacturerService>().As<IManufacturer>().WithParameter("context", new ProductContext(cs));
            builder.RegisterType<MarketingAuthorizHolderService>().As<IMarketingAuthorizHolder>().WithParameter("context", new ProductContext(cs));
            builder.RegisterType<MarketingAuthorizNumberService>().As<IMarketingAuthorizNumber>().WithParameter("context", new ProductContext(cs));
            builder.RegisterType<PackSizeService>().As<IPackSize>().WithParameter("context", new ProductContext(cs));
            builder.RegisterType<PharmaceuticalFormService>().As<IPharmaceuticalForm>().WithParameter("context", new ProductContext(cs));
            builder.RegisterType<ProductCodeService>().As<IProductCode>().WithParameter("context", new ProductContext(cs));
            builder.RegisterType<ProductNameService>().As<IProductName>().WithParameter("context", new ProductContext(cs));
            builder.RegisterType<StrengthService>().As<IStrength>().WithParameter("context", new ProductContext(cs));
            builder.RegisterType<ProcedureService>().As<IProcedure>().WithParameter("context", new ProductContext(cs));

            // создаем новый контейнер с теми зависимостями, которые определены выше
            var container = builder.Build();

            // установка сопоставителя зависимостей
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}