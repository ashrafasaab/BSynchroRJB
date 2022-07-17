using Application.Core.Entities;
using AutoMapper;
using Puplic_API.DTOs;

namespace Puplic_API.Profiles
{
    public class TransactionProfile : Profile
    {
        public TransactionProfile()
        {
            CreateMap<Transaction, TransactionCreateDto>()
                 .ForMember(dest => dest.creditValue, act => act.MapFrom(src => src.Credit))
                 .ForMember(dest => dest.accountId, act => act.MapFrom(src => src.AccountId))
                .ReverseMap();

            CreateMap<TransactionDto, Transaction>()
                .ForMember(dest => dest.Credit, act => act.MapFrom(src => src.CreditValue))
                .ForMember(dest => dest.CreationDate, act => act.MapFrom(src => src.CreationDate))
                .ReverseMap();

        }
    }
}
