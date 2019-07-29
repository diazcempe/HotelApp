using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HotelApp.Core.DTOs;
using HotelApp.Core.Models;

namespace HotelApp.Api.AutoMapper
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
