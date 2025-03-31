using FluentValidation;
using VibeCRM.Shared.DTOs.SalesOrderStatus;

namespace VibeCRM.Application.Features.SalesOrderStatus.Validators
{
    /// <summary>
    /// Validator for the SalesOrderStatusDetailsDto class.
    /// Defines validation rules for detailed sales order status data.
    /// </summary>
    public class SalesOrderStatusDetailsDtoValidator : AbstractValidator<SalesOrderStatusDetailsDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SalesOrderStatusDetailsDtoValidator"/> class.
        /// Configures validation rules for SalesOrderStatusDetailsDto properties.
        /// </summary>
        public SalesOrderStatusDetailsDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Sales order status ID is required.");

            RuleFor(x => x.Status)
                .NotEmpty()
                .WithMessage("Status name is required.")
                .MaximumLength(50)
                .WithMessage("Status name cannot exceed 50 characters.");

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Description is required.")
                .MaximumLength(500)
                .WithMessage("Description cannot exceed 500 characters.");

            RuleFor(x => x.OrdinalPosition)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Ordinal position must be a non-negative number.");

            RuleFor(x => x.SalesOrderCount)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Sales order count must be a non-negative number.");

            RuleFor(x => x.CreatedDate)
                .NotEmpty()
                .WithMessage("Created date is required.");

            RuleFor(x => x.CreatedBy)
                .NotEmpty()
                .WithMessage("Created by is required.")
                .MaximumLength(100)
                .WithMessage("Created by cannot exceed 100 characters.");

            RuleFor(x => x.ModifiedDate)
                .NotEmpty()
                .WithMessage("Modified date is required.");

            RuleFor(x => x.ModifiedBy)
                .NotEmpty()
                .WithMessage("Modified by is required.")
                .MaximumLength(100)
                .WithMessage("Modified by cannot exceed 100 characters.");
        }
    }
}