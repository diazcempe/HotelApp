using GraphQL.Types;
using HotelApp.Core.Models;

namespace HotelApp.Api.GraphQL.Types
{
    public class RoomType : ObjectGraphType<Room>
    {
        public RoomType()
        {
            Field(x => x.Id);
            Field(x => x.Name);
            Field(x => x.Number);
            Field(x => x.AllowedSmoking);
            Field<RoomStatusType>(nameof(Room.Status));
        }
    }
}
