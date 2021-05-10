using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorServerValidation.Server.Extensions;
using BlazorServerValidation.Shared;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BlazorServerValidation.Server.Products
{
    [ApiController]
    public class CreateProduct : Controller
    {
        private readonly IValidator<CreateProductCommand> _validator;

        public CreateProduct(IValidator<CreateProductCommand> validator)
        {
            _validator = validator;
        }

        [Route("api/products")]
        public async Task<ActionResult> Create(CreateProductCommand command)
        {
            var validationResult = await _validator.ValidateAsync(command);

            if (validationResult.IsValid == false)
            {
                return BadRequest(validationResult.ToResult());
            }

            return Ok(Result.Ok(Guid.NewGuid()));
        }


        public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
        {
            public CreateProductCommandValidator()
            {
                RuleFor(c => c.Name)
                    .NotEmpty();
            }
        }
    }
}
