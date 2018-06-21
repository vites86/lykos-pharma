using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using AutoMapper.Configuration;
using Olga.BLL.DTO;
using Olga.BLL.Interfaces;
using Olga.DAL.Entities;

namespace Olga.AutoMapper
{
    public class MapperForProduct
    {
        public static IMapper GetProductMapper(IProductService productService,IProductName _productNameService)
        {

            var confExpressMap = new MapperConfigurationExpression();
            var buildMap = confExpressMap.CreateMap<ProductDTO, Product>();
            //var prodName = model.ProductNameId != null ? _productNameService.GetItem((int)model.ProductNameId) : null;
            //var authNumber = model.MarketingAuthorizNumberId != null ? _marketingAuthorizNumberService.GetItem((int)model.MarketingAuthorizNumberId) : null;
            //var phamForm = model.PharmaceuticalFormId != null ? _pharmaceuticalFormService.GetItem((int)model.PharmaceuticalFormId) : null;
            //var prodCode = model.ProductCodeId != null ? _productCodeService.GetItem((int)model.ProductCodeId) : null;
            //var strength = model.StrengthId != null ? _strengthService.GetItem((int)model.StrengthId) : null;
            //buildMap.ForMember(dest => dest.ProductName, opt => opt.MapFrom(m => _productNameService.GetItem())));
            //buildMap.ForMember(dest => dest.Manager, opt => opt.MapFrom(m => accountService.GetManagerById(m.Manager.ManagerId)));
            //buildMap.ForMember(dest => dest.UserName, opt => opt.MapFrom(c => c.UserName));
            var configuration = new MapperConfiguration(confExpressMap);
            return configuration.CreateMapper();
        }
    }
}