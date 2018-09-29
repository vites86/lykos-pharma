using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Olga.BLL.DTO;
using Olga.BLL.Interfaces;
using Olga.DAL.Entities;
using Olga.Models;

namespace Olga.Controllers
{
    [Authorize]
    public class ProcedureController : Controller
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

        public ProcedureController(ICountry serv, IProductName prodName, IProductCode prodCode, IMarketingAuthorizNumber marketingAuthorizNumber, IPackSize packSize,
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
        // GET: Procedure
        public ActionResult Index(int countryId)
        {
            if (countryId == 0)
            { 
                @ViewBag.Error = "No countryId in request!";
                return View("Error");
            }
            var errorMessage = String.Empty;
            if (!InitialiseModel(countryId, out errorMessage))
            {
                @ViewBag.Error = errorMessage;
                return View("Error");
            }
            var allProcedures = GetProcedures(countryId);
            return View(allProcedures);
        }

        public List<ProcedureViewModel> GetProcedures(int countryId)
        {
            var productsDto = _productService.GetProducts(countryId);
            var allProducts = Mapper.Map<IEnumerable<ProductDTO>, List<ProductViewModel>>(productsDto).ToArray();
            var allProcedures = new List<ProcedureViewModel>();
            foreach (var product in allProducts)
            {
                var procedures = Mapper.Map<IEnumerable<ProcedureDTO>, IEnumerable<ProcedureViewModel>>(_procedureService.GetItems(product.Id)).ToList();
                allProcedures.AddRange(procedures);
            }
            return allProcedures;
        }

        // GET: Procedure/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        // GET: Procedure/Create
        public ActionResult Create(int countryId)
        {
            if (countryId == 0)
            {
                @ViewBag.Error = "No countryId in request!";
                return View("Error");
            }
            try
            {
                var model = new ProcedureViewModel();
                var errorMessage = String.Empty;
                if (!InitialiseModel(countryId, out errorMessage))
                {
                    @ViewBag.Error = errorMessage;
                    return View("Error");
                }
                return View(model);
            }
            catch (Exception e)
            {
                @ViewBag.Error = e.Message;
                return View("Error");
            }
        }

        // POST: Procedure/Create
        [HttpPost]
        public ActionResult Create(ProcedureViewModel model, string countryId)
        {
            if (!ModelState.IsValid)
            {
                return View("Error");
            }
            try
            {
                var procedureDto = Mapper.Map<ProcedureViewModel, ProcedureDTO>(model);
                _procedureService.AddItem(procedureDto);
                _procedureService.Commit();

                var errorMessage = String.Empty;
                if (!InitialiseModel(Int32.Parse(countryId), out errorMessage))
                {
                    @ViewBag.Error = errorMessage;
                    return View("Error");
                }
                var allProcedures = GetProcedures(Int32.Parse(countryId));
                return View("Index",allProcedures);
            }
            catch (Exception ex)
            {
                @ViewBag.Error = ex.Message;
                return View("Error");
            }
        }
        // GET: Procedure/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        // POST: Procedure/Edit/5
        //[HttpPost]
        //public ActionResult Edit(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: Procedure/Delete/5
        public ActionResult Delete(int id, int countryId)
        {
            if (id==0)
            {
                @ViewBag.Error = "Error happened in DeleteProduct method: no Id in GET request.";
                return View("Error");
            }
            try
            {
                _procedureService.DeleteItem(id);
                _procedureService.Commit();

                var errorMessage = String.Empty;
                if (!InitialiseModel(countryId, out errorMessage))
                {
                    @ViewBag.Error = errorMessage;
                    return View("Error");
                }

                var allProcedures = GetProcedures(countryId);
                return View("Index", allProcedures);
            }
            catch (Exception ex)
            {
                @ViewBag.Error = ex.Message;
                return View("Error");
            }
        }

        // POST: Procedure/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public bool InitialiseModel(int? countryId, out string errorMessage)
        {
            errorMessage = String.Empty;
            var country = Mapper.Map<CountryDTO, CountryViewModel>(_countryService.GetItem((int)countryId));
            @ViewBag.CountryName = country.Name;
            @ViewBag.CountryId = countryId;
            var allProducts = _productService.GetProducts(countryId);
            if (allProducts == null)
            {
                errorMessage = $"{country.Name} don't have Products! So there are no Procedures to work with!";
                return false;
            }
            List<SelectListItem> products = allProducts.Select(n => new SelectListItem { Text = n.ProductName.Name, Value = n.Id.ToString() }).ToList();
            @ViewBag.Products = products;
            return true;
        }
    }
}
