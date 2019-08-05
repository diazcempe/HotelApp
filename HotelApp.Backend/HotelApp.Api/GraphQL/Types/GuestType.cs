using GraphQL.Types;
using HotelApp.Core.Models;

namespace HotelApp.Api.GraphQL.Types
{
    public class GuestType : ObjectGraphType<Guest>
    {
        public GuestType()
        {
            Field(x => x.Id);
            Field(x => x.Name);
            Field(x => x.RegisterDate);
        }
    }
}
