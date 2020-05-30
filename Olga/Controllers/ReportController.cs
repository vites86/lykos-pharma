using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Olga.BLL.DTO;
using Olga.BLL.Interfaces;
using Olga.DAL.Entities;
using Olga.Models;

namespace Olga.Controllers
{
    public class ReportController : Controller
    {
        IProcedure _procedureService;
        ICountry _countryService;

        public ReportController(IProcedure procedure,ICountry serv)
        {
            _procedureService = procedure;
            _countryService = serv;
        }

        // GET: Report
        public ActionResult Index(int? country, string dateFrom, string dateTo)
        {
            if (!User.IsInRole("Admin"))
            {
                @ViewBag.Error = @Resources.ErrorMessages.NoPermissionToRecource;
                return View("Error");
            }

            var allProcedures = new List<ProcedureViewModel>();
            var _allProcedures = _procedureService.GetItems();

            var countries = _countryService.GetItems().OrderBy(a => a.Name).ToList();
            ViewBag.Countries = countries;

            if (country != null && country != 0)
            {
                _allProcedures = _allProcedures.Where(p => p.Product.Country.Id == country);
                ViewBag.Countries = _countryService.GetItems().Where(p=> p.Id == country);
            }
            if (!string.IsNullOrEmpty(dateFrom) || !string.IsNullOrEmpty(dateTo))
            {
                var _dateFrom = string.IsNullOrEmpty(dateFrom) ? DateTime.MinValue : DateTime.Parse(dateFrom);
                var _dateTo = string.IsNullOrEmpty(dateTo) ? DateTime.MaxValue : DateTime.Parse(dateTo);
                _allProcedures = _allProcedures.Where(p => p.SubmissionDate >= _dateFrom && p.SubmissionDate <= _dateTo);
            }

            var procedures = Mapper.Map<IEnumerable<ProcedureDTO>, IEnumerable<ProcedureViewModel>>(_allProcedures).ToList();
            allProcedures.AddRange(procedures);

            var countriesForDrpdwn = countries;
            countriesForDrpdwn.Insert(0, new CountryDTO() { Name = "All", Id = 0 });
            ViewBag.CountriesForDrpdwn = new SelectList(countriesForDrpdwn, "Id", "Name");

            return View(procedures);
        }
    }
}