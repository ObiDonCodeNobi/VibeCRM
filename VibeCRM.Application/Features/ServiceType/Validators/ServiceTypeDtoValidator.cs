using FluentValidation;
using VibeCRM.Shared.DTOs.ServiceType;

namespace VibeCRM.Application.Features.ServiceType.Validators
{
    /// <summary>
    /// Validator for the ServiceTypeDto class.
    /// Defines validation rules for service type data.
    /// </summary>
    public class ServiceTypeDtoValidator : AbstractValidator<ServiceTypeDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceTypeDtoValidator"/> class.
        /// Configures validation rules for ServiceTypeDto properties.
        /// </summary>
        public ServiceTypeDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Service type ID is required.");

            RuleFor(x => x.Type)
                .NotEmpty()
                .WithMessage("Service type name is required.")
                .MaximumLength(50)
                .WithMessage("Service type name cannot exceed 50 characters.");

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Description is required.")
                .MaximumLength(500)
                .WithMessage("Description cannot exceed 500 characters.");

            RuleFor(x => x.OrdinalPosition)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Ordinal position must be a non-negative number.");
        }
    }
}