using FluentValidation;
using VibeCRM.Application.Features.Service.DTOs;

namespace VibeCRM.Application.Features.Service.Validators
{
    /// <summary>
    /// Validator for the ServiceDto.
    /// Provides validation rules for the base service data transfer object.
    /// </summary>
    public class ServiceDtoValidator : AbstractValidator<ServiceDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceDtoValidator"/> class
        /// and defines the validation rules.
        /// </summary>
        public ServiceDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty).WithMessage("Service ID is required");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Service name is required")
                .MaximumLength(100).WithMessage("Service name must not exceed 100 characters");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Service description must not exceed 500 characters")
                .When(x => !string.IsNullOrEmpty(x.Description));

            RuleFor(x => x.ServiceTypeId)
                .NotEqual(Guid.Empty).WithMessage("ServiceTypeId is required");
        }
    }
}