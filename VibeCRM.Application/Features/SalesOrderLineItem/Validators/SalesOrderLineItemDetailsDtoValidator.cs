using FluentValidation;
using VibeCRM.Shared.DTOs.SalesOrderLineItem;

namespace VibeCRM.Application.Features.SalesOrderLineItem.Validators
{
    /// <summary>
    /// Validator for the SalesOrderLineItemDetailsDto
    /// </summary>
    public class SalesOrderLineItemDetailsDtoValidator : AbstractValidator<SalesOrderLineItemDetailsDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SalesOrderLineItemDetailsDtoValidator"/> class
        /// </summary>
        public SalesOrderLineItemDetailsDtoValidator()
        {
            // Include all rules from the base validator
            Include(new SalesOrderLineItemDtoValidator());

            // CreatedBy is required
            RuleFor(x => x.CreatedBy)
                .NotEmpty()
                .WithMessage("Created By is required");

            // CreatedDate is required
            RuleFor(x => x.CreatedDate)
                .NotEmpty()
                .WithMessage("Created Date is required");

            // ModifiedBy is required
            RuleFor(x => x.ModifiedBy)
                .NotEmpty()
                .WithMessage("Modified By is required");

            // ModifiedDate is required
            RuleFor(x => x.ModifiedDate)
                .NotEmpty()
                .WithMessage("Modified Date is required");
        }
    }
}