using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HotelApp.Core.DTOs;
using Humanizer;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace HotelApp.Core.Validations
{
    public static class ValidatorUtil
    {
        public static ValidationErrorDto MapToErrorDto(ModelStateDictionary dictionary)
        {
            return new ValidationErrorDto
            {
                InputErrors = dictionary
                    .Select(entry =>
                    {
                        var (key, value) = entry;
                        return new InputErrorDto(key,
                            value.Errors.Select(err => err.ErrorMessage).ToArray());
                    }).ToArray()
            };
        }

        public static ValidationErrorDto MapToErrorDto(OpDto opDto)
        {
            return new ValidationErrorDto
            {
                InputErrors = opDto.InputValidationErrors.GroupBy(s => s.Item1)
                    .Select(entry =>
                    {
                        return new InputErrorDto(entry.Key,
                            entry.Select(s => s.Item2).ToArray());
                    }).ToArray(),
                StateErrors = opDto.StateValidationErrors.Select(entry => new StateErrorDto(entry)).ToArray()
            };
        }

        public static (string, string) CreateExistsError(string fieldName, object value)
        {
            return (fieldName, $"{fieldName.Humanize()} with the value {value} is not found.");
        }

        public static (string, string) CreateUniqueError(string fieldName, object value)
        {
            return (fieldName, $"{fieldName.Humanize()} with the value {value} is already in use.");
        }
    }
}
