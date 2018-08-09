using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
using Olga.BLL.DTO;
using Olga.DAL.Entities;
using Olga.Models;
using Olga.Util;

[assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.config", Watch = true)]
namespace Olga
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            // внедрение зависимостей
            AutofacConfig.ConfigureContainer();
            CreateMapping();
        }

        private void CreateMapping()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<CountryDTO, Country>().MaxDepth(3)
                    .ForMember(m => m.MarketingAuthorizNumbers, opt => opt.Ignore())
                    .ForMember(m => m.PackSizes, opt => opt.Ignore())
                    .ForMember(m => m.ProductCodes, opt => opt.Ignore())
                    .ForMember(m => m.ProductNames, opt => opt.Ignore())
                ;

                cfg.CreateMap<Product, ProductDTO>().MaxDepth(3);

                cfg.CreateMap<Product, Product>()
                    .ForMember(m => m.ProductDocuments,
                        opt => opt.MapFrom(k =>
                            Mapper.Map<IEnumerable<ProductDocument>, IEnumerable<ProductDocument>>(
                                k.ProductDocuments)));

                cfg.CreateMap<ApprDocsType, ApprDocsTypeDTO>();
                    //.ForMember(x => x.Products, opt => opt.Ignore());

                cfg.CreateMap<Artwork, ArtworkDTO>();
                    //.ForMember(x => x.Products, opt => opt.Ignore());

                cfg.CreateMap<Manufacturer, ManufacturerDTO>();
                //.ForMember(x => x.Products, opt => opt.Ignore());

                cfg.CreateMap<ApprDocsTypeDTO, ApprDocsType>();
                //    .ForMember(x => x.Products, opt => opt.Ignore());

                cfg.CreateMap<ArtworkDTO, Artwork>();
                //    .ForMember(x => x.Products, opt => opt.Ignore());

                cfg.CreateMap<ManufacturerDTO, Manufacturer>();
                //    .ForMember(x => x.Products, opt => opt.Ignore());


                cfg.CreateMap<ProductDTO, ProductViewModel>()
                    .ForMember(x => x.Country, o => o.MapFrom(s => s.Country.Name))
                    .ForMember(x => x.MarketingAuthorizHolder, o => o.MapFrom(s => s.MarketingAuthorizHolder.Name))
                    .ForMember(x => x.MarketingAuthorizNumber, o => o.MapFrom(s => s.MarketingAuthorizNumber.Number))
                    .ForMember(x => x.PackSize, o => o.MapFrom(s => s.PackSize.Size))
                    .ForMember(x => x.PharmaceuticalForm, o => o.MapFrom(s => s.PharmaceuticalForm.PharmaForm))
                    .ForMember(x => x.ProductCode, o => o.MapFrom(s => s.ProductCode.Code))
                    .ForMember(x => x.ProductName, o => o.MapFrom(s => s.ProductName.Name))
                    .ForMember(x => x.Strength, o => o.MapFrom(s => s.Strength.Strngth))
                    .ForMember(x => x.ProductDocuments, o => o.MapFrom(s => s.ProductDocuments))
                    .ForMember(m => m.DocumentImages, opt => opt.Ignore())
                    .ForMember(m => m.DocumentImagesListString, opt => opt.Ignore())
                    .MaxDepth(3);

                cfg.CreateMap<ProductDTO, ProductCreateModel>()
                    .ForMember(m => m.DocumentImagesArtworks, opt => opt.Ignore())
                    .ForMember(m => m.DocumentImagesListStringArtworks, opt => opt.Ignore())
                    .ForMember(m => m.DocumentImagesApprs, opt => opt.Ignore())
                    .ForMember(m => m.DocumentImagesListStringApprs, opt => opt.Ignore());

                cfg.CreateMap<ProductDTO, ShowProductModel>().MaxDepth(3)
                    .ForMember(m => m.DocumentImagesArtworks, opt => opt.Ignore())
                    .ForMember(m => m.DocumentImagesListStringArtworks, opt => opt.Ignore())
                    .ForMember(m => m.DocumentImagesApprs, opt => opt.Ignore())
                    .ForMember(x => x.Country, o => o.MapFrom(s => s.Country.Name))
                    .ForMember(m => m.ProductName,
                        opt => opt.MapFrom(m => m.ProductName == null ? "No Product Name" : m.ProductName.Name))
                    .ForMember(m => m.ProductCode,
                        opt => opt.MapFrom(m => m.ProductCode == null ? "No Product Code" : m.ProductCode.Code))
                    .ForMember(m => m.MarketingAuthorizHolder,
                        opt => opt.MapFrom(m => m.MarketingAuthorizHolder == null ? "No Marketing Authoriz. Holder" : m.MarketingAuthorizHolder.Name))
                    .ForMember(m => m.MarketingAuthorizNumber,
                        opt => opt.MapFrom(m => m.MarketingAuthorizNumber == null ? "No Marketing Authoriz. Number" : m.MarketingAuthorizNumber.Number))
                    .ForMember(m => m.PackSize,
                        opt => opt.MapFrom(m => m.PackSize == null ? "No Pack Size" : m.PackSize.Size))
                    .ForMember(m => m.Strength,
                        opt => opt.MapFrom(m => m.Strength == null ? "No Strength" : m.Strength.Strngth))
                    .ForMember(m => m.PharmaceuticalForm,
                        opt => opt.MapFrom(m => m.PharmaceuticalForm == null ? "No Pharmaceutical Form" : m.PharmaceuticalForm.PharmaForm))
                    .ForMember(m => m.Country,
                        opt => opt.MapFrom(m => m.Country == null ? "No Country" : m.Country.Name))
                    .ForMember(m => m.IssuedDate,
                        opt => opt.MapFrom(m => m.IssuedDate == null ? null : m.IssuedDate.ToString()))
                    .ForMember(m => m.ExpiredDate,
                        opt => opt.MapFrom(m => m.ExpiredDate == null ? null : m.ExpiredDate.ToString()));

                cfg.CreateMap<ApprDocsTypeViewModel, ApprDocsTypeDTO>();
                    //.ForMember(x => x.Products, opt => opt.Ignore());

                cfg.CreateMap<ArtworkViewModel, ArtworkDTO>();
                    //.ForMember(x => x.Products, opt => opt.Ignore());

                cfg.CreateMap<ManufacturerViewModel, ManufacturerDTO>();
                //.ForMember(x => x.Products, opt => opt.Ignore());

                cfg.CreateMap<ProductViewModel, ProductDTO>().
                    ForMember(m => m.ProductDocuments, opt => opt.Ignore());

                cfg.CreateMap<ProductCreateModel, ProductDTO>().
                    ForMember(m => m.ProductDocuments, opt => opt.Ignore());
                cfg.CreateMap<ProductDTO, Product>();

                cfg.CreateMap<ProductNameViewModel, ProductNameDTO>()
                    .ForMember(x => x.Id, o => o.MapFrom(s => s.Id))
                    .ForMember(x => x.CountryId, o => o.MapFrom(s => s.CountryId))
                    .ForMember(x => x.Name, o => o.MapFrom(s => s.Name));

            });

        }
    }

}
