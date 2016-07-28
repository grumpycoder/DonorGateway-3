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
                   .ForMember(vm => vm.HeaderText, map => map.MapFrom(m => m.Event.Template.HeaderText))
                   .ForMember(vm => vm.BodyText, map => map.MapFrom(m => m.Event.Template.BodyText))
                   .ForMember(vm => vm.FooterText, map => map.MapFrom(m => m.Event.Template.FooterText))
                   .ForMember(vm => vm.FAQText, map => map.MapFrom(m => m.Event.Template.FAQText))
                   .ForMember(vm => vm.YesText, map => map.MapFrom(m => m.Event.Template.YesText))
                   .ForMember(vm => vm.NoText, map => map.MapFrom(m => m.Event.Template.NoText))
                   .ForMember(vm => vm.WaitText, map => map.MapFrom(m => m.Event.Template.WaitText))
                   .ForMember(vm => vm.Image, map => map.MapFrom(m => m.Event.Template.Image))
                   .ForMember(vm => vm.MimeType, map => map.MapFrom(m => m.Event.Template.MimeType))
                   .ForMember(vm => vm.DisplayName, map => map.MapFrom(m => m.Event.EventName))
                   .ForMember(vm => vm.StartDate, map => map.MapFrom(m => m.Event.StartDate.Value.ToString("dd MMM yyyy @ hh:mm")))
                   .ForMember(vm => vm.EndDate, map => map.MapFrom(m => m.Event.EndDate.Value.ToString("dd MMM yyyy @ hh:mm")))
                   .ForMember(vm => vm.VenueOpenDate, map => map.MapFrom(m => m.Event.VenueOpenDate.Value.ToString("dd MMM yyyy @ hh:mm")))
                   ;
            });

        }
        protected override void Configure()
        {

        }
    }
}