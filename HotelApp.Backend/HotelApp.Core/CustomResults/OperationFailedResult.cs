using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using HotelApp.Core.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace HotelApp.Core.CustomResults
{
    public class OperationFailedResult : ObjectResult
    {
        public OperationFailedResult(HttpStatusCode statusCode, OpDto opDto) : base(opDto)
        {
            StatusCode = (int)statusCode;
        }
    }
}
