using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorServerValidation.Shared;
using FluentValidation.Results;

namespace BlazorServerValidation.Server.Extensions
{
    public static class ValidationResultExtensions
    {
        public static Result<T> ToResult<T>(this ValidationResult validationResult)
        {
            var result = new Result<T>();

            result.Success = false;
            result.Errors = validationResult.Errors.Select(v => new ErrorMessage()
            {
                PropertyName = v.PropertyName,
                Message = v.ErrorMessage
            });

            return result;
        }

        public static Result ToResult(this ValidationResult validationResult)
        {
            var result = new Result();

            result.Success = false;
            result.Errors = validationResult.Errors.Select(v => new ErrorMessage()
            {
                PropertyName = v.PropertyName,
                Message = v.ErrorMessage
            });

            return result;
        }
    }
}
