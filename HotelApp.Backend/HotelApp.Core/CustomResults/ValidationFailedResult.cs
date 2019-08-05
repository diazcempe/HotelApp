using HotelApp.Core.DTOs;
using HotelApp.Core.Validations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace HotelApp.Core.CustomResults
{
    /// <summary>
    /// 
    /// Courtesy http://www.jerriepelser.com/blog/validation-response-aspnet-core-webapi/
    /// </summary>
    public class ValidationFailedResult : ObjectResult
    {
        public ValidationFailedResult() : base(null)
        {
            StatusCode = StatusCodes.Status422UnprocessableEntity;
        }

        public ValidationFailedResult(ModelStateDictionary modelState) : this()
        {
            Value = ValidatorUtil.MapToErrorDto(modelState);
        }

        public ValidationFailedResult(OpDto modelState) : this()
        {
            Value = ValidatorUtil.MapToErrorDto(modelState);
        }
    }
}
