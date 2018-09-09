using System.Configuration;
using Autofac;
using Autofac.Core;
using Autofac.Integration.Mvc;
using System.Web.Mvc;
using Olga.BLL.Interfaces;
using Olga.BLL.Services;
using Olga.DAL.EF;
using Olga.DAL.Entities;
using Olga.DAL.Interfaces;
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

            var context = new ProductContext(cs);

            builder.RegisterType<ProductService>().As<IProductService>().WithParameter("context", context);
            builder.RegisterType<ApprDocsTypeService>().As<IApprDocsType>().WithParameter("context", context);
            builder.RegisterType<ArtworkService>().As<IArtwork>().WithParameter("context", context);
            builder.RegisterType<CountryService>().As<ICountry>().WithParameter("context", context);
            builder.RegisterType<ManufacturerService>().As<IManufacturer>().WithParameter("context", context);
            builder.RegisterType<MarketingAuthorizHolderService>().As<IMarketingAuthorizHolder>().WithParameter("context", context);
            builder.RegisterType<MarketingAuthorizNumberService>().As<IMarketingAuthorizNumber>().WithParameter("context", context);
            builder.RegisterType<PackSizeService>().As<IPackSize>().WithParameter("context", context);
            builder.RegisterType<PharmaceuticalFormService>().As<IPharmaceuticalForm>().WithParameter("context", context);
            builder.RegisterType<ProductCodeService>().As<IProductCode>().WithParameter("context", context);
            builder.RegisterType<ProductNameService>().As<IProductName>().WithParameter("context", context);
            builder.RegisterType<StrengthService>().As<IStrength>().WithParameter("context", context);
            builder.RegisterType<ProcedureService>().As<IProcedure>().WithParameter("context", context);

            builder.RegisterType<EfUnitOfWorkGeneral>().As<IUnitOfWorkGeneral>().WithParameter("connectionString", cs);

            // создаем новый контейнер с теми зависимостями, которые определены выше
            var container = builder.Build();

            // установка сопоставителя зависимостей
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}