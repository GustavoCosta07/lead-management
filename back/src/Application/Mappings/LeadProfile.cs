using AutoMapper;
using MyApp.Application.DTOs;
using MyApp.Domain.Entities;
using MyApp.Domain.Enums;

namespace MyApp.Application.Mappings
{
    public class LeadProfile : Profile
    {
        public LeadProfile()
        {
            CreateMap<Lead, LeadSummaryDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));

            CreateMap<Lead, LeadDetailsDto>();
        }
    }
}