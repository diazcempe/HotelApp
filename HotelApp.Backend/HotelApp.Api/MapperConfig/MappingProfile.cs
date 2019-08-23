using AutoMapper;
using HotelApp.Core.DTOs;
using HotelApp.Core.Models;

namespace HotelApp.Api.MapperConfig
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Guest, GuestDto>();
            CreateMap<Room, RoomDto>();
            CreateMap<Reservation, ReservationDto>();
        }
    }
}
