using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HotelApp.Core.Models;
using Microsoft.EntityFrameworkCore.Query;

namespace HotelApp.Core.Data
{
    public interface IReservationRepository
    {
        Task<List<T>> GetAll<T>();
        Task<IEnumerable<Reservation>> GetAll();
        IIncludableQueryable<Reservation, Guest> GetQuery();
        Reservation Get(int id);
    }
}
