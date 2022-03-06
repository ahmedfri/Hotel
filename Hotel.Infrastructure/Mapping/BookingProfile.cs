

using AutoMapper;
using Hotel.Domain.Entities;

namespace Hotel.Infrastructure.Mapping
{

    public class BookingProfile : Profile
    {
        public BookingProfile()
        {
            //CreateMap<Booking, CreateBookingCommand>();
            //CreateMap<CreateBookingCommand, Booking>();
            //CreateMap<Booking, BookingDto>()
            // .ForMember(from => from.Room, to => to.MapFrom(value => value.Room))
            //    ;
        }
    }
}
