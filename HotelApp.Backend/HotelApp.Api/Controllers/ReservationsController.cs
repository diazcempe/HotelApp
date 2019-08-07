using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelApp.Core.Data;
using HotelApp.Core.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace HotelApp.Api.Controllers
{
    [Route("api/[controller]")]
    public class ReservationsController : Controller
    {
        private readonly IReservationRepository _reservationRepository;

        public ReservationsController(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        [HttpGet("[action]")]
        public async Task<List<ReservationDto>> List()
        {
            return await _reservationRepository.GetAll<ReservationDto>();
        }
    }
}
