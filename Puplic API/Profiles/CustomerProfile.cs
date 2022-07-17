using Application.Core.Entities;
using AutoMapper;
using Puplic_API.DTOs;

namespace Puplic_API.Profiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerDto>().ReverseMap();
            CreateMap<Customer, CustomerFullInformationDto>().ReverseMap();
        }
    }
}
