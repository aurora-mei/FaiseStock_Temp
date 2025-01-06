using AutoMapper;
using FaiseStock.Data.Models;
using FaiseStock.Data.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaiseStock.Utilities.Mapping
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<TopUser, TopUserDto>().ForMember(x => x.UserName, opt => opt.MapFrom(x => x.User.Name)).ReverseMap();
        }
    }
}
