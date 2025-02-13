using AutoMapper;
using FaiseStock.Data.Models;
using FaiseStock.Data.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FaiseStock.Data.Models.ViewModels;

namespace FaiseStock.Utilities.Mapping
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<TopUser, TopUserDto>().ForMember(x => x.UserName, opt => opt.MapFrom(x => x.User.Name))
                .ForMember(x => x.ContestName, opt => opt.MapFrom(x => x.Contest.ContestName)).ReverseMap();
            CreateMap<Contest, ContestDto>().ReverseMap();
            CreateMap<Contest, ContestVM>().ReverseMap();
            CreateMap<Wallet, WalletDto>().ReverseMap();
            CreateMap<Wallet, WalletVM>().ForMember(x => x.UserName, opt => opt.MapFrom(x => x.User.Name)).ReverseMap();
            CreateMap<ContestParticipant, ContestParticipantDto>().ReverseMap();
            CreateMap<ContestParticipant, ContestParticipantVM>()
                .ForMember(x => x.ContestName, opt => opt.MapFrom(x => x.Contest.ContestName))
                .ForMember(x => x.UserName, opt => opt.MapFrom(x => x.User.Name)).ReverseMap();
        }
    }
}
