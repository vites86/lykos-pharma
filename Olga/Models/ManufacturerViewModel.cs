﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Olga.BLL.DTO;

namespace Olga.Models
{
    public class ManufacturerViewModel
    {
        public ManufacturerViewModel()
        {
            this.Products = new List<ProductViewModel>();
        }
        public List<ProductViewModel> Products { get; set; }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}