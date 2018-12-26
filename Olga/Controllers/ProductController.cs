using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
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
using Olga.DAL.Entities.Account;
using Olga.Models;
using Olga.Util;

namespace Olga.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        readonly ICountry _countryService;
        //IProductName _productNameService;
        //IProductCode _productCodeService;
        //IMarketingAuthorizNumber _marketingAuthorizNumberService;
        //IPackSize _packSizeService;
        readonly IApprDocsType _apprDocsTypeService;
        readonly IStrength _strengthService;
        readonly IManufacturer _manufacturerService;
        readonly IArtwork _artworkService;
        readonly IMarketingAuthorizHolder _marketingAuthorizHolderService;
        readonly IMarketingAuthorizNumber _marketingAuthorizNumber;
        readonly IPharmaceuticalForm _pharmaceuticalFormService;
        readonly IProductService _productService;
        readonly IProductName _productNameService;
        readonly IProductCode _productCode;
        readonly IBaseEmailService _emailService;
        readonly IPackSize _packSizeService;
        UserViewModel _currentUser;
        bool toSend = bool.Parse(WebConfigurationManager.AppSettings["makeNotificationProd"]);
        Emailer emailer;


        // GET: Settings
        public ProductController(ICountry serv, IProductName prodName, IProductCode prodCode, IMarketingAuthorizNumber marketingAuthorizNumber, IPackSize packSize,
            IApprDocsType apprDocsType, IStrength strength, IManufacturer manufacturer, IArtwork artwork, IMarketingAuthorizHolder marketingAuthorizHolder,
            IPharmaceuticalForm pharmaceuticalForm, IProductService product, IBaseEmailService emailService, IProductName productNameService, IPackSize packSizeService,
            IProductCode productCode)
        {
            _countryService = serv;
            //_productNameService = prodName;
            //_productCodeService = prodCode;
            //_packSizeService = packSize;
            //_marketingAuthorizNumberService = marketingAuthorizNumber;
            _apprDocsTypeService = apprDocsType;
            _strengthService = strength;
            _manufacturerService = manufacturer;
            _artworkService = artwork;
            _marketingAuthorizHolderService = marketingAuthorizHolder;
            _pharmaceuticalFormService = pharmaceuticalForm;
            _productService = product;
            _emailService = emailService;
            _productNameService = productNameService;
            _marketingAuthorizNumber = marketingAuthorizNumber;
            _packSizeService = packSizeService;
            _productCode = productCode;
            emailer = new Emailer();
                emailer.Login = WebConfigurationManager.AppSettings["login"];
                emailer.Pass = WebConfigurationManager.AppSettings["password"];
                emailer.From = WebConfigurationManager.AppSettings["from"];
                emailer.Port = int.Parse(WebConfigurationManager.AppSettings["smtpPort"]);
                emailer.SmtpServer = WebConfigurationManager.AppSettings["smtpSrv"];
                emailer.DirectorMail = WebConfigurationManager.AppSettings["directorMail"];
                emailer.DeveloperMail = WebConfigurationManager.AppSettings["developerMail"];
        }

        private IUserService UserService => HttpContext.GetOwinContext().GetUserManager<IUserService>();

        // GET: Product
        public ActionResult Index(int? id)
        {
            if (id==null)
            {
                @ViewBag.Error = Resources.ErrorMessages.NoIdInRequest;
                return View("Error");
            }
            try
            {
                var countryDto = _countryService.GetItem((int)id);
                var productsDto  = _productService.GetProducts(id).ToList();
                _currentUser = GetCurrentUser();
                if (_currentUser.Countries.All(a => a.Id != id) && !User.IsInRole("Admin") && !User.IsInRole("Holder"))
                {
                    ViewBag.Error = Resources.ErrorMessages.NoPermission;
                    return View("Error");
                }
                ViewBag.Country = countryDto.Name;
                ViewBag.CountryId = id;
                ViewBag.User = _currentUser;
                if (productsDto.Count == 0)
                {
                    return View();
                }
                if (User.IsInRole("Holder"))
                {
                    productsDto = productsDto.Where(a => a.MarketingAuthorizHolderId == _currentUser.MarketingAuthorizHolder.Id).ToList();
                }

                var products = Mapper.Map<List<ProductDTO>, List<ProductViewModel>>(productsDto).OrderBy(a => a.ProductName);
                DateTime yearAndHalf = DateTime.Now.AddMonths(18);

                foreach (ProductViewModel product in products)
                {
                    if (product.ExpiredDate != null)
                    {
                        int result = DateTime.Compare((DateTime)product.ExpiredDate, yearAndHalf);
                        product.IsFired = result < 0;
                    }
                }
                return View(products.ToList());
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
        public async Task<ActionResult> CreateProduct(ProductCreateModel model, string[] selectedManufacturers, string[] selectedArtworks, string countryName)
        {
            if (!ModelState.IsValid)
            {
                return View("Error");
            }

            var documentNamesApprs = model.DocumentImagesListStringApprs?.Split(',');
            var documentNamesArtworks = model.DocumentImagesListStringArtworks?.Split(',');

            try
            {
                var userEmailsToNotify = _countryService.GetCountryUsersEmails((int)model.CountryId);
                await SenEmailAboutAddUpdateProduct(model, selectedManufacturers, selectedArtworks, countryName, documentNamesApprs, documentNamesArtworks, userEmailsToNotify);

                var productDto = Mapper.Map<ProductCreateModel, ProductDTO>(model);
                AddDocumentsToProduct(ref productDto, documentNamesApprs, documentNamesArtworks);
                _productService.AddProduct(productDto, selectedManufacturers, selectedArtworks);
                _productService.Commit();

                var userName = User.Identity.Name;
                Logger.Log.Info($"{userName}: Created/Updated Product {model.Id} ");

                TempData["Success"] = Resources.Messages.ProductCreatedSuccess;
                return RedirectToAction("Index", new {id = model.CountryId});
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
                    var newProductDocument =
                        new ProductDocument() {PathToDocument = name, ApprDocsTypeId = apprNumber };
                    var res = product.ProductDocuments.FirstOrDefault(a=>a.PathToDocument == name && a.ApprDocsTypeId == apprNumber);
                    var res2 = product.ProductDocuments.Contains(newProductDocument);
                    if (res == null && !res2)
                    {
                        product.ProductDocuments.Add(newProductDocument);
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
            _currentUser = GetCurrentUser();
            if (_currentUser.Countries.All(a => a.Id != countryId) && !User.IsInRole("Admin") && !User.IsInRole("Holder"))
            {
                @ViewBag.Error = Resources.ErrorMessages.NoPermission;
                return View("Error");
            }
            try
            {
                InitialiseModel(countryId);
                var prod = _productService.GetProduct(int.Parse(id));
                var product = Mapper.Map<ProductDTO, ShowProductModel>(prod);
                var userDocumentsApprs = new List<string>();

                if (User.IsInRole("Holder") && !product.MarketingAuthorizHolder.Equals(_currentUser.MarketingAuthorizHolder.Name))
                {
                    @ViewBag.Error = Resources.ErrorMessages.NoPermission;
                    return View("Error");
                }
                if (!User.IsInRole("Admin") && !_currentUser.NcAccess)
                {
                    userDocumentsApprs = prod?.ProductDocuments.Where(a => a.ApprDocsTypeId != null && a.ApprDocsTypeId != 3).Select(m => m.PathToDocument).ToList();
                    ViewBag.NcAccess = false;
                }
                else if(User.IsInRole("Admin") || _currentUser.NcAccess)
                {
                    ViewBag.NcAccess = true;
                    userDocumentsApprs = prod?.ProductDocuments.Where(a => a.ApprDocsTypeId != null).Select(m => m.PathToDocument).ToList();
                }
                

                //var userDocumentsApprs = prod?.ProductDocuments.Where(a => a.ApprDocsTypeId != null).Select(m => m.PathToDocument).ToList();
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
                TempData["Success"] = Resources.Messages.ProductDeletedSuccess;

                var userName = User.Identity.Name;
                Logger.Log.Info($"{userName}: Deleted Product {Id} ");


                var countryDto = _countryService.GetItem(int.Parse(CountryId));
                @ViewBag.Country = countryDto.Name;
                @ViewBag.CountryId = CountryId;
                @ViewBag.User = GetCurrentUser();
                var productsDto = _productService.GetProducts(int.Parse(CountryId));
                if (productsDto != null)
                {
                    var products = Mapper.Map<IEnumerable<ProductDTO>, List<ProductViewModel>>(productsDto).OrderBy(a => a.ProductName);
                    return View("Index", products.ToList());
                }
                return View("Index");
            }
            catch (Exception ex)
            {
                var userName = User.Identity.Name;
                Logger.Log.Error($"{userName}: DeleteProduct() {ex.Message} ");

                @ViewBag.Error = ex.Message;
                TempData["Error"] = ex.Message;
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
                        var fileTrimmName = file.FileName.Replace(",", "_").Replace("#", "№").Replace(" ", "_");
                        apprFolder = GetApprFolder(apprId);
                        var targetFolder = Server.MapPath($"~/Upload/Documents/{apprFolder}");
                        var localFileName =
                            $"{apprId + "__" + Path.GetFileNameWithoutExtension(fileTrimmName)}_{Guid.NewGuid().ToString().Substring(0, 6)}{Path.GetExtension(fileTrimmName)}";
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
                        var fileTrimmName = file.FileName.Replace(",", "_").Replace("#", "№").Replace(" ", "_");
                        artworkFolder = $"Artwork/{artworkId}";
                        var targetFolder = Server.MapPath($"~/Upload/Documents/{artworkFolder}");
                        var id = artworkId != null ? artworkId : "";
                        var localFileName =
                            $"{artworkId + "__" + Path.GetFileNameWithoutExtension(fileTrimmName)}_{Guid.NewGuid().ToString().Substring(0, 6)}{Path.GetExtension(file.FileName)}";
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
                case "5":
                    return "ApprDocType/Trademarks";
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
                        if (!System.IO.File.Exists(fileToDel)) continue;
                        System.IO.File.Delete(fileToDel);
                        _productService.DeleteDocument(fileName);
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

        public async Task SenEmailAboutAddUpdateProduct(ProductCreateModel model, string[] selectedManufacturers, string[] selectedArtworks, 
            string countryName, string[] documentNamesApprs, string[] documentNamesArtworks, IEnumerable<string> emailsToNotify)
        {
            try
            {
                if (model.ProductNameId == null) return;
                //var country = _countryService.GetItem();

                string subject;
                var body = new StringBuilder();

                var productName = _productNameService.GetItem((int)model.ProductNameId).Name;

                if (model.Id == null)
                {
                    subject = string.Concat((Resources.Email.SubjectProductCreate.Replace("(name)", productName)), $" in {countryName}");
                    body.Append(Resources.Email.BodyProductCreate.Replace("(name)", productName) + $" in {countryName}" + Resources.Email.Signature);
                    var emailerDto = Mapper.Map<Emailer,EmailerDTO>(emailer);
                    await _emailService.SendEmailNotification(body.ToString(), subject, emailerDto,emailsToNotify, toSend);
                }
                else
                {
                    subject = string.Concat((Resources.Email.SubjectProductUpdate.Replace("(name)", productName)), $" in {countryName}");
                    body.Append(Resources.Email.BodyProductUpdate.Replace("(name)", productName) + $" in {countryName}");
                    var bodyCompared = await CreateBodyText(model, selectedManufacturers, selectedArtworks, documentNamesApprs, documentNamesArtworks);
                    if (!string.IsNullOrEmpty(bodyCompared))
                    {
                        body.Append(":<br>");
                        body.Append(bodyCompared);
                        body.Append(Resources.Email.Signature);
                        var emailerDto = Mapper.Map<Emailer, EmailerDTO>(emailer);
                        await _emailService.SendEmailNotification(body.ToString(), subject, emailerDto, emailsToNotify, toSend);
                    }
                }
            }
            catch (Exception ex)
            {
                var userName = User.Identity.Name;
                Logger.Log.Error($"{userName}: SenEmailAboutAddUpdateProduct() {ex.Message} ");
            }
        }

        public async Task<string> CreateBodyText(ProductCreateModel model, string[] selectedManufacturers, string[] selectedArtworks, string[] documentNamesApprs, string[] documentNamesArtworks)
        {
            var bodyStr = new StringBuilder();
            var _bodyForDropdowns = GetBodyForDropDowns(model);
            var _bodyForArtworks = GetBodyForArtworks(model, selectedArtworks);
            var _bodyForManufacturers = GetBodyForManufacturers(model, selectedManufacturers);
            var _bodyForDocuments = GetBodyForDocuments(model, documentNamesApprs, documentNamesArtworks);
            await Task.WhenAll(_bodyForDropdowns, _bodyForArtworks, _bodyForManufacturers, _bodyForDocuments);
            var bodyForDropdowns = _bodyForDropdowns.Result;
            var bodyForArtworks = _bodyForArtworks.Result;
            var bodyForManufacturers = _bodyForManufacturers.Result;
            var bodyForDocuments = _bodyForDocuments.Result;

            bodyStr.Append(bodyForDropdowns);
            bodyStr.Append(bodyForArtworks);
            bodyStr.Append(bodyForManufacturers);
            bodyStr.Append(bodyForDocuments);
            return bodyStr.ToString();
        }

        public async Task<string> GetBodyForDropDowns(ProductCreateModel model)
        {
            var bodyStr = new StringBuilder();
            if (model.Id == null) return string.Empty;
            var oldProduct = _productService.GetProduct((int)model.Id);
            //var _oldProduct = Mapper.Map<ProductDTO, ProductCompareModel>(oldProduct); 

            //Type newType = Type.GetType("Olga.Models.ProductCreateModel", false, true);
            //Type oldType = Type.GetType("Olga.Models.ProductCompareModel", false, true);

            //foreach (var property in newType.GetProperties())
            //{
            //    var oldProperty = oldType.GetProperties().FirstOrDefault(a => a.Name.Equals(property.Name));
            //    if (oldProperty==null) continue;
            //    object[] attrs = oldProperty.GetCustomAttributes(true);
            //    if (!attrs.Any(a => a is IgnoreDataMemberAttribute))
            //    {
            //        var newValue = property.GetValue(model) == null ? "No value" : property.GetValue(model).ToString();
            //        var oldValue = oldProperty.GetValue(_oldProduct) == null ? "No value" : oldProperty.GetValue(_oldProduct).ToString();
            //        if (!oldValue.Equals(newValue))
            //        {
            //            bodyStr.Append($"<strong>{property.Name}</strong> changed to {newValue}<br>");
            //        }
            //    }
            //}

            if (oldProduct.IssuedDate != model.IssuedDate)
            {
                bodyStr.Append($"<strong>IssuedDate</strong> changed to {model.IssuedDate.ToString().Substring(0, 10)}<br>");
            }
            if (oldProduct.MarketingAuthorizHolderId != model.MarketingAuthorizHolderId && model.MarketingAuthorizHolderId != null)
            {
                var marketingAuthorizHolder = _marketingAuthorizHolderService.GetItem((int)model.MarketingAuthorizHolderId);
                bodyStr.Append($"<strong>MarketingAuthorizHolder</strong> changed to {marketingAuthorizHolder.Name}<br>");
            }
            if (oldProduct.MarketingAuthorizNumberId != model.MarketingAuthorizNumberId && model.MarketingAuthorizNumberId != null)
            {
                var marketingAuthorizNumber = _marketingAuthorizNumber.GetItem((int)model.MarketingAuthorizNumberId);
                bodyStr.Append($"<strong>MarketingAuthorizNumber</strong> changed to {marketingAuthorizNumber.Number}<br>");
            }
            if (oldProduct.PackSizeId != model.PackSizeId && model.PackSizeId != null)
            {
                var packSize = _packSizeService.GetItem((int)model.PackSizeId);
                bodyStr.Append($"<strong>PackSize</strong> changed to {packSize.Size}<br>");
            }
            if (oldProduct.PharmaceuticalFormId != model.PharmaceuticalFormId && model.PharmaceuticalFormId != null)
            {
                var pharmaceuticalForm = _pharmaceuticalFormService.GetItem((int)model.PharmaceuticalFormId);
                bodyStr.Append($"<strong>PharmaceuticalForm</strong> changed to {pharmaceuticalForm.PharmaForm}<br>");
            }
            if (oldProduct.ProductNameId != model.ProductNameId && model.ProductNameId != null)
            {
                var productName = _productNameService.GetItem((int)model.ProductNameId);
                bodyStr.Append($"<strong>ProductName</strong> changed to {productName.Name}<br>");
            }
            if (oldProduct.ProductCodeId != model.ProductCodeId && model.ProductCodeId != null)
            {
                var productCode = _productCode.GetItem((int)model.ProductCodeId);
                bodyStr.Append($"<strong>ProductCode</strong> changed to {productCode.Code}<br>");
            }
            if (oldProduct.StrengthId != model.StrengthId && model.StrengthId != null)
            {
                var strength = _strengthService.GetItem((int)model.StrengthId);
                bodyStr.Append($"<strong>Strength</strong> changed to {strength.Strngth}<br>");
            }
            if (oldProduct.UnLimited != model.UnLimited && model.UnLimited)
            {
                bodyStr.Append($"<strong>ExpiredDate</strong> changed to UnLimited");
            }
            else if (!model.UnLimited && oldProduct.ExpiredDate != model.ExpiredDate)
            {
                bodyStr.Append($"<strong>ExpiredDate</strong> changed to {model.ExpiredDate}<br>");
            }
            return bodyStr.ToString();
        }

        public async Task<string> GetBodyForArtworks(ProductCreateModel model, string[] selectedArtworks)
        {
            var bodyStr = new StringBuilder();
            if (model.Id == null || selectedArtworks == null) return string.Empty;
            var oldProduct = _productService.GetProduct((int)model.Id);

            foreach (var id in selectedArtworks)
            {
                if (string.IsNullOrEmpty(id)) continue;
                var newArtworkId = int.Parse(id);
                if (oldProduct.Artworks.Any(a => a.Id == newArtworkId)) continue;
                var artwork = _artworkService.GetItem(newArtworkId);
                bodyStr.Append($"<strong>To Artworks added </strong> {artwork.Artwork_name}<br>");
            }

            foreach (var oldArtwork in oldProduct.Artworks)
            {
                if (selectedArtworks.Contains(oldArtwork.Id.ToString())) continue;
                var artwork = _artworkService.GetItem(oldArtwork.Id);
                bodyStr.Append($"<strong>From Artworks was deleted: </strong> {artwork.Artwork_name}<br>");
            }

            return bodyStr.ToString();

        }

        public async Task<string> GetBodyForManufacturers(ProductCreateModel model, string[] selectedManufacturers)
        {
            var bodyStr = new StringBuilder();
            if (model.Id == null || selectedManufacturers == null) return string.Empty;
            var oldProduct = _productService.GetProduct((int)model.Id);

            foreach (var id in selectedManufacturers)
            {
                if (string.IsNullOrEmpty(id)) continue;
                var newManufacturerId = int.Parse(id);
                if (oldProduct.Manufacturers.Any(a => a.Id == newManufacturerId)) continue;
                var manufacturer = _manufacturerService.GetItem(newManufacturerId);
                bodyStr.Append($"<strong>To Manufacturers added </strong> {manufacturer.Name}<br>");
            }

            foreach (var oldManufecturer in oldProduct.Manufacturers)
            {
                if (selectedManufacturers.Contains(oldManufecturer.Id.ToString())) continue;
                var manufacturer = _manufacturerService.GetItem(oldManufecturer.Id);
                bodyStr.Append($"<strong>From Manufecturers was deleted: </strong> {manufacturer.Name}<br>");
            }

            return bodyStr.ToString();
        }

        public async Task<string> GetBodyForDocuments(ProductCreateModel model, string[] documentNamesApprs, string[] documentNamesArtworks)
        {
            var bodyStr = new StringBuilder();
            if (model.Id == null) return string.Empty;
            var productDto = Mapper.Map<ProductCreateModel, ProductDTO>(model);
            var oldProduct = _productService.GetProduct((int)model.Id);

            if (documentNamesApprs != null && documentNamesApprs.Length > 0)
            {
                foreach (var name in documentNamesApprs)
                {
                    if (name.IndexOf("__") == -1) continue;
                    var apprNumberString = name.Substring(0, name.IndexOf("__"));
                    var apprNumber = Int32.Parse(apprNumberString);
                    var newProductDocument = new ProductDocument() { PathToDocument = name, ApprDocsTypeId = apprNumber };
                    var res = oldProduct.ProductDocuments.FirstOrDefault(a => a.PathToDocument == name && a.ApprDocsTypeId == apprNumber);
                    var res2 = oldProduct.ProductDocuments.Contains(newProductDocument);
                    if (res == null && !res2)
                    {
                        bodyStr.Append($"<strong>To ApprDocsTypes added document:</strong> {name}<br>");
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
                    var newProductDocument = new ProductDocument() { PathToDocument = name, ArtworkId = artworkNumber };
                    var res = oldProduct.ProductDocuments.FirstOrDefault(a => a.PathToDocument == name && a.ArtworkId == artworkNumber);
                    var res2 = oldProduct.ProductDocuments.Contains(newProductDocument);
                    if (res == null && !res2)
                    {
                        bodyStr.Append($"<strong>To Artworks Documents added document:</strong> {name}<br>");
                    }
                }
            }
            return bodyStr.ToString();
        }
       
    }
}