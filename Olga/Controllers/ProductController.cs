using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json;
using Olga.AutoMapper;
using Olga.BLL.DTO;
using Olga.BLL.Interfaces;
using Olga.BLL.Services;
using Olga.DAL.Entities;
using Olga.Models;
using Olga.Util;

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
        UserViewModel currentUser;


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

        private IUserService UserService
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<IUserService>();
            }
        }

        // GET: Product
        public ActionResult Index(int? id)
        {
            try
            {
                var countryDto = _countryService.GetItem((int)id);
                @ViewBag.Country = countryDto.Name;
                @ViewBag.CountryId = id;
                var productsDto  = _productService.GetProducts(id).ToList();
                currentUser = GetCurrentUser();
                ViewBag.User = currentUser;
                if (productsDto.Count > 0)
                {
                    var products = Mapper.Map<List<ProductDTO>, List<ProductViewModel>>(productsDto);
                    return View(products);
                }
                return View();
            }
            catch (Exception e)
            {
                var userName = User.Identity.Name;
                Logger.Log.Error($"{userName}: Index() {e.Message} ");

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
                var userName = User.Identity.Name;
                Logger.Log.Error($"{userName}: CreateProduct() {e.Message} ");

                @ViewBag.Error = e.Message;
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult CreateProduct(ProductCreateModel model, string[] selectedManufacturers, string[] selectedArtworks, string CountryName)
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
                //var prodName = model.ProductNameId !=null ? _productNameService.GetItem((int)model.ProductNameId) : null;
                //productDto.ProductName = Mapper.Map<ProductNameDTO, ProductName> (prodName);
                //productDto.ProductNameId = model.ProductNameId;

                AddDocumentsToProduct(ref productDto, documentNamesApprs, documentNamesArtworks);
                _productService.AddProduct(productDto, selectedManufacturers, selectedArtworks);
                _productService.Commit();

                var userName = User.Identity.Name;
                Logger.Log.Info($"{userName}: Created/Updated Product {model.Id} ");

                var products = Mapper.Map<IEnumerable<ProductDTO>, IEnumerable<ProductViewModel>>(_productService.GetProducts(model.CountryId));
                @ViewBag.CountryId = model.CountryId;
                @ViewBag.Country = CountryName;
                @ViewBag.User = GetCurrentUser();
                return View("Index",products.ToList());
            }
            catch (Exception ex)
            {
                var userName = User.Identity.Name;
                Logger.Log.Error($"{userName}: CreateProduct() {ex.Message} ");

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
                    if(name.IndexOf("__") == -1) continue;
                    var apprNumberString = name.Substring(0, name.IndexOf("__"));
                    var apprNumber = Int32.Parse(apprNumberString);
                    //if (apprNumberString.Length > 15) apprNumberString = apprNumberString.Substring(apprNumberString.LastIndexOf("/"), apprNumberString.Length - apprNumberString.LastIndexOf("/")).Replace("/", "");
                    var newProduct =
                        new ProductDocument() {PathToDocument = name, ApprDocsTypeId = apprNumber };
                    var res = product.ProductDocuments.FirstOrDefault(a=>a.PathToDocument == name && a.ApprDocsTypeId == apprNumber);
                    var res2 = product.ProductDocuments.Contains(newProduct);
                    if (res == null && !res2)
                    {
                        product.ProductDocuments.Add(newProduct);
                    }
                    
                }
            }

            if (documentNamesArtworks != null && documentNamesArtworks.Length > 0)
            {
                foreach (var name in documentNamesArtworks)
                {
                    if (name.IndexOf("__") == -1) continue;
                    var artworkNumberString = name.Substring(0, name.IndexOf("__"));
                    var artworkNumber = Int32.Parse(artworkNumberString);
                    //if (apprNumberString.Length > 15) apprNumberString = apprNumberString.Substring(apprNumberString.LastIndexOf("/"), apprNumberString.Length - apprNumberString.LastIndexOf("/")).Replace("/", "");
                    var newProduct =
                        new ProductDocument() { PathToDocument = name, ArtworkId = artworkNumber };
                    var res = product.ProductDocuments.FirstOrDefault(a => a.PathToDocument == name && a.ArtworkId == artworkNumber);
                    var res2 = product.ProductDocuments.Contains(newProduct);
                    if (res == null && !res2)
                    {
                        product.ProductDocuments.Add(newProduct);
                    }

                }
            }
        }

        [HttpGet]
        public ActionResult ShowDocuments(string id, int? countryId)
        {
            if (string.IsNullOrEmpty(id) || countryId == null)
            {
                @ViewBag.Error = nameof(id);
                return View("Error");
            }
            try
            {
                InitialiseModel(countryId);

                var prod = _productService.GetProduct(int.Parse(id));
                var product = Mapper.Map<ProductDTO, ShowProductModel>(prod);

                var userDocumentsApprs = prod?.ProductDocuments.Where(a => a.ApprDocsTypeId != null).Select(m => m.PathToDocument).ToList();
                var userDocumentsArtworks = prod?.ProductDocuments.Where(a => a.ArtworkId != null).Select(m => m.PathToDocument).ToList();

                product.DocumentImagesArtworks = userDocumentsArtworks;
                product.DocumentImagesApprs = userDocumentsApprs;
                product.DocumentImagesListStringApprs = userDocumentsApprs != null ? String.Join(",", userDocumentsApprs) : String.Empty;
                product.DocumentImagesListStringArtworks = userDocumentsArtworks != null ? String.Join(",", userDocumentsArtworks) : String.Empty;

                return View(product);
            }
            catch (Exception ex)
            {
                var userName = User.Identity.Name;
                Logger.Log.Error($"{userName}: ShowProduct() {ex.Message} ");

                @ViewBag.Error = ex.Message;
                return View("Error");
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

                var userName = User.Identity.Name;
                Logger.Log.Info($"{userName}: Deleted Product {Id} ");


                var countryDto = _countryService.GetItem(int.Parse(CountryId));
                @ViewBag.Country = countryDto.Name;
                @ViewBag.CountryId = CountryId;
                @ViewBag.User = GetCurrentUser();
                var productsDto = _productService.GetProducts(int.Parse(CountryId));
                if (productsDto != null)
                {
                    var products = Mapper.Map<IEnumerable<ProductDTO>, List<ProductViewModel>>(productsDto);
                    return View("Index", products.ToList());
                }
                return View("Index");
            }
            catch (Exception ex)
            {
                var userName = User.Identity.Name;
                Logger.Log.Error($"{userName}: DeleteProduct() {ex.Message} ");

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

               
                return View("CreateProduct",product);

            }
            catch (Exception ex)
            {
                var userName = User.Identity.Name;
                Logger.Log.Error($"{userName}: EditProduct() {ex.Message} ");

                @ViewBag.Error = ex.Message;
                return View("Error");
            }
        }

        public void InitialiseModel(int? countryId)
        {
            var country = Mapper.Map<CountryDTO, CountryViewModel>(_countryService.GetItem((int)countryId));
            
            @ViewBag.ProductNames = country.ProductNames.OrderBy(a=>a.Name);
            @ViewBag.ProductCodes = country.ProductCodes.OrderBy(a => a.Code);
            @ViewBag.MarketingAuthorizNumbers = country.MarketingAuthorizNumbers.OrderBy(a => a.Number);
            @ViewBag.PackSizes = country.PackSizes.OrderBy(a => a.Size);
            @ViewBag.ContryName = country.Name;
            @ViewBag.ContryId = countryId;
            @ViewBag.User = GetCurrentUser();

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

            var allManufacturers = _manufacturerService.GetItems().OrderBy(a=>a.Name);
            var manufacturers = Mapper.Map<IEnumerable<ManufacturerDTO>, IEnumerable<ManufacturerViewModel>>(allManufacturers).ToList();

            @ViewBag.manufacturers = manufacturers.Select(o => new SelectListItem
            {
                Text = o.Name,
                Value = o.Id.ToString()
            });

            var strengthDto = _strengthService.GetItems();
            var strength = Mapper.Map<IEnumerable<StrengthDTO>, IEnumerable<StrengthViewModel>>(strengthDto).ToList();
            @ViewBag.strength = strength;

            var marketingAuthorizHolderDto = _marketingAuthorizHolderService.GetItems().OrderBy(a => a.Name);
            var marketingAuthorizHolder = Mapper.Map<IEnumerable<MarketingAuthorizHolderDTO>, IEnumerable<MarketingAuthorizHolderViewModel>>(marketingAuthorizHolderDto).ToList();
            @ViewBag.marketingAuthorizHolder = marketingAuthorizHolder;

            var pharmaceuticalFormDto = _pharmaceuticalFormService.GetItems().OrderBy(a => a.PharmaForm);
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
            string targetPath = "";
            string apprFolder = "";
            try
            {
                foreach (string fileName in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[fileName];
                    if (file != null && file.ContentLength > 0)
                    {
                        apprFolder = GetApprFolder(apprId);
                        var targetFolder = Server.MapPath($"~/Upload/Documents/{apprFolder}");
                        var localFileName = String.Format("{0}_{1}{2}", apprId + "__" + Path.GetFileNameWithoutExtension(file.FileName), Guid.NewGuid().ToString().Substring(0,6), Path.GetExtension(file.FileName));
                        targetPath = Path.Combine(targetFolder, localFileName);
                        fName = localFileName;
                        file.SaveAs(targetPath);
                    }
                }
            }
            catch (Exception ex)
            {
                var userName = User.Identity.Name;
                Logger.Log.Error($"{userName}: SaveUploadedFile() {ex.Message} ");

                isSavedSuccessfully = false;
            }

            if (isSavedSuccessfully)
            {
                return Json(new { Message = fName, Folder = apprFolder });
            }
            return Json(new { Message = "Error in saving file" });
        }

        [HttpPost]
        public ActionResult SaveArtworkUploadedFile(string artworkId)
        {
            bool isSavedSuccessfully = true;
            string fName = "";
            string targetPath = "";
            string artworkFolder = "";
            try
            {
                foreach (string fileName in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[fileName];
                    if (file != null && file.ContentLength > 0)
                    {
                        artworkFolder = $"Artwork/{artworkId}";
                        var targetFolder = Server.MapPath($"~/Upload/Documents/{artworkFolder}");
                        var id = artworkId != null ? artworkId.ToString() : "";
                        //var localFileName = String.Format("document_{0}_{1}{2}", id, Guid.NewGuid(), Path.GetExtension(file.FileName));
                        var localFileName = String.Format("{0}_{1}{2}", artworkId + "__" + Path.GetFileNameWithoutExtension(file.FileName), Guid.NewGuid().ToString().Substring(0, 6), Path.GetExtension(file.FileName));
                        targetPath = Path.Combine(targetFolder, localFileName);
                        fName = localFileName;
                        file.SaveAs(targetPath);
                    }
                }
            }
            catch (Exception ex)
            {
                var userName = User.Identity.Name;
                Logger.Log.Error($"{userName}: SaveArtworkUploadedFile() {ex.Message} ");

                isSavedSuccessfully = false;
            }

            if (isSavedSuccessfully)
            {
                return Json(new { Message = fName, Folder = artworkFolder });
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
                var userName = User.Identity.Name;
                Logger.Log.Error($"{userName}: DeleteUploadedFile() {e.Message} ");
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
        public ActionResult DeleteFile(string fileName)
        {
            var userName = User.Identity.Name;
            Logger.Log.Info($"{userName}: Deleted file {fileName}");

            if (string.IsNullOrEmpty(fileName))
            {
                return Json(new { Message = "File deleted!" });
            } 
            try
            {
                var dirs = Directory.GetDirectories(Server.MapPath("~/Upload/Documents/"));
                foreach (var dir in dirs)
                {
                    var subDirs = Directory.GetDirectories(dir);
                    foreach (var subDir in subDirs)
                    {
                        var fileToDeleteList = Directory.GetFiles(subDir, fileName);
                        if (fileToDeleteList.Length == 0) continue;
                        var fileToDel = fileToDeleteList[0];
                        if(string.IsNullOrEmpty(fileToDel)) continue;
                        if (System.IO.File.Exists(fileToDel))
                        {
                            System.IO.File.Delete(fileToDel);
                            _productService.DeleteDocument(fileName);
                        }
                    }

                   
                }
                return Json(new { Message = "File deleted!" });
            }
            catch (Exception ex)
            {
                Logger.Log.Error($"{userName}: DeleteFile() {ex.Message} ");

                return Json(new { Message = ex.Message });
            }
        }
        public UserViewModel GetCurrentUser()
        {
            try
            {
                var userId = HttpContext.User.Identity.GetUserId();
                var user = UserService.GetUser(userId);
                var userMapper = MapperForUser.GetUserMapperForView(UserService);
                return userMapper.Map<UserDTO, UserViewModel>(user);
            }
            catch (Exception ex)
            {
                return new UserViewModel();
            }
        }
    }
}