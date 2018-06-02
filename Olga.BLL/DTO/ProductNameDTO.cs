using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Olga.BLL.DTO;

namespace Olga.DAL.Entities
{
    public class ProductNameDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int? CountryId { get; set; }
        public Country Country { get; set; }
    }
}
