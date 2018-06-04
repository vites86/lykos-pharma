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

                //List<ProductViewModel> nullModel = new List<ProductViewModel>()
                //{
                //    new ProductViewModel { Country =  countryDto.Name}
                //};
                return View();
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
        public ActionResult CreateProduct(ProductCreateModel model, string[] selectedManufacturers, string[] selectedArtworks)
        {
            if (!ModelState.IsValid)
            {
                return View("Error");
            }

            var documentNamesApprs = model.DocumentImagesListStringApprs?.Split(',');
            var documentNamesArtworks = model.DocumentImagesListStringArtworks?.Split(',');

            try
            {
                var productDto = Mapper.Map<ProductCreateModel, ProductDTO>(model);

                AddDocumentsToProduct(ref productDto, documentNamesApprs, documentNamesArtworks);

                _productService.AddProduct(productDto, selectedManufacturers, selectedArtworks);

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

        private void AddDocumentsToProduct(ref ProductDTO product, string[] documentNamesApprs, string[] documentNamesArtworks)
        {
            if (documentNamesApprs != null && documentNamesApprs.Length > 0)
            {
                foreach (var name in documentNamesApprs)
                {
                    var apprNumber = name.Substring(0, name.IndexOf("__"));
                    if (apprNumber.Length > 15) apprNumber = apprNumber.Substring(apprNumber.LastIndexOf("/"), apprNumber.Length - apprNumber.LastIndexOf("/")).Replace("/", "");

                    if (name.Contains("/Upload/Documents/"))
                    {
                        product.ProductDocuments.Add(new ProductDocument() { PathToDocument = name, ApprDocsTypeId = Int32.Parse(apprNumber) });
                    }
                    else
                    {
                        //var folder = GetApprFolder(apprNumber);
                        product.ProductDocuments.Add(new ProductDocument() { PathToDocument = name, ApprDocsTypeId = Int32.Parse(apprNumber) });
                    }

                }
            }

            if (documentNamesArtworks != null && documentNamesArtworks.Length > 0)
            {
                foreach (var name in documentNamesArtworks)
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

                var userDocumentsApprs = prod?.ProductDocuments.Where(a=>a.ApprDocsTypeId!=null).Select(m => m.PathToDocument).ToList();
                var userDocumentsArtworks = prod?.ProductDocuments.Where(a => a.ArtworkId != null).Select(m => m.PathToDocument).ToList();

                product.DocumentImagesArtworks = userDocumentsArtworks;
                product.DocumentImagesApprs = userDocumentsApprs;
                product.DocumentImagesListStringApprs = userDocumentsApprs != null ? String.Join(",", userDocumentsApprs) : String.Empty;
                product.DocumentImagesListStringArtworks = userDocumentsArtworks != null ? String.Join(",", userDocumentsArtworks) : String.Empty;

                var DocumentImagesListStringApprs1 = userDocumentsApprs?.FirstOrDefault(stringToCheck  => stringToCheck.Contains("1__"));
                var DocumentImagesListStringApprs2 = userDocumentsApprs?.FirstOrDefault(stringToCheck  => stringToCheck.Contains("2__"));
                var DocumentImagesListStringApprs3 = userDocumentsApprs?.FirstOrDefault(stringToCheck  => stringToCheck.Contains("3__"));
                var DocumentImagesListStringApprs4 = userDocumentsApprs?.FirstOrDefault(stringToCheck  => stringToCheck.Contains("4__"));

                ViewBag.DocumentImagesApprs1 = DocumentImagesListStringApprs1 != null ? String.Join(",", DocumentImagesListStringApprs1) : null;
                ViewBag.DocumentImagesApprs2 = DocumentImagesListStringApprs2 != null ? String.Join(",", DocumentImagesListStringApprs2) : null;
                ViewBag.DocumentImagesApprs3 = DocumentImagesListStringApprs3 != null ? String.Join(",", DocumentImagesListStringApprs3) : null;
                ViewBag.DocumentImagesApprs4 = DocumentImagesListStringApprs4 != null ? String.Join(",", DocumentImagesListStringApprs4) : null;

                ViewBag.DocumentImagesListStringApprs1=DocumentImagesListStringApprs1;
                ViewBag.DocumentImagesListStringApprs2=DocumentImagesListStringApprs2;
                ViewBag.DocumentImagesListStringApprs3=DocumentImagesListStringApprs3;
                ViewBag.DocumentImagesListStringApprs4 = DocumentImagesListStringApprs4;


                return View("CreateProduct",product);

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
        public ActionResult SaveUploadedFile(string apprId)
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
                        var apprFolder = GetApprFolder(apprId);
                        var targetFolder = Server.MapPath($"~/Upload/Documents/{apprFolder}");
                        var id = apprId != null ? apprId.ToString() : "";
                        //var localFileName = String.Format("document_{0}_{1}{2}", id, Guid.NewGuid(), Path.GetExtension(file.FileName));
                        var localFileName = String.Format("{0}_{1}{2}", apprId + "__" + Path.GetFileNameWithoutExtension(file.FileName), Guid.NewGuid().ToString().Substring(0,6), Path.GetExtension(file.FileName));
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

        public void DeleteUploadedFile(int productId)
        {
            var product = _productService.GetProduct(productId);
            try
            {
                for (int i = 0; i < product.ProductDocuments.Count; i++)
                {
                    if(product.ProductDocuments[i].ApprDocsTypeId==null) continue;
                    var apprFolder = GetApprFolder(product.ProductDocuments[i].ApprDocsTypeId.ToString());
                    var targetPath = Server.MapPath($"~/Upload/Documents/{apprFolder}/{product.ProductDocuments[i].PathToDocument}");
                    if (System.IO.File.Exists(targetPath))
                    {
                        System.IO.File.Delete(targetPath);
                    }
                }
            }
            catch (Exception e)
            {
            }
        }

        public static string GetApprFolder(string apprId)
        {
            switch (apprId)
            {
                case "1":
                    return "ApprDocType/RegistrationCertificate";
                case "2":
                    return $"ApprDocType/PIL";
                case "3":
                    return "ApprDocType/NDMQC";
                case "4":
                    return "ApprDocType/PackMaterialsLabelling";
            }
            return "/";
        }
    }
}