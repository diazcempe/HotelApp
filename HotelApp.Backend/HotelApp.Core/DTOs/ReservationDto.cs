using System;
using System.Collections.Generic;
using System.Text;

namespace HotelApp.Core.DTOs
{
    public class ReservationDto
    {
        public int Id { get; set; }

        public RoomDto Room { get; set; }

        public GuestDto Guest { get; set; }

        public DateTime CheckinDate { get; set; }

        public DateTime CheckoutDate { get; set; }
    }
}
