using System;
using System.Collections.Generic;
using System.Text;

namespace HotelApp.Core.DTOs
{
    public class ValidationErrorDto
    {
        public ValidationErrorDto()
        {
            InputErrors = new InputErrorDto[0];
            StateErrors = new StateErrorDto[0];
        }

        public InputErrorDto[] InputErrors { get; set; }
        public StateErrorDto[] StateErrors { get; set; }
    }

    public class StateErrorDto
    {
        public StateErrorDto(string message)
        {
            Message = message;
        }

        public string Message { get; set; }
    }

    public class InputErrorDto
    {
        public InputErrorDto(string field, string[] messages)
        {
            Field = field;
            Messages = messages;
        }

        public string Field { get; set; }
        public string[] Messages { get; set; }
    }
}
