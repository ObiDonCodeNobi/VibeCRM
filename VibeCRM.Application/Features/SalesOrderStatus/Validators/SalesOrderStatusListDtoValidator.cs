using FluentValidation;
using VibeCRM.Application.Features.SalesOrderStatus.DTOs;

namespace VibeCRM.Application.Features.SalesOrderStatus.Validators
{
    /// <summary>
    /// Validator for the SalesOrderStatusListDto class.
    /// Defines validation rules for sales order status list data.
    /// </summary>
    public class SalesOrderStatusListDtoValidator : AbstractValidator<SalesOrderStatusListDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SalesOrderStatusListDtoValidator"/> class.
        /// Configures validation rules for SalesOrderStatusListDto properties.
        /// </summary>
        public SalesOrderStatusListDtoValidator()
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
        }
    }
}