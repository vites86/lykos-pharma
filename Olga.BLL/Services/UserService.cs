using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using System.Threading.Tasks;
using Olga.BLL.DTO;
using Olga.BLL.Infrastructure;
using Olga.BLL.Interfaces;
using Olga.DAL.Entities.Account;
using Olga.DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNet.Identity.EntityFramework;
using Olga.BLL.AutoMapper;
using Olga.DAL.Entities;

namespace Olga.BLL.Services
{
    public class UserService : IUserService
    {
        IUnitOfWork Database { get; set; }

        public UserService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public async Task<OperationDetails> Create(UserDTO userDto)
        {
            ApplicationUser user = Database.UserManager.FindByEmail(userDto.Email);
            if (user == null)
            {
                user = new ApplicationUser { Email = userDto.Email, UserName = userDto.Email };
                var result = Database.UserManager.Create(user, userDto.Password);
                if (result.Errors.Any())
                    return new OperationDetails(false, result.Errors.FirstOrDefault(), "");
                Database.UserManager.AddToRole(user.Id, userDto.Role);
                ClientProfile clientProfile = new ClientProfile
                {
                    Id = user.Id,
                    Rank = userDto.Rank,
                    Name = userDto.Name,
                    NcAccess = userDto.NcAccess
                };

                foreach (var country in userDto.Countries)
                {
                    var item = Database.GetCountry(country.Id);
                    clientProfile.Countries.Add(item);
                }
                
                Database.ClientManager.CreateClientProfile(clientProfile);
                Database.SaveChanges();
                return new OperationDetails(true, "Регистрация успешно пройдена", "");
            }
                return new OperationDetails(false, "Пользователь с таким логином уже существует", "Email");
        }

        public async Task<OperationDetails> Delete(string userId)
        {
            try
            {
                ApplicationUser user = Database.UserManager.FindById(userId);
                if (user != null)
                {
                    var userRole = user.Roles.FirstOrDefault()?.RoleId;
                    if (!string.IsNullOrEmpty(userRole))
                    {
                       var res0 = Database.UserManager.RemoveFromRole(userId, userRole);
                    }
                    
                    Database.ClientManager.DeleteClientProfile(userId);
                    var res1 = Database.UserManager.Delete(user);
                    Database.SaveChanges();
                    return new OperationDetails(true, "Пользователь удален", "");
                }
                return new OperationDetails(false, "Пользователь не существует", "");
            }
            catch (Exception e)
            {
                return new OperationDetails(false, e.Message + " " + e.InnerException, "");
            }
        }

        public async Task<OperationDetails> Update(UserDTO userDto)
        {
            ApplicationUser user = Database.UserManager.FindByEmail(userDto.Email);
            if (user != null)
            {
                user.Email = userDto.Email;
                user.UserName = userDto.Email;
                Database.UserManager.RemoveFromRole(user.Id, userDto.OldRole);
                Database.UserManager.AddToRole(user.Id, userDto.Role);
                user.ClientProfile.Name = userDto.Name;
                user.ClientProfile.Rank = userDto.Rank;
                user.ClientProfile.NcAccess = userDto.NcAccess;

                user.ClientProfile.Countries.Clear();
                foreach (var country in userDto.Countries)
                {
                    var item = Database.GetCountry(country.Id);
                    user.ClientProfile.Countries.Add(item);
                }

                //var userMapper = MapperForUser.GetUserMapperForEdit(userService);
                //user.ClientProfile = userMapper.Map<UserDTO, ClientProfile>(userDto);
                Database.SaveChanges();
                return new OperationDetails(true, "Обновление успешно", "");
            }
            return new OperationDetails(false, "Пользователь для обновления в базе не найден", "Email");
            
        }

        public async Task<ClaimsIdentity> Authenticate(UserDTO userDto)
        {
            ClaimsIdentity claim = null;
            // находим пользователя
            ApplicationUser user = await Database.UserManager.FindAsync(userDto.Email, userDto.Password);
            // авторизуем его и возвращаем объект ClaimsIdentity
            if (user != null)
                claim = await Database.UserManager.CreateIdentityAsync(user,
                                            DefaultAuthenticationTypes.ApplicationCookie);
            return claim;
        }

        // начальная инициализация бд
        public async Task SetInitialData(UserDTO adminDto, List<string> roles)
        {
            foreach (string roleName in roles)
            {
                var role = await Database.RoleManager.FindByNameAsync(roleName);
                if (role == null)
                {
                    role = new ApplicationRole { Name = roleName };
                    await Database.RoleManager.CreateAsync(role);
                }
            }
            await Create(adminDto);
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public string GetRoleIdByName(string roleName)
        {
            if (string.IsNullOrEmpty(roleName))
            {
                throw new ArgumentNullException();
            }

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(Database.GetContext()));
            var role = roleManager.Roles.FirstOrDefault(r => r.Name == roleName);
            return role?.Id ?? "";
        }

        public string GetRoleNameById(string roleId)
        {
            if (string.IsNullOrEmpty(roleId))
            {
                throw new ArgumentNullException();
            }
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(Database.GetContext()));
            var role = roleManager.Roles.FirstOrDefault(r => r.Id == roleId);
            return role?.Name;
        }

        public IEnumerable<UserDTO> GetAll()
        {
            var userService = new UserService(Database);
            var userMapper = MapperForUser.GetUserMapperForView(userService);
            var users = Database.GetAll().ToList();
            return userMapper.Map<IEnumerable<ClientProfile>, IEnumerable<UserDTO>>(users);
        }
        

        public UserDTO GetUser(string userId = null, string email = null)
        {

            if (string.IsNullOrEmpty(userId) && string.IsNullOrEmpty(email))
            {
                return null;
            }

            return string.IsNullOrEmpty(userId)
                ? GetAll().FirstOrDefault(m => m.Email.Equals(email))
                : GetAll().FirstOrDefault(m => m.Id.Equals(userId));
        }
        
    }
}
