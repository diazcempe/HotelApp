using GraphQL.Types;
using HotelApp.Core.Models;

namespace HotelApp.Api.GraphQL.Types
{
    public class ReservationType : ObjectGraphType<Reservation>
    {
        public ReservationType()
        {
            Field(x => x.Id);
            Field(x => x.CheckinDate);
            Field(x => x.CheckoutDate);
            Field<GuestType>(nameof(Reservation.Guest));
            Field<RoomType>(nameof(Reservation.Room));
        }
    }
}
