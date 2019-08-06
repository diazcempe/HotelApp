using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Types;

namespace HotelApp.Api.GraphQL
{
    public class HotelAppSchema : Schema
    {
        public HotelAppSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<HotelAppQuery>();
        }
    }
}
