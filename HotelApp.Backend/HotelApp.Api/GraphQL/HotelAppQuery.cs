using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Types;
using HotelApp.Api.GraphQL.Types;
using HotelApp.Core.Data;
using HotelApp.Core.Enums;
using HotelApp.Core.Models;

namespace HotelApp.Api.GraphQL
{
    public class HotelAppQuery : ObjectGraphType
    {
        /*
         -- Simple test query --
            query TestQuery {
              reservations {
                id
                checkinDate
                checkoutDate
                guest {
                  id
                  name
                  registerDate
                }
                room {
                  id
                  name
                  number
                  allowedSmoking
                  status
                }
              }
            }
        */

        public HotelAppQuery(IReservationRepository reservationRepository)
        {
            Field<ListGraphType<ReservationType>>(
                "reservations",
                arguments: new QueryArguments(new List<QueryArgument>
                {
                    new QueryArgument<IdGraphType> { Name = "id" },
                    new QueryArgument<DateGraphType> { Name = "checkinDate" },
                    new QueryArgument<DateGraphType> { Name = "checkoutDate" },
                    new QueryArgument<BooleanGraphType> { Name = "roomAllowedSmoking" },
                    new QueryArgument<RoomStatusType> { Name = "roomStatus" }
                }),
                resolve: context =>
                {
                    var query = reservationRepository.GetQuery();

                    var reservationId = context.GetArgument<int?>("id");
                    if (reservationId.HasValue)
                    {
                        if (reservationId.Value > 0) return query.Where(r => r.Id == reservationId.Value);

                        context.Errors.Add(new ExecutionError("reservationId must be greater than zero!"));
                        return new List<Reservation>();
                    }

                    var checkinDate = context.GetArgument<DateTime?>("checkinDate");
                    if (checkinDate.HasValue)
                        return query.Where(r => r.CheckinDate.Date == checkinDate.Value.Date);

                    var checkoutDate = context.GetArgument<DateTime?>("checkoutDate");
                    if (checkoutDate.HasValue)
                        return query.Where(r => r.CheckoutDate.Date >= checkoutDate.Value.Date);

                    var allowedSmoking = context.GetArgument<bool?>("roomAllowedSmoking");
                    if (allowedSmoking.HasValue)
                        return query.Where(r => r.Room.AllowedSmoking == allowedSmoking.Value);

                    var roomStatus = context.GetArgument<RoomStatus?>("roomStatus");
                    if (roomStatus.HasValue)
                        return query.Where(r => r.Room.Status == roomStatus.Value);

                    return query.ToList();
                }

            );
        }
    }
}
