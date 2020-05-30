using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Olga.BLL.DTO;
using Olga.DAL.Entities;

namespace Olga.Models
{
    public class CountrySettingViewModel
    {
        public int? CountryId { get; set; }
        public virtual CountryDTO Country { get; set; }
        public bool GtinActive { get; set; }
        public bool EanActive { get; set; }
    }
}