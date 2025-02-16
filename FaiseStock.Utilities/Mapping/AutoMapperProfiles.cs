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
            CreateMap<TopUser, TopUserDto>()
                .ForMember(x => x.contestName, opt => opt.MapFrom(x => x.contest.contestName)).ReverseMap();
            CreateMap<Contest, ContestDto>().ReverseMap();
            CreateMap<Contest, ContestVM>().ReverseMap();
            CreateMap<Wallet, WalletDto>().ReverseMap();
            CreateMap<Wallet, WalletVM>().ReverseMap();
            CreateMap<ContestParticipant, ContestParticipantDto>().ReverseMap();
            CreateMap<ContestParticipant, ContestParticipantVM>()
                .ForMember(x => x.contestName, opt => opt.MapFrom(x => x.contest.contestName))
              .ReverseMap();
        }
    }
}
