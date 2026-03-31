using AutoMapper;
using EventBooking.API.DTOs;
using EventBooking.API.Entities;

namespace EventBooking.API.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Event, EventDto>();
        CreateMap<CreateEventDto, Event>();

        CreateMap<Booking, BookingDto>()
            .ForMember(dest => dest.EventTitle,   opt => opt.MapFrom(src => src.Event.Title))
            .ForMember(dest => dest.EventLocation, opt => opt.MapFrom(src => src.Event.Location))
            .ForMember(dest => dest.EventDate,    opt => opt.MapFrom(src => src.Event.Date))
            .ForMember(dest => dest.Status,       opt => opt.MapFrom(src => src.Status.ToString()));

        CreateMap<CreateBookingDto, Booking>();
    }
}