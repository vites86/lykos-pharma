﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Olga.DAL.Entities
{
    public class CountrySetting
    {
        [Key]
        [ForeignKey("Country")]
        public int? CountryId { get; set; }
        public virtual Country Country { get; set; }

        public bool GtinActive { get; set; }
        public bool EanActive { get; set; }
    }
}
