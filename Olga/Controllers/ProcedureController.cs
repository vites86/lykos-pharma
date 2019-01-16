using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using AutoMapper;
using Ionic.Zip;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using NUnrar.Archive;
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
        readonly IBaseEmailService _emailService;
        IProcedure _procedureService;
        bool toSend = bool.Parse(WebConfigurationManager.AppSettings["makeNotificationProc"]);
        Emailer emailer;
        private UserViewModel _currentUser;
        IArchProccessor _archProccessor;



        public ProcedureController(ICountry serv, IProductName prodName, IProductCode prodCode, IMarketingAuthorizNumber marketingAuthorizNumber, IPackSize packSize,
            IApprDocsType apprDocsType, IStrength strength, IManufacturer manufacturer, IArtwork artwork, IMarketingAuthorizHolder marketingAuthorizHolder,
            IPharmaceuticalForm pharmaceuticalForm, IProductService product, IProcedure procedure,IBaseEmailService emailService, IArchProccessor archProccessor)
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
            _emailService = emailService;
            _procedureService = procedure;
            _archProccessor = archProccessor;
            emailer = new Emailer()
            {
                Login = WebConfigurationManager.AppSettings["login"],
                Pass = WebConfigurationManager.AppSettings["password"],
                From = WebConfigurationManager.AppSettings["from"],
                Port = int.Parse(WebConfigurationManager.AppSettings["smtpPort"]),
                SmtpServer = WebConfigurationManager.AppSettings["smtpSrv"],
                DirectorMail = WebConfigurationManager.AppSettings["directorMail"],
                DeveloperMail = WebConfigurationManager.AppSettings["developerMail"],
            };
            _currentUser = GetCurrentUser();
        }
        // GET: Procedure
        public ActionResult Index(int? countryId)
        {
            if (countryId == 0 || countryId == null)
            { 
                ViewBag.Error = Resources.ErrorMessages.NoIdInRequest;
                return View("Error");
            }

            if (!InitialiseModel(countryId, out var errorMessage))
            {
                ViewBag.Error = errorMessage;
                return View("Error");
            }

            _currentUser = GetCurrentUser();
            /*Todo add to check && !User.IsInRole("Holder")*/
            if (_currentUser.Countries.All(a => a.Id != countryId) && !User.IsInRole("Admin") )
            {
                ViewBag.Error = Resources.ErrorMessages.NoPermission;
                return View("Error");
            }
            ViewBag.User = _currentUser;

            var allProcedures = GetProcedures((int)countryId);
            return View(allProcedures);
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
                errorMessage = $"{country.Name} {Resources.ErrorMessages.NoProcCauseNoProd}";
                return false;
            }
            List<SelectListItem> products = 
                allProducts.Select(n => new SelectListItem { Text = n.ProductName?.Name ?? "No name", Value = n.Id.ToString() }).ToList();
            @ViewBag.Products = products;
            return true;
        }

        public List<ProcedureViewModel> GetProcedures(int countryId)
        {
            var productsDto = _productService.GetProducts(countryId);
            if (User.IsInRole("Holder"))
            {
                return null;
                //Todo add productsDto = productsDto.Where(a => a.MarketingAuthorizHolderId == _currentUser.MarketingAuthorizHolder.Id).ToList();
            }

            var allProducts = Mapper.Map<IEnumerable<ProductDTO>, List<ProductViewModel>>(productsDto).ToArray();
            var allProcedures = new List<ProcedureViewModel>();

            foreach (var product in allProducts)
            {
                var proc = _procedureService.GetItems().Where(a => a.ProductId == product.Id);
                var procedureDtos = proc as ProcedureDTO[] ?? proc.ToArray();
                if(!procedureDtos.Any()) continue;
                var procedures = Mapper.Map<IEnumerable<ProcedureDTO>, IEnumerable<ProcedureViewModel>>(procedureDtos).ToList();
                allProcedures.AddRange(procedures);
            }
            return allProcedures;
        }

        /*----------------------------------------------------------------------------*/

        private IUserService UserService => HttpContext.GetOwinContext().GetUserManager<IUserService>();

        [HttpGet]
        public ActionResult ProductProcedures(int id)
        {
            //Todo delete this check
            if (User.IsInRole("Holder"))
            {
                return null;
            }

            if (id == 0)
            {
                @ViewBag.Error = Resources.ErrorMessages.NoIdInRequest;
                return View("Error");
            }
            _currentUser = GetCurrentUser();
            var productDto = _productService.GetProduct(id);
           
            var product = Mapper.Map<ProductDTO, ProductViewModel>(productDto);

            ViewBag.Country = product.Country;
            ViewBag.CountryId = productDto.CountryId;

            if (_currentUser.Countries.All(a => a.Id != productDto.CountryId) && !User.IsInRole("Admin") && !User.IsInRole("Holder"))
            {
                @ViewBag.Error = Resources.ErrorMessages.NoPermission;
                return View("Error");
            }
            if (User.IsInRole("Holder") && !product.MarketingAuthorizHolder.Equals(_currentUser.MarketingAuthorizHolder.Name))
            {
                @ViewBag.Error = Resources.ErrorMessages.NoPermission;
                return View("Error");
            }

            ViewBag.Product = product;
            ViewBag.User = _currentUser;
            ViewBag.DocsType = Enum.GetValues(typeof(ProcedureDocsType));
            var proceduresListDto = _procedureService.GetItems().Where(a => a.ProductId == id);
            var procedureDto = Mapper.Map<IEnumerable<ProcedureDTO>, IList<ProcedureViewModel>>(proceduresListDto);
            return View(procedureDto);
        }

        [HttpGet]
        public ActionResult CreateProcedure(int id)
        {
            if (id == 0)
            {
                @ViewBag.Error = "No productId in request!";
                return View("Error");
            }
            try
            {
                var model = new ProcedureViewModel();
                var productDto = _productService.GetProduct(id);
                var product = Mapper.Map<ProductDTO, ProductViewModel>(productDto);
                var _currentUser = GetCurrentUser();
                model.Product = product;
                model.ProductId = id;

                ViewBag.CountryId = productDto.CountryId;
                ViewBag.Product = product;

                ViewBag.Country = product.Country;
                ViewBag.Product = product;
                ViewBag.User = _currentUser;
                return View(model);
            }
            catch (Exception e)
            {
                @ViewBag.Error = e.Message;
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateProcedure(ProcedureViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Error = CreateError();
                return View("Error");
            }
            try
            {
                var procedureDto = Mapper.Map<ProcedureViewModel, ProcedureDTO>(model);
                _procedureService.AddItem(procedureDto);
                _procedureService.Commit();

                TempData["Success"] = Resources.Messages.ProcedureCreatedSuccess;
                await SenEmailAboutAddProcedure(model);
                _currentUser = GetCurrentUser();
                Logger.Log.Info($"{_currentUser.Email} Added Procedure for Product #{procedureDto.ProductId} {procedureDto.ProcedureType}");
                return RedirectToAction("ProductProcedures", new { id = model.ProductId });
            }
            catch (Exception ex)
            {
                @ViewBag.Error = ex.ToString();
                return View("Error");
            }
        }

        // GET: Procedure/Delete/5
        public ActionResult DeleteProcedure(int id, int productId)
        {
            if (id == 0 || productId==0)
            {
                @ViewBag.Error = "Error happened in DeleteProcedure method: no Id in GET request.";
                return View("Error");
            }
            try
            {
                var procedureDocs = _procedureService.GetItem(id).ProcedureDocuments.ToList();
                var targetFolder = Server.MapPath($"~/Upload/Documents/Procedures/");

                foreach (var doc in procedureDocs)
                {
                    DeleteFile(doc.PathToDocument, targetFolder);
                }
                _procedureService.DeleteItem(id);
                _procedureService.Commit();
                _currentUser = GetCurrentUser();
                TempData["Success"] = Resources.Messages.ProcedureDeletedSuccess;
                Logger.Log.Info($"{_currentUser.Email} Deleted Procedure #{id} for Product #{productId} ");

                return RedirectToAction("ProductProcedures", new { id = productId });
            }
            catch (Exception ex)
            {
                @ViewBag.Error = ex.Message;
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult EditProcedure(int id, int? productId)
        {
            if (id==0 || productId == 0)
            {
                @ViewBag.Error = nameof(id);
                return View("Error");
            }
            try
            {
                var procedure = _procedureService.GetItem(id);
                var procedureDto = Mapper.Map<ProcedureDTO, ProcedureEditModel>(procedure);

                var productDto = _productService.GetProduct((int)productId);
                var product = Mapper.Map<ProductDTO, ProductViewModel>(productDto);
                _currentUser = GetCurrentUser();
                //procedureDto.Product = product;
                procedureDto.ProductId = id;

                ViewBag.CountryId = productDto.CountryId;
                ViewBag.Country = product.Country;
                ViewBag.Product = product;
                ViewBag.User = _currentUser;
                ViewBag.DocsType = Enum.GetValues(typeof(ProcedureDocsType));

                return View(procedureDto);
            }
            catch (Exception e)
            {
                @ViewBag.Error = e.Message;
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<ActionResult> EditProcedure(ProcedureEditModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Error = CreateError();
                return View("Error");
            }
            try
            {
                var procedureDto = Mapper.Map<ProcedureEditModel, ProcedureDTO>(model);
                await SenEmailAboutUpdateProcedure(model);
                _procedureService.Update(procedureDto);
                _procedureService.Commit();
                _currentUser = GetCurrentUser();
                TempData["Success"] = Resources.Messages.ProcedureUpdatedSuccess;
                Logger.Log.Info($"{_currentUser.Email} Edited Procedure #{model.Id} for Product #{model.ProductId} ");

                return RedirectToAction("ProductProcedures", new { id = model.ProductId });
            }
            catch (Exception ex)
            {
                @ViewBag.Error = ex.ToString();
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult EditProcedureFiles(int id, int? productId)
        {
            if (id == 0 || productId == 0)
            {
                @ViewBag.Error = nameof(id);
                return View("Error");
            }
            try
            {
                var procedure = _procedureService.GetItem(id);
                var procedureDto = Mapper.Map<ProcedureDTO, ProcedureViewModel>(procedure);

                var productDto = _productService.GetProduct((int)productId);
                var product = Mapper.Map<ProductDTO, ProductViewModel>(productDto);
                //var _currentUser = GetCurrentUser();

                procedureDto.ProductId = (int)productId;
                ViewBag.Country = product.Country;
                ViewBag.CountryId = productDto.CountryId;
                ViewBag.Product = product;
                ViewBag.Product = product;
                ViewBag.User = _currentUser;
                ViewBag.DocsType = Enum.GetValues(typeof(ProcedureDocsType));
                return View(procedureDto);
            }
            catch (Exception ex)
            {
                @ViewBag.Error = ex.ToString();
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult EditFiles(int id, int? productId, ProcedureDocsType procedureDocsType)
        {
            if (id == 0 || productId == 0)
            {
                @ViewBag.Error = nameof(id);
                return View("Error");
            }
            try
            {
                var procedure = _procedureService.GetItem(id);
                var procedureDto = Mapper.Map<ProcedureDTO, ProcedureViewModel>(procedure);

                var productDto = _productService.GetProduct((int)productId);
                var product = Mapper.Map<ProductDTO, ProductViewModel>(productDto);
                _currentUser = GetCurrentUser();

                procedureDto.ProductId = (int)productId;
                ViewBag.ProcedureDocsType = procedureDocsType;
                ViewBag.CountryId = productDto.CountryId;
                ViewBag.Country = product.Country;
                ViewBag.Product = product;
                ViewBag.User = _currentUser;
                ViewBag.DocsType = Enum.GetValues(typeof(ProcedureDocsType));
                return View(procedureDto);
            }
            catch (Exception ex)
            {
                @ViewBag.Error = ex.ToString();
                return View("Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task EditProcedureFiles(IEnumerable<HttpPostedFileBase> uploads, string procedureDocsType, string procedureId, string productId)
        {
            if (!ModelState.IsValid)
            {
                CreateError();
            }
            try
            {
                if (uploads == null) return;

                foreach (var file in uploads)
                {
                    if (file == null || file.ContentLength <= 0) continue;
                    var targetFolder = Server.MapPath($"~/Upload/Documents/Procedures/");

                    if (!SaveHttpPostedFile(file, ref targetFolder, out string targetPath, out string localFileName)) continue;

                    var fileExt = Path.GetExtension(localFileName);

                    var procId = int.Parse(procedureId);
                    var procDocType = int.Parse(procedureDocsType);

                    if (fileExt.Equals(".zip"))
                    {
                        var filesFromArchive = _archProccessor.ProcessArchive(targetPath, targetFolder);
                        foreach (var fileFromArchive in filesFromArchive)
                        {
                            AddFileToProc(fileFromArchive, procId, procDocType);
                        }
                        if (filesFromArchive.Count == 0)
                        {
                            Logger.Log.Error($"Archive {localFileName} wasn't processed! Count of extracted files == 0 ");
                        }
                        continue;
                    }
                    AddFileToProc(localFileName, procId, procDocType);
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error($"{ex}");
            }
        }

        public bool SaveHttpPostedFile(HttpPostedFileBase file, ref string targetFolder, out string targetPath, out string localFileName)
        {
            var fileTrimmName = file.FileName.Replace(",", "_");
            try
            {
                var fileExt = Path.GetExtension(fileTrimmName);
                targetFolder = fileExt.Equals(".zip") ? Server.MapPath($"~/Upload/Documents/Procedures/Archives/") : targetFolder;

                localFileName = $"{Path.GetFileNameWithoutExtension(fileTrimmName)}_{Guid.NewGuid().ToString().Substring(0, 6)}{fileExt}";
                targetPath = Path.Combine(targetFolder, localFileName);
                file.SaveAs(targetPath);
                _currentUser = GetCurrentUser();
                Logger.Log.Info($"{_currentUser.Email} Downloaded file {localFileName}");
                return true;
            }
            catch (Exception ex)
            {
                Logger.Log.Error($"Cannot save file {fileTrimmName} in {targetFolder}: {ex}");
                targetPath = localFileName = String.Empty;
                return false;
            }
        }

        public void AddFileToProc(string localFileName, int procedureId, int procDocType)
        {
            try
            {
                var doc = new ProcedureDocument()
                {
                    PathToDocument = localFileName,
                    ProcedureId = procedureId,
                    ProcedureDocsType = (ProcedureDocsType) procDocType
                };
                //await SendEmailAboutAddFileProcedure(procedureId, productId, (ProcedureDocsType)procDocType, localFileName);
                var procedure = _procedureService.GetItem(procedureId);
                procedure.ProcedureDocuments.Add(doc);
                _procedureService.Update(procedure);
                _currentUser = GetCurrentUser();
                Logger.Log.Info($"{_currentUser.Email} Added file {localFileName}");
            }
            catch (Exception ex)
            {
                Logger.Log.Error($"Cannot add file {localFileName} to Procedure: {ex}");
            }
        }

        [HttpPost]
        public void DeleteProcedureFile(string documentId, int procedureId)
        {
            if (documentId == null || procedureId == 0 ) throw new ArgumentNullException();
            var targetFolder = Server.MapPath($"~/Upload/Documents/Procedures/");
            var procedure = _procedureService.GetItem(procedureId);
            var documentID = int.Parse(documentId);
            var document = procedure.ProcedureDocuments.FirstOrDefault(a => a.Id == documentID);
            var deleteRes = DeleteFile(document.PathToDocument, targetFolder);
            if (deleteRes)
            {
                //procedure.ProcedureDocuments.Remove(document);
                //_procedureService.Update(procedure);
                _procedureService.DeleteDocument(document.PathToDocument);
            }
        }
        
        /*----------------------------------------------------------------------------*/
        public string CreateError()
        {
            var errorMessage = new StringBuilder();
            foreach (ModelState modelState in ViewData.ModelState.Values)
            {
                foreach (ModelError error in modelState.Errors)
                {
                    errorMessage.Append(error.ErrorMessage);
                }
            }
            Logger.Log.Error($"{errorMessage}");
            return errorMessage.ToString();
        }

        public bool DeleteFile(string fileName, string targetFolder)
        {
            try
            {
                var targetPath = String.Concat(targetFolder, fileName.Replace(@"/", @"\"));
                if (System.IO.File.Exists(targetPath))
                {
                    System.IO.File.Delete($"{targetPath}");
                }
                _currentUser = GetCurrentUser();
                Logger.Log.Info($"{_currentUser.Email} Deleted file {fileName}");
                return true;
            }
            catch (Exception e)
            {
                Logger.Log.Error(e.Message);
                return false;
            }
        }

        public async Task SenEmailAboutAddProcedure(ProcedureViewModel model)
        {
            try
            {
                var prod = _productService.GetProduct((int)model.ProductId);
                if (prod == null)
                {
                    Logger.Log.Error($"{Resources.ErrorMessages.EmailNotSendCantFindProdToProc} ProcedureId={model.Id}");
                    return;
                }

                var product = Mapper.Map<ProductDTO, ShowProductModel>(prod);
                var productName = product.ProductName;
                var userEmailsToNotify = _countryService.GetCountryUsersEmailsViaName(product.Country);

                var subject = Resources.Email.SubjectProcedureCreate.Replace("(name)", productName) + $" {model.ProcedureType}" + $" in {product.Country}";
                var body = $"{Resources.Email.BodyProcedureCreate} {model.ProcedureType} for <b>{productName}</b> in {product.Country}<br><br>" +
                           $"<b>Pharmaceutical Form:</b> {product.PharmaceuticalForm}<br><br>" +
                           $"<b>Strength:</b> {product.Strength}<br><br><hr>" +
                           $"<b>Name:</b> {model.Name}<br><br>" +
                           $"<b>SubmissionDate:</b> {model.SubmissionDate}<br><br>" +
                           $"<b>EstimatedApprovalDate:</b> {model.EstimatedApprovalDate}<br><br>" +
                           $"<b>ApprovalDate:</b> {model.ApprovalDate}<br><br>" +
                           $"<b>Comments:</b> {model.Comments}<br><br>" +
                           $"{Resources.Email.Signature}";
                var emailerDto = Mapper.Map<Emailer,EmailerDTO>(emailer);
                if (model.SubmissionDate >= DateTime.Parse("2018-11-11 00:00:00"))
                {
                    await _emailService.SendEmailNotification(body, subject, emailerDto, userEmailsToNotify, toSend);
                }
            }
            catch (Exception ex)
            {
                var userName = User.Identity.Name;
                Logger.Log.Error($"{userName}: SenEmailAboutAddUpdateProduct() {ex.Message} ");
            }
        }
        
       
        public async Task SenEmailAboutUpdateProcedure(ProcedureEditModel model)
        {
            try
            {
                var prod = _productService.GetProduct((int)model.ProductId);
                if (prod == null)
                {
                    Logger.Log.Error($"{Resources.ErrorMessages.EmailNotSendCantFindProdToProc} ProcedureId={model.Id}");
                    return;
                }

                var product = Mapper.Map<ProductDTO, ShowProductModel>(prod);
                var productName = product.ProductName;
                var userEmailsToNotify = _countryService.GetCountryUsersEmailsViaName(product.Country);

                var body = new StringBuilder();
                
                    var subject = Resources.Email.SubjectProcedureUpdate.Replace("(name)", productName) + $" in {product.Country}";
                    body.Append(Resources.Email.BodyProcedureUpdate.Replace("(name)", productName) + $" in {product.Country}");
                    body.Append($" {model.ProcedureType}");
                    var bodyCompared = await CreateBodyText(model);
                if (!string.IsNullOrEmpty(bodyCompared))
                {
                    body.Append(":<br>");
                    body.Append(bodyCompared);
                    body.Append(Resources.Email.Signature);
                    var emailerDto = Mapper.Map<Emailer, EmailerDTO>(emailer);
                    if (model.SubmissionDate >= DateTime.Parse("2018-11-11 00:00:00"))
                    {
                        await _emailService.SendEmailNotification(body.ToString(), subject, emailerDto, userEmailsToNotify, toSend);
                    }
                }
            }
            catch (Exception ex)
            {
                var userName = User.Identity.Name;
                Logger.Log.Error($"{userName}: SenEmailAboutAddUpdateProduct() {ex.Message} ");
            }
        }

        public async Task<string> CreateBodyText(ProcedureEditModel model)
        {
            var bodyStr = new StringBuilder();

            var oldProcedure = _procedureService.GetItem(model.Id);

            if (oldProcedure.ApprovalDate != model.ApprovalDate)
            {
                bodyStr.Append($"<strong>{Resources.Labels.ApprovalDate}</strong> changed to {model.ApprovalDate.ToString().Substring(0, 10)}<br>");
            }
            if (oldProcedure.EstimatedApprovalDate != model.ApprovalDate)
            {
                bodyStr.Append($"<strong>{Resources.Labels.EstimatedApprovalDate}</strong> changed to {model.EstimatedApprovalDate.ToString(CultureInfo.InvariantCulture).Substring(0, 10)}<br>");
            }
            if (oldProcedure.SubmissionDate != model.SubmissionDate )
            {
                bodyStr.Append($"<strong>{Resources.Labels.SubmissionDate}</strong> changed to {model.SubmissionDate.ToString(CultureInfo.InvariantCulture).Substring(0, 10)}<br>");
            }

            if (oldProcedure.Comments != model.Comments)
            {
                bodyStr.Append($"<strong>Comments</strong> changed to {model.Comments}<br>");
            }

            if (model.ProcedureDocuments != null && model.ProcedureDocuments.Count > 0)
            {
                foreach (var doc in model.ProcedureDocuments)
                {
                    var newDocument = new ProcedureDocument() { PathToDocument = doc.PathToDocument, ProcedureId = doc.ProcedureId, ProcedureDocsType = doc.ProcedureDocsType};
                    var res = oldProcedure.ProcedureDocuments.FirstOrDefault(a => a.PathToDocument == doc.PathToDocument && a.ProcedureId == doc.ProcedureId && a.ProcedureDocsType == doc.ProcedureDocsType);
                    var res2 = oldProcedure.ProcedureDocuments.Contains(newDocument);
                    if (res == null && !res2)
                    {
                        bodyStr.Append($"<strong>To {doc.ProcedureDocsType} added document:</strong> {doc.PathToDocument}<br>");
                    }
                }
            }
            return bodyStr.ToString();
        }

        public async Task SendEmailAboutAddFileProcedure(string procedureId, string productId, ProcedureDocsType procedureDocsType, string localFileName)
        {
            try
            {
                var prod = _productService.GetProduct(int.Parse(productId));
                if (prod == null)
                {
                    Logger.Log.Error($"{Resources.ErrorMessages.EmailNotSendCantFindProdToProc} ProcedureId={procedureId}");
                    return;
                }

                var product = Mapper.Map<ProductDTO, ShowProductModel>(prod);
                var productName = product.ProductName;
                var userEmailsToNotify = _countryService.GetCountryUsersEmailsViaName(product.Country);

                var body = new StringBuilder();

                var subject = Resources.Email.SubjectProcedureUpdate.Replace("(name)", productName) + $" in {product.Country}";
                body.Append(Resources.Email.BodyProcedureUpdate.Replace("(name)", productName) + $" in {product.Country}");
                var bodyCompared = $"<strong>To procedure {procedureDocsType} added document:</strong> {localFileName}<br>";
                if (!string.IsNullOrEmpty(bodyCompared))
                {
                    body.Append(":<br>");
                    body.Append(bodyCompared);
                    body.Append(Resources.Email.Signature);
                    var emailerDto = Mapper.Map<Emailer, EmailerDTO>(emailer);
                    var proc = _procedureService.GetItem(int.Parse(procedureId));
                    if (proc.SubmissionDate >= DateTime.Parse("2018-11-11 00:00:00"))
                    {
                        await _emailService.SendEmailNotification(body.ToString(), subject, emailerDto,
                            userEmailsToNotify, toSend);
                    }
                }
            }
            catch (Exception ex)
            {
                var userName = User.Identity.Name;
                Logger.Log.Error($"{userName}: SenEmailAboutAddUpdateProduct() {ex.Message} ");
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
