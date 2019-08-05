using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using HotelApp.Core.CustomResults;
using HotelApp.Core.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HotelApp.Api.Controllers
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        private readonly ILogger Logger;

        public BaseController(ILogger logger)
        {
            Logger = logger;
        }

        /// <summary>
        /// Returns a response based on the operation result DTO.
        /// </summary>
        /// <param name="opDto"></param>
        /// <returns></returns>
        protected ActionResult RouteOpResult(OpDto opDto)
        {

            switch (opDto.ServerStatusCode)
            {
                case HttpStatusCode.NotFound:
                    return NotFound();
                case HttpStatusCode.NoContent:
                    return new EmptyResult();
                case HttpStatusCode.Forbidden:
                    return new StatusCodeResult(403);
                case HttpStatusCode.Created:
                case HttpStatusCode.OK:
                    return new JsonResult(opDto);
                case HttpStatusCode.UnprocessableEntity:
                    Logger.LogWarning("OpDTO: {@OpDTO}", opDto);
                    return new ValidationFailedResult(opDto);
                case HttpStatusCode.Unauthorized: // for any unauthorised API ops, the server shall return bad request with unauthorised errors
                case HttpStatusCode.InternalServerError: // there are a few services which will return internal server error.
                case HttpStatusCode.BadRequest: // will be returned for validation errors. Messages are assumed to be in the DTO.
                default:
                    Logger.LogWarning("OpDTO: {@OpDTO}", opDto);
                    return new OperationFailedResult(opDto.ServerStatusCode, opDto);
            }
        }
    }
}
