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
using Olga.AutoMapper;
using Olga.BLL.DTO;
using Olga.BLL.Interfaces;
using Olga.BLL.Services;
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

                procedureDto.ProductId = id;
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
        public ActionResult EditProcedureFiles(IEnumerable<HttpPostedFileBase> uploads, string procedureDocsType, string procedureId, string productId)
        {
            if (!ModelState.IsValid)
            {
                CreateError();
                return View("Error");
            }
            try
            {
                var targetFolder = Server.MapPath($"~/Upload/Documents/Procedures/");

                foreach (var file in uploads)
                {
                    if (file == null) continue;
                    var fileTrimmName = file.FileName.Replace(",", "_");
                    var localFileName =
                        $"{Path.GetFileNameWithoutExtension(fileTrimmName)}_{Guid.NewGuid().ToString().Substring(0, 6)}{Path.GetExtension(fileTrimmName)}";
                    var targetPath = Path.Combine(targetFolder, localFileName);
                    file.SaveAs(targetPath);
                }

                TempData["Success"] = Resources.Messages.FilesDownoadSuccess;
                return RedirectToAction("EditProcedureFiles", new { id = 0 });
            }
            catch (Exception ex)
            {
                @ViewBag.Error = ex.ToString();
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult SaveUploadedFile()
        {
            return Json(new { Message = "Page not ready yet" });
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
            @ViewBag.Error = errorMessage.ToString();
        }
    }
}
