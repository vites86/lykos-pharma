using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Olga.BLL.DTO;
using Olga.BLL.Infrastructure;
using Olga.DAL.Entities.Account;

namespace Olga.BLL.Interfaces
{
    public interface IUserService : IDisposable
    {
        Task<OperationDetails> Create(UserDTO userDto);
        Task<OperationDetails> Update(UserDTO userDto);
        Task<ClaimsIdentity> Authenticate(UserDTO userDto);
        Task SetInitialData(UserDTO adminDto, List<string> roles);
        string GetRoleIdByName(string roleName);
        string GetRoleNameById(string roleId);
        IEnumerable<UserDTO> GetAll();
        UserDTO GetUser(string userId = null, string email = null);
        Task<OperationDetails> Delete(string userId);
    }
}
