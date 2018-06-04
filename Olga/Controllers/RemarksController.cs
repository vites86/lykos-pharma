using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Olga.BLL.DTO;
using Olga.BLL.Interfaces;
using Olga.Models;

namespace Olga.Controllers
{
    public class RemarksController : Controller
    {
        ICountry _countryService;
        IProductName _productNameService;
        IProductCode _productCodeService;
        IMarketingAuthorizNumber _marketingAuthorizNumberService;
        IPackSize _packSizeService;
        IApprDocsType _apprDocsTypeService;
        IStrength _strengthService;
        IManufacturer _manufacturerService;
        IArtwork _artworkService;
        IMarketingAuthorizHolder _marketingAuthorizHolderService;
        IPharmaceuticalForm _pharmaceuticalFormService;
        IProductService _productService;
        IProcedure _procedureService;

        public RemarksController(ICountry serv, IProductName prodName, IProductCode prodCode, IMarketingAuthorizNumber marketingAuthorizNumber, IPackSize packSize,
            IApprDocsType apprDocsType, IStrength strength, IManufacturer manufacturer, IArtwork artwork, IMarketingAuthorizHolder marketingAuthorizHolder,
            IPharmaceuticalForm pharmaceuticalForm, IProductService product, IProcedure procedure)
        {
            _countryService = serv;
            _productNameService = prodName;
            _productCodeService = prodCode;
            _packSizeService = packSize;
            _marketingAuthorizNumberService = marketingAuthorizNumber;
            _apprDocsTypeService = apprDocsType;
            _strengthService = strength;
            _manufacturerService = manufacturer;
            _artworkService = artwork;
            _marketingAuthorizHolderService = marketingAuthorizHolder;
            _pharmaceuticalFormService = pharmaceuticalForm;
            _productService = product;
            _procedureService = procedure;
        }
        // GET: Remarks
        public ActionResult Index(int id)
        {
            var procedure = _procedureService.GetItem(id);
            var remarks = procedure.Remarks;
            var model = Mapper.Map<IEnumerable<RemarkDTO>, IEnumerable<RemarkViewModel>>(remarks).ToList();
            return View(model);
        }

        public ActionResult Create(int id)
        {
            var procedure = _procedureService.GetItem(id);
            var remarks = procedure.Remarks.FirstOrDefault(a=>a.Id == id);
            var model = Mapper.Map<RemarkDTO, RemarkViewModel>(remarks);
            return View(model);
        }


    }
}
