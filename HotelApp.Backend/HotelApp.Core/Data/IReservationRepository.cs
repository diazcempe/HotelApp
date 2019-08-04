using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HotelApp.Core.Models;

namespace HotelApp.Core.Data
{
    public interface IReservationRepository
    {
        Task<List<T>> GetAll<T>();
        Task<IEnumerable<Reservation>> GetAll();
    }
}
