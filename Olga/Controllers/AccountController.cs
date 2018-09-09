using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Olga.BLL.DTO;
using Olga.BLL.Infrastructure;
using Olga.BLL.Interfaces;
using Olga.BLL.Services;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using Microsoft.AspNet.Identity;
using Olga.AutoMapper;
using Olga.DAL.Entities.Account;
using Olga.Models;
using Olga.Util;

namespace Olga.Controllers
{
    [Authorize(Roles = "Admin")]
    //[AllowAnonymous]
    public class AccountController : Controller
    {
        ICountry _countryService;

        public AccountController(ICountry serv)
        {
            _countryService = serv;
        }

        private IUserService UserService
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<IUserService>();
            }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model)
        {
            await SetInitialDataAsync();
            if (ModelState.IsValid)
            {
                UserDTO userDto = new UserDTO { Email = model.Email, Password = model.Password };
                ClaimsIdentity claim = await UserService.Authenticate(userDto);
                if (claim == null)
                {
                    Logger.Log.Error(Resources.ErrorMessages.NotCorrectLogin);
                    ModelState.AddModelError("NotCorrectLogin", Resources.ErrorMessages.NotCorrectLogin);
                }
                else
                {
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);
                    Logger.Log.Info($"{userDto.Email} Logged in");
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }

        [Authorize]
        public ActionResult Logout()
        {
            var userName = User.Identity.Name;
            Logger.Log.Info($"{userName} Logged out");

            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            var all = _countryService.GetItems().ToList();
            var allCountries = Mapper.Map<IEnumerable<CountryDTO>, IEnumerable<CountryViewModel>>(all);

            @ViewBag.userCountries = allCountries.Select(o => new SelectListItem
            {
                Text = o.Name,
                Value = o.Id.ToString()
            });
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterModel model, string[] selectedCountries)
        {
            await SetInitialDataAsync();
            if (ModelState.IsValid)
            {
                
                UserDTO userDto = new UserDTO
                {
                    Email = model.Email,
                    Password = model.Password,
                    Rank = model.Rank,
                    Name = model.Name,
                    Role = model.Role.ToString(),
                    NcAccess = model.NcAccess
                };
                userDto = AddCountriesToUser(userDto, selectedCountries);
                OperationDetails operationDetails = await UserService.Create(userDto);
                if (operationDetails.Succedeed)
                {
                    InitialiseSuccessRegisster(userDto);
                    return RedirectToAction("Users", "Account");
                }
                ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
                Logger.Log.Error($"in AccountController/Register {operationDetails.Message}");
            await SetInitialDataAsync();

            }
            return View(model);
        }

        public void InitialiseSuccessRegisster(UserDTO userDto)
        {
            var userName = User.Identity.Name;
            Logger.Log.Info($"{userName} registered {userDto.Email} ");
            TempData["Success"] = $"User { userDto.Email} registered success!";
        }

        public UserDTO AddCountriesToUser(UserDTO userDto, string[] selectedCountries)
        {
            if (selectedCountries != null)
            {
                foreach (var country in selectedCountries)
                {
                    var countryItem = _countryService.GetItem(int.Parse(country));
                    userDto.Countries.Add(countryItem);
                }
            }
            return userDto;
        }

        [HttpGet]
        public ActionResult EditUser(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }

            var user = UserService.GetUser(id);

            var userMapper = MapperForUser.GetUserEditMapper(UserService);
            var map = userMapper.Map<UserDTO, UserEditModel>(user);

            var allCountries = Mapper.Map<IEnumerable<CountryDTO>, IEnumerable<CountryViewModel>>(_countryService.GetItems().ToArray());

            @ViewBag.userCountries = allCountries.Select(o => new SelectListItem
            {
                Text = o.Name,
                Value = o.Id.ToString()
            });

            return View(map);
        }

        [HttpPost]
        public ActionResult EditUser(UserEditModel editModel, string[] selectedCountries) //TODO unit tests to check old valuest to new values (including role) update
        {
            if (editModel.Id == null) RedirectToAction("Users");
            if (ModelState.IsValid)
            {
                var mapper = MapperForUser.GetUserMapperToEdit(UserService);
                var userToEdit = mapper.Map<UserEditModel, UserDTO>(editModel);
                userToEdit = AddCountriesToUser(userToEdit, selectedCountries);

                var result = UserService.Update(userToEdit);
                if (result.Result.Succedeed)
                {
                    TempData["Success"] = result.Result.Message;
                }
                else
                {
                    TempData["Error"] = result.Result.Message;
                }
                return RedirectToAction("Users");
            }
            return View(editModel);
        }

        [HttpGet]
        public ActionResult DeleteUser(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                TempData["Success"] = "User not exists!";
                return RedirectToAction("Users");
            }

            var result = UserService.Delete(id).Result;

            if (result.Succedeed)
            {
                TempData["Success"] = result.Message;
            }
            else
            {
                TempData["Error"] = result.Message;
            }
            return RedirectToAction("Users");
        }



        private async Task SetInitialDataAsync()
        {
            await UserService.SetInitialData(new UserDTO
            {
                Email = "ok@lykospharma.com",
                Password = "55555",
                Name = "Olga Kravchuk",
                Rank = "Head of Regulatory Affairs Russia, Ukraine and CIS Countries"
            }, new List<string> { "user", "admin" });
        }

        public ActionResult Users()
        {
            var userMapper = MapperForUser.GetUserMapperForView(UserService);
            var users = UserService.GetAll().OrderBy(a=>a.Name).ToList();
            var userViewModels = userMapper.Map<IEnumerable<UserDTO>, IEnumerable<UserViewModel>>(users).OrderBy(a=>a.Name);
            return View(userViewModels.ToList());
        }

        public UserViewModel GetCurrentUser()
        {
            var userId = HttpContext.User.Identity.GetUserId();
            var user = UserService.GetUser(userId);
            var userMapper = MapperForUser.GetUserMapperToEdit(UserService);
            return userMapper.Map<UserDTO, UserViewModel>(user);
        }
    }
}