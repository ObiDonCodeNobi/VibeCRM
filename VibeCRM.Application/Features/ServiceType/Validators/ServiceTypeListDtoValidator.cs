using FluentValidation;
using VibeCRM.Application.Features.ServiceType.DTOs;

namespace VibeCRM.Application.Features.ServiceType.Validators
{
    /// <summary>
    /// Validator for the ServiceTypeListDto class.
    /// Defines validation rules for service type list data.
    /// </summary>
    public class ServiceTypeListDtoValidator : AbstractValidator<ServiceTypeListDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceTypeListDtoValidator"/> class.
        /// Configures validation rules for ServiceTypeListDto properties.
        /// </summary>
        public ServiceTypeListDtoValidator()
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

            RuleFor(x => x.ServiceCount)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Service count must be a non-negative number.");
        }
    }
}
