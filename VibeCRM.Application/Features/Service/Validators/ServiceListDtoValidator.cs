using FluentValidation;
using VibeCRM.Application.Features.Service.DTOs;

namespace VibeCRM.Application.Features.Service.Validators
{
    /// <summary>
    /// Validator for the ServiceListDto.
    /// Provides validation rules for the list service data transfer object.
    /// </summary>
    public class ServiceListDtoValidator : AbstractValidator<ServiceListDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceListDtoValidator"/> class
        /// and defines the validation rules.
        /// </summary>
        public ServiceListDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty).WithMessage("Service ID is required");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Service name is required")
                .MaximumLength(100).WithMessage("Service name must not exceed 100 characters");

            RuleFor(x => x.ServiceTypeId)
                .NotEqual(Guid.Empty).WithMessage("ServiceTypeId is required");
        }
    }
}