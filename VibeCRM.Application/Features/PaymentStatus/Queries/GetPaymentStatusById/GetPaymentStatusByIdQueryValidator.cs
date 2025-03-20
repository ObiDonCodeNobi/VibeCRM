using FluentValidation;

namespace VibeCRM.Application.Features.PaymentStatus.Queries.GetPaymentStatusById
{
    /// <summary>
    /// Validator for the GetPaymentStatusByIdQuery.
    /// Defines validation rules for payment status retrieval by ID queries.
    /// </summary>
    public class GetPaymentStatusByIdQueryValidator : AbstractValidator<GetPaymentStatusByIdQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetPaymentStatusByIdQueryValidator"/> class
        /// and configures the validation rules.
        /// </summary>
        public GetPaymentStatusByIdQueryValidator()
        {
            RuleFor(q => q.Id)
                .NotEmpty().WithMessage("Payment status ID is required.");
        }
    }
}
