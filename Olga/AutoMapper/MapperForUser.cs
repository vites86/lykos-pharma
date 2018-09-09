using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using AutoMapper.Configuration;
using Olga.BLL.DTO;
using Olga.BLL.Interfaces;
using Olga.Models;
using Olga.DAL.Entities;
using Olga.DAL.Entities.Account;

namespace Olga.AutoMapper
{
    public class MapperForUser
    {
        public static IMapper GetUserMapperForView(IUserService accountService)
        {
            var confExpressMap = new MapperConfigurationExpression();
            var buildMap = confExpressMap.CreateMap<UserDTO, UserViewModel>();
            buildMap.ForMember(dest => dest.Email, opt => opt.MapFrom(c => c.Email));
            buildMap.ForMember(dest => dest.Role, opt => opt.MapFrom(m => m.Role));
            buildMap.ForMember(dest => dest.Rank, opt => opt.MapFrom(m => m.Rank));
            buildMap.ForMember(dest => dest.NcAccess, opt => opt.MapFrom(m => m.NcAccess));
            var configuration = new MapperConfiguration(confExpressMap);
            return configuration.CreateMapper();
        }

        public static IMapper GetUserEditMapper(IUserService accountService)
        {
            var confExpressMap = new MapperConfigurationExpression();
            var buildMap = confExpressMap.CreateMap<UserDTO, UserEditModel>();

            buildMap.ForMember(dest => dest.Email, opt => opt.MapFrom(c => c.Email));
            buildMap.ForMember(dest => dest.Role, opt => opt.MapFrom(m => m.Role));
            buildMap.ForMember(dest => dest.OldRole, opt => opt.MapFrom(m => m.Role));
            buildMap.ForMember(dest => dest.Rank, opt => opt.MapFrom(m => m.Rank));
            buildMap.ForMember(dest => dest.Name, opt => opt.MapFrom(m => m.Name));
            buildMap.ForMember(dest => dest.NcAccess, opt => opt.MapFrom(m => m.NcAccess));
            buildMap.ForMember(dest => dest.Password, opt => opt.Ignore());

            //buildMap.ForMember(dest => dest.OldRole, opt => opt.MapFrom(m => accountService.GetRoleNameById(m.ApplicationUser.Roles.FirstOrDefault().RoleId.ToString())));

            var configuration = new MapperConfiguration(confExpressMap);
            return configuration.CreateMapper();
        }

        public static IMapper GetUserMapperToEdit(IUserService accountService)
        {
            var confExpressMap = new MapperConfigurationExpression();
            var buildMap = confExpressMap.CreateMap<UserEditModel, UserDTO>();

            buildMap.ForMember(dest => dest.Email, opt => opt.MapFrom(c => c.Email));
            buildMap.ForMember(dest => dest.Role, opt => opt.MapFrom(m => m.Role));
            buildMap.ForMember(dest => dest.OldRole, opt => opt.MapFrom(m => m.OldRole));
            //buildMap.ForMember(dest => dest.OldEmail, opt => opt.MapFrom(m => m.OldEmail));
            buildMap.ForMember(dest => dest.Rank, opt => opt.MapFrom(m => m.Rank));
            buildMap.ForMember(dest => dest.Name, opt => opt.MapFrom(m => m.Name));
            buildMap.ForMember(dest => dest.NcAccess, opt => opt.MapFrom(m => m.NcAccess));
            buildMap.ForMember(dest => dest.Password, opt => opt.Ignore());

            var configuration = new MapperConfiguration(confExpressMap);
            return configuration.CreateMapper();
        }

        public static IMapper GetUserMapperForClientProfile(IUserService accountService)
        {
            var confExpressMap = new MapperConfigurationExpression();
            var buildMap = confExpressMap.CreateMap<UserDTO, ClientProfile>();
            buildMap.ForMember(dest => dest.ApplicationUser, opt => opt.Ignore());
            buildMap.ForMember(dest => dest.Rank, opt => opt.MapFrom(m => m.Rank));
            buildMap.ForMember(dest => dest.NcAccess, opt => opt.MapFrom(m => m.NcAccess));
            buildMap.ForMember(dest => dest.Countries, opt => opt.MapFrom(m => m.Countries));
            var configuration = new MapperConfiguration(confExpressMap);
            return configuration.CreateMapper();
        }

    }
}