using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Olga.BLL.DTO;
using Olga.BLL.Interfaces;
using Olga.DAL.Entities;
using Olga.Models;
using Olga.Util;

namespace Olga.Controllers
{
    [Authorize]
    public class SettingsController : Controller
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
        IBase<CountrySettingDTO> _countrySettingsService;


        // GET: Settings
        public SettingsController(ICountry serv, IProductName prodName, IProductCode prodCode, IMarketingAuthorizNumber marketingAuthorizNumber, IPackSize packSize,
            IApprDocsType apprDocsType, IStrength strength, IManufacturer manufacturer, IArtwork artwork, IMarketingAuthorizHolder marketingAuthorizHolder, 
            IPharmaceuticalForm pharmaceuticalForm, IBase<CountrySettingDTO> countrySettingsService)
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
            _countrySettingsService = countrySettingsService;
        }

        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                @ViewBag.Error = "Error happened in Settings Index method: no country id in GET request.";
                return View("Error");
            }

            var country = Mapper.Map<CountryDTO, CountryViewModel>(_countryService.GetItem((int)id));

            var prodNamesDto = _productNameService.GetItems((int)id).OrderBy(a=>a.Name);
            var prodNames = Mapper.Map<IEnumerable<ProductNameDTO>, IEnumerable<ProductNameViewModel>>(prodNamesDto);
            @ViewBag.ProductNames = prodNames;

            var prodCodesDto = _productCodeService.GetItems((int)id).OrderBy(a => a.Code);
            var prodCodes = Mapper.Map<IEnumerable<ProductCodeDTO>, IEnumerable<ProductCodeViewModel>>(prodCodesDto);
            @ViewBag.ProductCodes = prodCodes;

            var markAuthNumsDto = _marketingAuthorizNumberService.GetItems((int)id).OrderBy(a => a.Number);
            var markAuthNums = Mapper.Map<IEnumerable<MarketingAuthorizNumberDTO>, IEnumerable<MarketingAuthorizNumberViewModel>>(markAuthNumsDto);
            @ViewBag.MarketingAuthorizNumbers = markAuthNums;

            var packSizesDto = _packSizeService.GetItems((int)id).OrderBy(a => a.Size);
            var packSizes = Mapper.Map<IEnumerable<PackSizeDTO>, IEnumerable<PackSizeViewModel>>(packSizesDto);
            @ViewBag.PackSizes = packSizes;

            return View(country);
        }

        [HttpPost]
        public ActionResult AddProductName(string CountryId, string Name)
        {
            if (string.IsNullOrEmpty(CountryId))
            {
                @ViewBag.Error = "Error happened in Settings AddProductName method: no countryId/countryName in GET request.";
                return View("Error");
            }
            try
            {
                _productNameService.AddItem(new ProductNameDTO() { Name = Name, CountryId = Int32.Parse(CountryId) });
                _productNameService.Commit();

                var userName = User.Identity.Name;
                Logger.Log.Info($"{userName}: AddProductName() {Name} ");

                var prodNamesDto = _productNameService.GetItems(int.Parse(CountryId));
                var prodNames = Mapper.Map<IEnumerable< ProductNameDTO>, IEnumerable<ProductNameViewModel>>(prodNamesDto);
                @ViewBag.ProductNames = prodNames;
                return PartialView("ProductNames");
            }
            catch (Exception ex)
            {
                var userName = User.Identity.Name;
                Logger.Log.Error($"{userName}: AddProductName() {ex.Message} ");
               
                @ViewBag.Error = ex.Message;
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult DeleteProductName(string Id, string CountryId)
        {
            if (string.IsNullOrEmpty(CountryId) || string.IsNullOrEmpty(Id))
            {
                @ViewBag.Error = "Error happened in Settings DeleteProductName method: no countryId/ProductNameId in GET request.";
                return View("Error");
            }
            try
            {
                _productNameService.DeleteItem(int.Parse(Id));
                _productNameService.Commit();

                var prodNamesDto = _productNameService.GetItems(int.Parse(CountryId));
                var prodNames = Mapper.Map<IEnumerable<ProductNameDTO>, IEnumerable<ProductNameViewModel>>(prodNamesDto);
                @ViewBag.ProductNames = prodNames;

                var userName = User.Identity.Name;
                Logger.Log.Info($"{userName}: DeleteProductName() {Id} ");

                return PartialView("ProductNames");
            }
            catch (Exception ex)
            {
                var userName = User.Identity.Name;
                Logger.Log.Error($"{userName}: DeleteProductName() {Id} ");
                @ViewBag.Error = ex.Message;
                return RedirectToAction("Error", "Home", new { message = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult EditProductName(string Name, string Id)
        {
            try
            {
                return PartialView("ProductNames");
            }
            catch
            {
                return PartialView("ProductNames");
            }
        }

        [HttpPost]
        public ActionResult AddProductCode(string CountryId, string Code)
        {
            if (string.IsNullOrEmpty(CountryId))
            {
                @ViewBag.Error = "Error happened in Settings AddProductCode method: no countryId/countryName in GET request.";
                return View("Error");
            }
            try
            {
                _productCodeService.AddItem(new ProductCodeDTO() { Code = Code, CountryId = Int32.Parse(CountryId) });
                _productCodeService.Commit();

                var prodCodesDto = _productCodeService.GetItems(int.Parse(CountryId));
                var prodCodes = Mapper.Map<IEnumerable<ProductCodeDTO>, IEnumerable<ProductCodeViewModel>>(prodCodesDto);
                @ViewBag.ProductCodes = prodCodes;

                var userName = User.Identity.Name;
                Logger.Log.Info($"{userName}: AddProductCode() {Code} ");

                return PartialView("ProductCodes");
            }
            catch (Exception ex)
            {
                var userName = User.Identity.Name;
                Logger.Log.Error($"{userName}: AddProductCode() {Code} ");

                @ViewBag.Error = ex.Message;
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult DeleteProductCode(string Id, string CountryId)
        {
            if (string.IsNullOrEmpty(CountryId) || string.IsNullOrEmpty(Id))
            {
                @ViewBag.Error = "Error happened in Settings DeleteProductCode method: no countryId/ProductNameId in GET request.";
                return View("Error");
            }
            try
            {
                _productCodeService.DeleteItem(int.Parse(Id));
                _productCodeService.Commit();

                var prodCodesDto = _productCodeService.GetItems(int.Parse(CountryId));
                var prodCodes = Mapper.Map<IEnumerable<ProductCodeDTO>, IEnumerable<ProductCodeViewModel>>(prodCodesDto);
                @ViewBag.ProductCodes = prodCodes;

                var userName = User.Identity.Name;
                Logger.Log.Info($"{userName}: DeleteProductCode() {Id} ");

                return PartialView("ProductCodes");
            }
            catch (Exception ex)
            {
                var userName = User.Identity.Name;
                Logger.Log.Error($"{userName}: DeleteProductCode() {Id} ");

                TempData["Error"] = ex.Message;
                return RedirectToAction("Error", "Home", new { message = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult AddMarketingAuthorizNumber(string CountryId, string Number)
        {
            if (string.IsNullOrEmpty(CountryId))
            {
                @ViewBag.Error = "Error happened in Settings AddMarketingAuthorizNumber method: no countryId/countryName in GET request.";
                return View("Error");
            }
            try
            {
                _marketingAuthorizNumberService.AddItem(new MarketingAuthorizNumberDTO() { Number = Number, CountryId = Int32.Parse(CountryId) });
                _marketingAuthorizNumberService.Commit();

                var markAuthNumsDto = _marketingAuthorizNumberService.GetItems(int.Parse(CountryId));
                var markAuthNums = Mapper.Map<IEnumerable<MarketingAuthorizNumberDTO>, IEnumerable<MarketingAuthorizNumberViewModel>>(markAuthNumsDto);
                @ViewBag.MarketingAuthorizNumbers = markAuthNums;

                var userName = User.Identity.Name;
                Logger.Log.Info($"{userName}: AddMarketingAuthorizNumber() {Number} ");

                return PartialView("MarketingAuthorizNumbers");
            }
            catch (Exception ex)
            {
                var userName = User.Identity.Name;
                Logger.Log.Error($"{userName}: AddMarketingAuthorizNumber() {Number} ");

                @ViewBag.Error = ex.Message;
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult DeleteMarketingAuthorizNumber(string Id, string CountryId)
        {
            if (string.IsNullOrEmpty(CountryId) || string.IsNullOrEmpty(Id))
            {
                @ViewBag.Error = "Error happened in Settings DeleteProductName method: no countryId/ProductNameId in GET request.";
                return View("Error");
            }
            try
            {
                _marketingAuthorizNumberService.DeleteItem(int.Parse(Id));
                _marketingAuthorizNumberService.Commit();

                var markAuthNumsDto = _marketingAuthorizNumberService.GetItems(int.Parse(CountryId));
                var markAuthNums = Mapper.Map<IEnumerable<MarketingAuthorizNumberDTO>, IEnumerable<MarketingAuthorizNumberViewModel>>(markAuthNumsDto);
                @ViewBag.MarketingAuthorizNumbers = markAuthNums;

                var userName = User.Identity.Name;
                Logger.Log.Info($"{userName}: DeleteMarketingAuthorizNumber() {Id} ");

                return PartialView("MarketingAuthorizNumbers");
            }
            catch (Exception ex)
            {
                var userName = User.Identity.Name;
                Logger.Log.Error($"{userName}: DeleteMarketingAuthorizNumber() {Id} ");

                @ViewBag.Error = ex.Message;
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult AddPackSize(string CountryId, string Size)
        {
            if (string.IsNullOrEmpty(CountryId))
            {
                @ViewBag.Error = "Error happened in Settings AddProductName method: no countryId/countryName in GET request.";
                return View("Error");
            }
            try
            {
                _packSizeService.AddItem(new PackSizeDTO() { Size = Size, CountryId = Int32.Parse(CountryId) });
                _packSizeService.Commit();

                var packSizesDto = _packSizeService.GetItems(int.Parse(CountryId));
                var packSizes = Mapper.Map<IEnumerable<PackSizeDTO>, IEnumerable<PackSizeViewModel>>(packSizesDto);
                @ViewBag.PackSizes = packSizes;

                var userName = User.Identity.Name;
                Logger.Log.Info($"{userName}: AddPackSize() {Size} ");

                return PartialView("PackSizes");
            }
            catch (Exception ex)
            {
                var userName = User.Identity.Name;
                Logger.Log.Error($"{userName}: AddPackSize() {Size} ");

                @ViewBag.Error = ex.Message;
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult DeletePackSize(string Id, string CountryId)
        {
            if (string.IsNullOrEmpty(CountryId) || string.IsNullOrEmpty(Id))
            {
                @ViewBag.Error = "Error happened in Settings DeletePackSize method: no countryId/ProductNameId in GET request.";
                return View("Error");
            }
            try
            {
                _packSizeService.DeleteItem(int.Parse(Id));
                _packSizeService.Commit();

                var packSizesDto = _packSizeService.GetItems(int.Parse(CountryId));
                var packSizes = Mapper.Map<IEnumerable<PackSizeDTO>, IEnumerable<PackSizeViewModel>>(packSizesDto);
                @ViewBag.PackSizes = packSizes;

                var userName = User.Identity.Name;
                Logger.Log.Info($"{userName}: DeletePackSize() {Id} ");

                return PartialView("PackSizes");
            }
            catch (Exception ex)
            {
                var userName = User.Identity.Name;
                Logger.Log.Error($"{userName}: DeletePackSize() {Id} ");

                @ViewBag.Error = ex.Message;
                return RedirectToAction("Error", "Home", new { message = ex.Message });
            }
        }

        public ActionResult Settings(int? id)
        {
            return View();
        }


        [HttpPost]
        public ActionResult ShowApprDocsType()
        {
            try
            {
                var apprDocsTypesDto = _apprDocsTypeService.GetItems();
                var apprDocsTypes = Mapper.Map<IEnumerable<ApprDocsTypeDTO>, IEnumerable<ApprDocsTypeViewModel>>(apprDocsTypesDto).ToList();
                @ViewBag.apprDocsTypes = apprDocsTypes;
                return PartialView("ApprDocsType", apprDocsTypes);
            }
            catch (Exception ex)
            {
                @ViewBag.Error = ex.Message;
                return View("Error");
            }
        }
        
        [HttpPost]
        public ActionResult ShowStrength()
        {
            try
            {
                var strengthDto = _strengthService.GetItems();
                var strength = Mapper.Map<IEnumerable<StrengthDTO>, IEnumerable<StrengthViewModel>>(strengthDto).ToList();
                return PartialView("Strength", strength);
            }
            catch (Exception ex)
            {
                @ViewBag.Error = ex.Message;
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult ShowManufacturer()
        {
            try
            {
                var manufacturerDto = _manufacturerService.GetItems().OrderBy(a=>a.Name);
                var manufacturer = Mapper.Map<IEnumerable<ManufacturerDTO>, IEnumerable<ManufacturerViewModel>>(manufacturerDto).ToList();
                return PartialView("Manufacturer", manufacturer);
            }
            catch (Exception ex)
            {
                @ViewBag.Error = ex.Message;
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult ShowArtwork()
        {
            try
            {
                var artworkDto = _artworkService.GetItems().OrderBy(a => a.Artwork_name); ;
                var artwork = Mapper.Map<IEnumerable<ArtworkDTO>, IEnumerable<ArtworkViewModel>>(artworkDto).ToList();
                return PartialView("Artwork", artwork);
            }
            catch (Exception ex)
            {
                @ViewBag.Error = ex.Message;
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult ShowMarketingAuthorizHolder()
        {
            try
            {
                var marketingAuthorizHolderDto = _marketingAuthorizHolderService.GetItems().OrderBy(a => a.Name);
                var marketingAuthorizHolder = Mapper.Map<IEnumerable<MarketingAuthorizHolderDTO>, IEnumerable<MarketingAuthorizHolderViewModel>>(marketingAuthorizHolderDto).ToList();
                return PartialView("MarketingAuthorizHolder", marketingAuthorizHolder);
            }
            catch (Exception ex)
            {
                @ViewBag.Error = ex.Message;
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult ShowPharmaceuticalForm()
        {
            try
            {
                var pharmaceuticalFormDto = _pharmaceuticalFormService.GetItems().OrderBy(a => a.PharmaForm);
                var pharmaceuticalForm = Mapper.Map<IEnumerable<PharmaceuticalFormDTO>, IEnumerable<PharmaceuticalFormViewModel>>(pharmaceuticalFormDto).ToList();
                return PartialView("PharmaceuticalForm", pharmaceuticalForm);
            }
            catch (Exception ex)
            {
                @ViewBag.Error = ex.Message;
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult DeleteInfo(string Id, string objectType)
        {
            if (string.IsNullOrEmpty(Id) || string.IsNullOrEmpty(objectType))
            {
                @ViewBag.Error = "Error happened in Settings DeleteInfo method: no id in GET request.";
                return View("Error");
            }
            try
            {
                switch (objectType)
                {
                    case "ApprDocsType":
                        _apprDocsTypeService.DeleteItem(int.Parse(Id));
                        _apprDocsTypeService.Commit();
                        var apprDocsTypesDto = _apprDocsTypeService.GetItems();
                        var apprDocsTypes = Mapper.Map<IEnumerable<ApprDocsTypeDTO>, IEnumerable<ApprDocsTypeViewModel>>(apprDocsTypesDto).ToList();
                        return PartialView("ApprDocsType", apprDocsTypes);
                    case "Strength":
                        _strengthService.DeleteItem(int.Parse(Id));
                        _strengthService.Commit();
                        var strengthDto = _strengthService.GetItems();
                        var strength = Mapper.Map<IEnumerable<StrengthDTO>, IEnumerable<StrengthViewModel>>(strengthDto).ToList();
                        return PartialView("Strength", strength);
                    case "Manufacturer":
                        _manufacturerService.DeleteItem(int.Parse(Id));
                        _manufacturerService.Commit();
                        var manufacturerDto = _manufacturerService.GetItems();
                        var manufacturer = Mapper.Map<IEnumerable<ManufacturerDTO>, IEnumerable<ManufacturerViewModel>>(manufacturerDto).ToList();
                        return PartialView("Manufacturer", manufacturer);
                    case "Artwork":
                        _artworkService.DeleteItem(int.Parse(Id));
                        _artworkService.Commit();
                        var artworkDto = _artworkService.GetItems();
                        var artwork = Mapper.Map<IEnumerable<ArtworkDTO>, IEnumerable<ArtworkViewModel>>(artworkDto).ToList();
                        return PartialView("Artwork", artwork);
                    case "MarketingAuthorizHolder":
                        _marketingAuthorizHolderService.DeleteItem(int.Parse(Id));
                        _marketingAuthorizHolderService.Commit();
                        var marketingAuthorizHolderDto = _marketingAuthorizHolderService.GetItems();
                        var marketingAuthorizHolder = Mapper.Map<IEnumerable<MarketingAuthorizHolderDTO>, IEnumerable<MarketingAuthorizHolderViewModel>>(marketingAuthorizHolderDto).ToList();
                        return PartialView("MarketingAuthorizHolder", marketingAuthorizHolder);
                    case "PharmaceuticalForm":
                        _pharmaceuticalFormService.DeleteItem(int.Parse(Id));
                        _pharmaceuticalFormService.Commit();
                        var pharmaceuticalFormDto = _pharmaceuticalFormService.GetItems();
                        var pharmaceuticalForm = Mapper.Map<IEnumerable<PharmaceuticalFormDTO>, IEnumerable<PharmaceuticalFormViewModel>>(pharmaceuticalFormDto).ToList();
                        return PartialView("PharmaceuticalForm", pharmaceuticalForm);
                }
               
                return PartialView("PackSizes");
            }
            catch (Exception ex)
            {
                @ViewBag.Error = ex.Message;
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult AddInfo(string Name, string objectType)
        {
            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(objectType))
            {
                @ViewBag.Error = "Error happened in Settings AddInfo method: no params in GET request.";
                return View("Error");
            }
            try
            {
                switch (objectType)
                {
                    case "ApprDocsType":
                        _apprDocsTypeService.AddItem(new ApprDocsTypeDTO() { ApprType = Name });
                        _apprDocsTypeService.Commit();
                        var apprDocsTypesDto = _apprDocsTypeService.GetItems();
                        var apprDocsTypes = Mapper.Map<IEnumerable<ApprDocsTypeDTO>, IEnumerable<ApprDocsTypeViewModel>>(apprDocsTypesDto).ToList();
                        return PartialView("ApprDocsType", apprDocsTypes);
                    case "Strength":
                        _strengthService.AddItem(new StrengthDTO() { Strngth = Name });
                        _strengthService.Commit();
                        var strengthDto = _strengthService.GetItems();
                        var strength = Mapper.Map<IEnumerable<StrengthDTO>, IEnumerable<StrengthViewModel>>(strengthDto).ToList();
                        return PartialView("Strength", strength);
                    case "Manufacturer":
                        _manufacturerService.AddItem(new ManufacturerDTO() { Name = Name });
                        _manufacturerService.Commit();
                        var manufacturerDto = _manufacturerService.GetItems();
                        var manufacturer = Mapper.Map<IEnumerable<ManufacturerDTO>, IEnumerable<ManufacturerViewModel>>(manufacturerDto).ToList();
                        return PartialView("Manufacturer", manufacturer);
                    case "Artwork":
                        _artworkService.AddItem(new ArtworkDTO() { Artwork_name = Name });
                        _artworkService.Commit();
                        var artworkDto = _artworkService.GetItems();
                        var artwork = Mapper.Map<IEnumerable<ArtworkDTO>, IEnumerable<ArtworkViewModel>>(artworkDto).ToList();
                        return PartialView("Artwork", artwork);
                    case "MarketingAuthorizHolder":
                        _marketingAuthorizHolderService.AddItem(new MarketingAuthorizHolderDTO() { Name = Name });
                        _marketingAuthorizHolderService.Commit();
                        var marketingAuthorizHolderDto = _marketingAuthorizHolderService.GetItems();
                        var marketingAuthorizHolder = Mapper.Map<IEnumerable<MarketingAuthorizHolderDTO>, IEnumerable<MarketingAuthorizHolderViewModel>>(marketingAuthorizHolderDto).ToList();
                        return PartialView("MarketingAuthorizHolder", marketingAuthorizHolder);
                    case "PharmaceuticalForm":
                        _pharmaceuticalFormService.AddItem(new PharmaceuticalFormDTO() { PharmaForm = Name });
                        _pharmaceuticalFormService.Commit();
                        var pharmaceuticalFormDto = _pharmaceuticalFormService.GetItems();
                        var pharmaceuticalForm = Mapper.Map<IEnumerable<PharmaceuticalFormDTO>, IEnumerable<PharmaceuticalFormViewModel>>(pharmaceuticalFormDto).ToList();
                        return PartialView("PharmaceuticalForm", pharmaceuticalForm);
                }

                return PartialView("PackSizes");
            }
            catch (Exception ex)
            {
                @ViewBag.Error = ex.Message;
                return View("Error");
            }
        }

        public ActionResult CountrySettings()
        {
            var countrySettingDto = _countrySettingsService.GetItems();
            var countrSettings = Mapper.Map<IEnumerable<CountrySettingDTO>, IEnumerable<CountrySettingViewModel>>(countrySettingDto);
            return View(countrSettings.ToList());
        }

        [HttpGet]
        public ActionResult CreateCountrySetting()
        {
            try
            {
                var countries = _countryService.GetItems();
                @ViewBag.Countries = countries.OrderBy(a => a.Name);
                return View();
            }
            catch (Exception e)
            {
                var userName = User.Identity.Name;
                Logger.Log.Error($"{userName}: CreateCountrySetting() {e.Message} ");

                @ViewBag.Error = e.Message;
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateCountrySetting(CountrySettingViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Error");
            }

            var countryId = model.CountryId ?? 0;
            if (countryId == 0)
            {
                ViewBag.Error = @Resources.ErrorMessages.NoCountryId;
                return View("Error");
            }

            var setting = _countrySettingsService.GetItem(countryId);
            if (setting != null)
            {
                ViewBag.Error = @Resources.ErrorMessages.SettingAlreadyExists;
                return View("Error");
            }

            try
            {
                var item = Mapper.Map<CountrySettingViewModel, CountrySettingDTO>(model);
                
                _countrySettingsService.AddItem(item);
                _countrySettingsService.Commit();
                _countrySettingsService.Dispose();

                var userName = User.Identity.Name;
                Logger.Log.Info($"{userName}: CreateCountrySetting {model.CountryId} ");

                TempData["Success"] = Resources.Messages.SettingsCreateSuccess;
                return RedirectToAction("CountrySettings");
            }
            catch (Exception ex)
            {
                var userName = User.Identity.Name;
                Logger.Log.Error($"{userName}: CreateProduct() {ex.Message} ");

                @ViewBag.Error = ex.Message;
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult EditCountrySetting(int? countryId)
        {
            var _countryId = countryId ?? 0;
            if (_countryId == 0)
            {
                @ViewBag.Error = @Resources.ErrorMessages.NoCountryId;
                return View("Error");
            }

            try
            {
                var countrySetting = _countrySettingsService.GetItem(_countryId);
                var model = new CountrySettingViewModel()
                {
                    CountryId = _countryId,
                    Country = Mapper.Map<Country, CountryDTO>(countrySetting.Country),
                    EanActive = countrySetting.EanActive,
                    GtinActive = countrySetting.GtinActive
                };
                return View(model);
            }
            catch (Exception e)
            {
                var userName = User.Identity.Name;
                Logger.Log.Error($"{userName}: CreateCountrySetting() {e.Message} ");

                @ViewBag.Error = e.Message;
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<ActionResult> UpdateCountrySetting(CountrySettingViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Error");
            }

            try
            {
                var item = Mapper.Map<CountrySettingViewModel, CountrySettingDTO>(model);
                _countrySettingsService.AddItem(item);
                _countrySettingsService.Commit();
                _countrySettingsService.Dispose();

                var userName = User.Identity.Name;
                Logger.Log.Info($"{userName}: CreateCountrySetting {model.CountryId} ");

                TempData["Success"] = Resources.Messages.SettingsCreateSuccess;
                return RedirectToAction("CountrySettings");
            }
            catch (Exception ex)
            {
                var userName = User.Identity.Name;
                Logger.Log.Error($"{userName}: CreateProduct() {ex.Message} ");

                @ViewBag.Error = ex.Message;
                return View("Error");
            }
        }

    }

}
