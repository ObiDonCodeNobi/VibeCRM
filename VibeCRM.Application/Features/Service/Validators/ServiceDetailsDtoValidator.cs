using FluentValidation;
using VibeCRM.Application.Features.Service.DTOs;

namespace VibeCRM.Application.Features.Service.Validators
{
    /// <summary>
    /// Validator for the ServiceDetailsDto.
    /// Provides validation rules for the detailed service data transfer object.
    /// </summary>
    public class ServiceDetailsDtoValidator : AbstractValidator<ServiceDetailsDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceDetailsDtoValidator"/> class
        /// and defines the validation rules.
        /// </summary>
        public ServiceDetailsDtoValidator()
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

            RuleFor(x => x.CreatedBy)
                .NotEqual(Guid.Empty).WithMessage("CreatedBy is required");

            RuleFor(x => x.CreatedDate)
                .NotEmpty().WithMessage("CreatedDate is required");

            RuleFor(x => x.ModifiedBy)
                .NotEqual(Guid.Empty).WithMessage("ModifiedBy is required");

            RuleFor(x => x.ModifiedDate)
                .NotEmpty().WithMessage("ModifiedDate is required");
        }
    }
}