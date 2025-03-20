using System;
using FluentValidation;

namespace VibeCRM.Application.Features.ServiceType.Queries.GetServiceTypeById
{
    /// <summary>
    /// Validator for the GetServiceTypeByIdQuery.
    /// Defines validation rules for retrieving a service type by ID.
    /// </summary>
    public class GetServiceTypeByIdQueryValidator : AbstractValidator<GetServiceTypeByIdQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetServiceTypeByIdQueryValidator"/> class.
        /// Configures validation rules for the GetServiceTypeByIdQuery properties.
        /// </summary>
        public GetServiceTypeByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Service type ID is required.");
        }
    }
}
