using Application.Core.Entities;
using AutoMapper;
using Puplic_API.DTOs;

namespace Puplic_API.Profiles
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<Account, AccountDto>().ReverseMap();
        }
    }

    public class AccountCreateProfile : Profile
    {
        public AccountCreateProfile()
        {
            CreateMap<Account, AccountCreateDto>()
                .ForMember(dest => dest.InitialCredit, act => act.MapFrom(src => src.CreditBalance))
                .ReverseMap();

        }
    }
}
