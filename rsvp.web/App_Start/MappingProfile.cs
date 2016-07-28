using AutoMapper;
using DonorGateway.Domain;
using rsvp.web.ViewModels;
using System.Linq;

namespace web.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Event, EventViewModel>()
                    .ForMember(vm => vm.EventId, map => map.MapFrom(m => m.Id))
                    .ForMember(vm => vm.EventName, map => map.MapFrom(m => m.Name))
                    .ForMember(vm => vm.RegisteredGuestCount, map => map.MapFrom(x => x.Guests.Sum(guest => guest.TicketCount)))
                    .ForMember(vm => vm.WaitingGuestCount, map => map.MapFrom(x => x.Guests.Where(g => g.IsWaiting == true).Sum(guest => guest.TicketCount)));

                cfg.CreateMap<Guest, RegisterFormViewModel>()
                    .ForMember(vm => vm.PromoCode, map => map.MapFrom(m => m.FinderNumber))
                    .ForMember(vm => vm.GuestId, map => map.MapFrom(m => m.Id))
                    .ForMember(vm => vm.Template, map => map.MapFrom(m => m.Event.Template))
                    .ReverseMap();

                cfg.CreateMap<Guest, FinishFormViewModel>()
                          ;
            });

        }
        protected override void Configure()
        {

        }
    }
}