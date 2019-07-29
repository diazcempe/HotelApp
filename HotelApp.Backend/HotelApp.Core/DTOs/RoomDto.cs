using System;
using System.Collections.Generic;
using System.Text;
using HotelApp.Core.Enums;

namespace HotelApp.Core.DTOs
{
    public class RoomDto
    {
        public int Id { get; set; }

        public int Number { get; set; }

        public string Name { get; set; }

        public RoomStatus Status { get; set; }

        public bool AllowedSmoking { get; set; }
    }
}
