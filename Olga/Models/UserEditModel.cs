﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Olga.DAL.Entities.Account;

namespace Olga.Models
{
    public class UserEditModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Rank { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        //public string OldEmail { get; set; }
        public Roles OldRole { get; set; }
        public Roles Role { get; set; }
        public List<CountryViewModel> Countries { get; set; }
        public bool NcAccess { get; set; }

        [DisplayName("M.A.Holder")]
        public MarketingAuthorizHolderViewModel MarketingAuthorizHolder { get; set; }
        public int? MarketingAuthorizHolderId { get; set; }

    }
}