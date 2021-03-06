﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Olga.Models
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Rank { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        [DisplayName("ND/MQC")]
        public bool NcAccess { get; set; }
        public List<CountryViewModel> Countries { get; set; }
        [DisplayName("M.A.Holder")]
        public MarketingAuthorizHolderViewModel MarketingAuthorizHolder { get; set; }
        public int? MarketingAuthorizHolderId { get; set; }

    }
}