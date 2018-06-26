using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Configuration;
using Olga.BLL.DTO;
using Olga.BLL.Interfaces;
using Olga.BLL.Services;
using Olga.DAL.Entities.Account;

namespace Olga.BLL.AutoMapper
{
    internal class MapperForUser
    {
        public static IMapper GetUserMapperForView(UserService _userService)
        {
            var confExpressMap = new MapperConfigurationExpression();
            var buildMap = confExpressMap.CreateMap<ClientProfile, UserDTO>();

            buildMap.ForMember(dest => dest.Email, opt => opt.MapFrom(c => c.ApplicationUser.Email.ToString()));
            buildMap.ForMember(dest => dest.Role, opt => opt.MapFrom(m => _userService.GetRoleNameById(m.ApplicationUser.Roles.FirstOrDefault().RoleId.ToString())));
            buildMap.ForMember(dest => dest.OldRole, opt => opt.MapFrom(m => _userService.GetRoleNameById(m.ApplicationUser.Roles.FirstOrDefault().RoleId.ToString())));

            buildMap.ForMember(dest => dest.Rank, opt => opt.MapFrom(m => m.Rank));
            buildMap.ForMember(dest => dest.Name, opt => opt.MapFrom(m => m.Name));
            buildMap.ForMember(dest => dest.Id, opt => opt.MapFrom(m => m.Id));

            buildMap.ForMember(dest => dest.Password, opt => opt.Ignore());

            var configuration = new MapperConfiguration(confExpressMap);
            return configuration.CreateMapper();
        }

        public static IMapper GetUserMapperForEdit(UserService _userService)
        {
            var confExpressMap = new MapperConfigurationExpression();
            var buildMap = confExpressMap.CreateMap<UserDTO, ClientProfile>();

            buildMap.ForMember(dest => dest.Id, opt => opt.Ignore());
            buildMap.ForMember(dest => dest.Rank, opt => opt.MapFrom(m => m.Rank));
            buildMap.ForMember(dest => dest.Name, opt => opt.MapFrom(m => m.Name));
            //buildMap.ForMember(dest => dest.ApplicationUser, opt => opt.Ignore());

            var configuration = new MapperConfiguration(confExpressMap);
            return configuration.CreateMapper();
        }
    }
}
