using System;
using System.Collections.Generic;
using System.Linq;
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
using Olga.DAL.Entities.Account;
using Olga.Models;

namespace Olga.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        readonly ICountry _countryService;

        public HomeController(ICountry serv)
        {
            _countryService = serv;
        }

        private IUserService UserService => HttpContext.GetOwinContext().GetUserManager<IUserService>();

        public ActionResult Index()
        {
            var countries = Mapper.Map<IEnumerable<CountryDTO>, List<CountryViewModel>>(_countryService.GetItems().OrderBy(a=>a.Name));
            var user = GetCurrentUser();
            var userCountries = User.IsInRole("Admin") || User.IsInRole("Holder") ? countries : user.Countries;
            ViewBag.User = user;
            return View(userCountries);
        }

        //public ActionResult GetCountries()
        //{
        //    return PartialView("Countries");
        //}

        public ActionResult Menu()
        {
            var countries = Mapper.Map<IEnumerable<CountryDTO>, List<CountryViewModel>>(_countryService.GetItems()).OrderBy(a=>a.Name).ToList();
            ViewBag.User = GetCurrentUser();
            return PartialView("_Navigation", countries);
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
            catch (Exception)
            {
                return new UserViewModel();
            }
        }

        public ActionResult Error(string message)
        {
            ViewBag.Error = message;
            return View();
        }
    }
}