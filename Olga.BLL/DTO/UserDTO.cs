using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Olga.DAL.Entities.Account;

namespace Olga.BLL.DTO
{
    public class UserDTO
    {
        public UserDTO()
        {
            Countries = new List<CountryDTO>();
        }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Rank { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        //public string OldEmail { get; set; }
        public string Role { get; set; }
        public string OldRole { get; set; }
        public bool NcAccess { get; set; }
        public List<CountryDTO> Countries { get; set; }

    }
}
