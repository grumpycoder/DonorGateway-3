using System;
using AutoMapper;
using DonorGateway.Domain;
using web.Controllers;
using web.ViewModels;

namespace web.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Guest, GuestExportViewModel>()
                    .ForMember(dest => dest.EventCode, opt => opt.MapFrom(src => src.Event.EventCode))
                    .ForMember(dest => dest.EventName, opt => opt.MapFrom(src => src.Event.EventName));
            });

        }
        protected override void Configure()
        {

        }
    }
}