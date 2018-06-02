using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Newtonsoft.Json;
using Olga.BLL.DTO;
using Olga.BLL.Interfaces;
using Olga.Models;

namespace Olga.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        ICountry countryService;

        public HomeController(ICountry serv)
        {
            countryService = serv;
        }

        public ActionResult Index()
        {
            var countries = Mapper.Map<IEnumerable<CountryDTO>, List<CountryViewModel>>(countryService.GetItems());
            return View(countries);
        }

        public ActionResult GetCountries()
        {
            return PartialView("Countries");
        }

        public ActionResult Menu()
        {
            var countries = Mapper.Map<IEnumerable<CountryDTO>, List<CountryViewModel>>(countryService.GetItems());
            return PartialView("_Navigation", countries);
        }


    }
    }