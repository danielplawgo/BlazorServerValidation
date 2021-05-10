using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorServerValidation.Shared
{
    public class Result
    {
        public bool Success { get; set; }

        public IEnumerable<ErrorMessage> Errors { get; set; }

        public static Result<T> Ok<T>(T value)
        {
            return new Result<T>()
            {
                Success = true,
                Value = value
            };
        }

        public static Result<T> Failure<T>(string message)
        {
            var result = new Result<T>();

            result.Success = false;
            result.Errors = new List<ErrorMessage>()
            {
                new ErrorMessage()
                {
                    Message = message
                }
            };

            return result;
        }

        public static Result<T> Failure<T>(IEnumerable<ErrorMessage> messages)
        {
            var result = new Result<T>();

            result.Success = false;
            result.Errors = messages;

            return result;
        }
    }

    public class Result<T> : Result
    {
        public T Value { get; set; }
    }

    public class ErrorMessage
    {
        public string PropertyName { get; set; }

        public string Message { get; set; }
    }
}
