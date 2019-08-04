using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using HotelApp.Core.Data;
using HotelApp.Core.Models;
using HotelApp.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace HotelApp.Data.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly HotelAppDbContext _hotelAppDbContext;

        public ReservationRepository(HotelAppDbContext hotelAppDbContext)
        {
            _hotelAppDbContext = hotelAppDbContext;
        }

        public async Task<List<T>> GetAll<T>()
        {
            return await _hotelAppDbContext
                .Reservations
                .Include(x => x.Room)
                .Include(x => x.Guest)
                .ProjectTo<T>()
                .ToListAsync();
        }

        public async Task<IEnumerable<Reservation>> GetAll()
        {
            return await _hotelAppDbContext
                .Reservations
                .Include(x => x.Room)
                .Include(x => x.Guest)
                .ToListAsync();
        }
    }
}
