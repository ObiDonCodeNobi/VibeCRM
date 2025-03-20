using FluentValidation;

namespace VibeCRM.Application.Features.PaymentStatus.Queries.GetPaymentStatusByStatus
{
    /// <summary>
    /// Validator for the GetPaymentStatusByStatusQuery.
    /// Defines validation rules for payment status retrieval by status name queries.
    /// </summary>
    public class GetPaymentStatusByStatusQueryValidator : AbstractValidator<GetPaymentStatusByStatusQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetPaymentStatusByStatusQueryValidator"/> class
        /// and configures the validation rules.
        /// </summary>
        public GetPaymentStatusByStatusQueryValidator()
        {
            RuleFor(q => q.Status)
                .NotEmpty().WithMessage("Status name is required.")
                .MaximumLength(100).WithMessage("Status name must not exceed 100 characters.");
        }
    }
}