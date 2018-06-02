using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Newtonsoft.Json;
using Olga.BLL.DTO;
using Olga.BLL.Interfaces;
using Olga.DAL.Entities;
using Olga.Models;

namespace Olga.Controllers
{
    [Authorize]
    public class ProductController : Controller
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


        // GET: Settings
        public ProductController(ICountry serv, IProductName prodName, IProductCode prodCode, IMarketingAuthorizNumber marketingAuthorizNumber, IPackSize packSize,
            IApprDocsType apprDocsType, IStrength strength, IManufacturer manufacturer, IArtwork artwork, IMarketingAuthorizHolder marketingAuthorizHolder,
            IPharmaceuticalForm pharmaceuticalForm, IProductService product)
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
        }

        // GET: Product
        public ActionResult Index(int? id)
        {
            try
            {
                var countryDto = _countryService.GetItem((int)id);
                @ViewBag.Country = countryDto.Name;
                @ViewBag.CountryId = id;
                var productsDto  = _productService.GetProducts(id);
                if (productsDto != null)
                {
                    var products = Mapper.Map<IEnumerable<ProductDTO>, List<ProductViewModel>>(productsDto);
                    return View(products.ToList());
                }

                List<ProductViewModel> nullModel = new List<ProductViewModel>()
                {
                    new ProductViewModel { Country =  countryDto.Name}
                };
                return View(nullModel);
            }
            catch (Exception e)
            {
                @ViewBag.Error = e.Message;
                return View("Error");
            }
        }

        // GET: Product
        public ActionResult CreateProduct(int? countryId)
        {
            if (countryId == 0)
            {
                @ViewBag.Error = "No countryId in request!";
                return View("Error");
            } 
            try
            {
                var model = new ProductCreateModel();
                InitialiseModel(countryId);
                return View(model);
            }
            catch (Exception e)
            {
                @ViewBag.Error = e.Message;
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult CreateProduct(ProductCreateModel model, string[] selectedApprDocsTypes, string[] selectedManufacturers, string[] selectedArtworks)
        {

            if (!ModelState.IsValid)
            {
                return View("Error");
            }
            var documentNames = model.DocumentImagesListString?.Split(',');

            try
            {
                var productDto = Mapper.Map<ProductCreateModel, ProductDTO>(model);

                AddDocumentsToProduct(ref productDto, documentNames);

                _productService.AddProduct(productDto, selectedApprDocsTypes, selectedManufacturers, selectedArtworks);

                _productService.Commit();

                var products = Mapper.Map<IEnumerable<ProductDTO>, IEnumerable<ProductViewModel>>(_productService.GetProducts(model.CountryId));
                @ViewBag.CountryId = model.CountryId;
                return View("Index",products.ToList());
            }
            catch (Exception ex)
            {
                @ViewBag.Error = ex.Message;
                return View("Error");
            }
        }

        private void AddDocumentsToProduct(ref ProductDTO product, string[] images)
        {
            if (images != null && images.Length > 0)
            {
                foreach (var name in images)
                {
                    product.ProductDocuments.Add(new ProductDocument() { PathToDocument = name });
                }
            }
        }

        [HttpGet]
        public ActionResult DeleteProduct(string Id, string CountryId)
        {
            if (string.IsNullOrEmpty(Id) || string.IsNullOrEmpty(CountryId))
            {
                @ViewBag.Error = "Error happened in Settings DeleteProduct method: no Id in GET request.";
                return View("Error");
            }
            try
            {
                _productService.DeleteProduct(int.Parse(Id));
                _productService.Commit();


                var products = Mapper.Map<IEnumerable<ProductDTO>, IEnumerable<ProductViewModel>>(_productService.GetProducts(int.Parse(CountryId)));
                @ViewBag.CountryId = CountryId;
                return View("Index", products.ToList());
            }
            catch (Exception ex)
            {
                @ViewBag.Error = ex.Message;
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult EditProduct(string id, int? countryId)
        {
            if (string.IsNullOrEmpty(id)|| countryId==null)
            {
                @ViewBag.Error = nameof(id);
                return View("Error");
            }
            try
            {
                InitialiseModel(countryId);

                var prod = _productService.GetProduct(int.Parse(id));
                var product = Mapper.Map<ProductDTO, ProductCreateModel>(prod);

                var userDocuments = prod?.ProductDocuments.Select(m => m.PathToDocument).ToList();
                product.DocumentImages = userDocuments;
                product.DocumentImagesListString = userDocuments != null ? String.Join(",", userDocuments) : String.Empty;
                
                return View("CreateProduct",product);

            }
            catch (Exception ex)
            {
                @ViewBag.Error = ex.Message;
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult EditProduct(ProductCreateModel model, string[] selectedApprDocsTypes)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                var productDto = Mapper.Map<ProductCreateModel, ProductDTO>(model);

                foreach (var apprDocsType in selectedApprDocsTypes)
                {
                    if (apprDocsType.Equals("false")) continue;
                    var apprDocsTypeToAdd = _apprDocsTypeService.GetItem(int.Parse(apprDocsType));
                    productDto.ApprDocsTypes.Add(apprDocsTypeToAdd);
                }

                //_productService.AddProduct(productDto);
                _productService.Commit();

                var products = Mapper.Map<IEnumerable<ProductDTO>, IEnumerable<ProductViewModel>>(_productService.GetProducts(model.CountryId));
                @ViewBag.CountryId = model.CountryId;
                return View("Index", products.ToList());

            }
            catch (Exception ex)
            {
                @ViewBag.Error = ex.Message;
                return View("Error");
            }
        }


        public void InitialiseModel(int? countryId)
        {
            var country = Mapper.Map<CountryDTO, CountryViewModel>(_countryService.GetItem((int)countryId));
            
            @ViewBag.ProductNames = country.ProductNames;
            @ViewBag.ProductCodes = country.ProductCodes;
            @ViewBag.MarketingAuthorizNumbers = country.MarketingAuthorizNumbers;
            @ViewBag.PackSizes = country.PackSizes;
            @ViewBag.ContryName = country.Name;
            @ViewBag.ContryId = countryId;

            var allApprDocsTypes = _apprDocsTypeService.GetItems();
            var apprDocsTypes = Mapper.Map<IEnumerable<ApprDocsTypeDTO>, IEnumerable<ApprDocsTypeViewModel>>(allApprDocsTypes).ToList();

            @ViewBag.apprDocsTypes = apprDocsTypes.Select(o => new SelectListItem
            {
                Text = o.ApprType,
                Value = o.Id.ToString()
            });

            var allArtworks = _artworkService.GetItems();
            var artworks = Mapper.Map<IEnumerable<ArtworkDTO>, IEnumerable<ArtworkViewModel>>(allArtworks).ToList();

            @ViewBag.artworks = artworks.Select(o => new SelectListItem
            {
                Text = o.Artwork_name,
                Value = o.Id.ToString()
            });

            var allManufacturers = _manufacturerService.GetItems();
            var manufacturers = Mapper.Map<IEnumerable<ManufacturerDTO>, IEnumerable<ManufacturerViewModel>>(allManufacturers).ToList();

            @ViewBag.manufacturers = manufacturers.Select(o => new SelectListItem
            {
                Text = o.Name,
                Value = o.Id.ToString()
            });

            var strengthDto = _strengthService.GetItems();
            var strength = Mapper.Map<IEnumerable<StrengthDTO>, IEnumerable<StrengthViewModel>>(strengthDto).ToList();
            @ViewBag.strength = strength;

            var marketingAuthorizHolderDto = _marketingAuthorizHolderService.GetItems();
            var marketingAuthorizHolder = Mapper.Map<IEnumerable<MarketingAuthorizHolderDTO>, IEnumerable<MarketingAuthorizHolderViewModel>>(marketingAuthorizHolderDto).ToList();
            @ViewBag.marketingAuthorizHolder = marketingAuthorizHolder;

            var pharmaceuticalFormDto = _pharmaceuticalFormService.GetItems();
            var pharmaceuticalForm = Mapper.Map<IEnumerable<PharmaceuticalFormDTO>, IEnumerable<PharmaceuticalFormViewModel>>(pharmaceuticalFormDto).ToList();
            @ViewBag.pharmaceuticalForm = pharmaceuticalForm;
        }

        public void CreateError()
        {
            var errorMessage = new StringBuilder();
            foreach (ModelState modelState in ViewData.ModelState.Values)
            {
                foreach (ModelError error in modelState.Errors)
                {
                    errorMessage.Append(error.ErrorMessage);
                }
            }
            @ViewBag.Error = errorMessage.ToString();
        }

        [HttpPost]
        public ActionResult SaveUploadedFile(int? productId)
        {
            bool isSavedSuccessfully = true;
            string fName = "";
            try
            {
                foreach (string fileName in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[fileName];
                    if (file != null && file.ContentLength > 0)
                    {
                        var targetFolder = Server.MapPath("~/Upload/Documents");
                        var id = productId != null ? productId.ToString() : "";
                        //var localFileName = String.Format("document_{0}_{1}{2}", id, Guid.NewGuid(), Path.GetExtension(file.FileName));
                        var localFileName = String.Format("{0}_{1}{2}", Path.GetFileNameWithoutExtension(file.FileName), Guid.NewGuid().ToString().Substring(0,6), Path.GetExtension(file.FileName));
                        var targetPath = Path.Combine(targetFolder, localFileName);
                        fName = localFileName;
                        file.SaveAs(targetPath);
                    }
                }
            }
            catch (Exception ex)
            {
                isSavedSuccessfully = false;
            }

            if (isSavedSuccessfully)
            {
                return Json(new { Message = fName });
            }
            return Json(new { Message = "Error in saving file" });
        }
    }
}