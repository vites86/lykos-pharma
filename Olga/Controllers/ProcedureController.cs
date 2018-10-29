using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
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
            List<SelectListItem> products = 
                allProducts.Select(n => new SelectListItem { Text = n.ProductName?.Name ?? "No name", Value = n.Id.ToString() }).ToList();
            @ViewBag.Products = products;
            return true;
        }
        public List<ProcedureViewModel> GetProcedures(int countryId)
        {
            var productsDto = _productService.GetProducts(countryId);
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
            if (id == 0)
            {
                @ViewBag.Error = "Error happened: No productId in request!";
                return View("Error");
            }
            var productDto = _productService.GetProduct(id);
            var product = Mapper.Map<ProductDTO, ProductViewModel>(productDto);
            var _currentUser = GetCurrentUser();

            ViewBag.Country = product.Country;
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
        public ActionResult CreateProcedure(ProcedureViewModel model)
        {
            if (!ModelState.IsValid)
            {
                CreateError();
                return View("Error");
            }
            try
            {
                var procedureDto = Mapper.Map<ProcedureViewModel, ProcedureDTO>(model);
                _procedureService.AddItem(procedureDto);
                _procedureService.Commit();
                TempData["Success"] = Resources.Messages.ProcedureCreatedSuccess;
                return RedirectToAction("ProductProcedures", new { id = model.ProductId });
            }
            catch (Exception ex)
            {
                @ViewBag.Error = ex.ToString();
                return View("Error");
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

                TempData["Success"] = Resources.Messages.ProcedureDeletedSuccess;
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
                var _currentUser = GetCurrentUser();
                //procedureDto.Product = product;
                procedureDto.ProductId = id;

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
        public ActionResult EditProcedure(ProcedureEditModel model)
        {
            if (!ModelState.IsValid)
            {
                CreateError();
                return View("Error");
            }
            try
            {
                var procedureDto = Mapper.Map<ProcedureEditModel, ProcedureDTO>(model);
                _procedureService.Update(procedureDto);
                _procedureService.Commit();
                TempData["Success"] = Resources.Messages.ProcedureUpdatedSuccess;
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
                var _currentUser = GetCurrentUser();

                procedureDto.ProductId = (int)productId;
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
                var _currentUser = GetCurrentUser();

                procedureDto.ProductId = (int)productId;
                ViewBag.ProcedureDocsType = procedureDocsType;
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
        public void EditProcedureFiles(IEnumerable<HttpPostedFileBase> uploads, string procedureDocsType, string procedureId, string productId)
        {
            if (!ModelState.IsValid)
            {
                CreateError();
            }
            try
            {
                if (uploads == null) return;
                var targetFolder = Server.MapPath($"~/Upload/Documents/Procedures/");

                foreach (var file in uploads)
                {
                    if (file == null || file.ContentLength <= 0) continue;
                    var fileTrimmName = file.FileName.Replace(",", "_");
                    var localFileName =
                        $"{Path.GetFileNameWithoutExtension(fileTrimmName)}_{Guid.NewGuid().ToString().Substring(0, 6)}{Path.GetExtension(fileTrimmName)}";
                    var targetPath = Path.Combine(targetFolder, localFileName);
                    file.SaveAs(targetPath);
                    
                    var procId = int.Parse(procedureId);
                    var procDocType = int.Parse(procedureDocsType);
                    var doc = new ProcedureDocument()
                    {
                        PathToDocument = localFileName,
                        ProcedureId = procId,
                        ProcedureDocsType = (ProcedureDocsType)procDocType
                    };
                    var procedure = _procedureService.GetItem(procId);
                    procedure.ProcedureDocuments.Add(doc);
                    _procedureService.Update(procedure);
                }

            }
            catch (Exception ex)
            {
                Logger.Log.Error($"{ex}");
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
                procedure.ProcedureDocuments.Remove(document);
                _procedureService.Update(procedure);
            }

        }

        
        /*----------------------------------------------------------------------------*/
        public void CreateError()
        {
            var errorMessage = new StringBuilder();
            foreach (ModelState modelState in ViewData.ModelState.Values)
            {
                foreach (ModelError error in modelState.Errors)
                {
                    errorMessage.Append(error.Exception.Message);
                }
            }
            Logger.Log.Error($"{errorMessage}");
        }

        public bool DeleteFile(string fileName, string targetFolder)
        {
            try
            {
                var targetPath = Path.Combine(targetFolder, fileName);
                if (System.IO.File.Exists(targetPath))
                {
                    System.IO.File.Delete($"{targetPath}");
                }
                return true;
            }
            catch (Exception e)
            {
                Logger.Log.Error(e.Message);
                return false;
            }
            

        }
    }
}
