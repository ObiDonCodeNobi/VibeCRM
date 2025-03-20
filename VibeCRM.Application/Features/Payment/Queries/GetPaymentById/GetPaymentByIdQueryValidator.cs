using FluentValidation;

namespace VibeCRM.Application.Features.Payment.Queries.GetPaymentById
{
    /// <summary>
    /// Validator for the GetPaymentByIdQuery to ensure the query contains valid parameters.
    /// </summary>
    public class GetPaymentByIdQueryValidator : AbstractValidator<GetPaymentByIdQuery>
    {
        /// <summary>
        /// Initializes a new instance of the GetPaymentByIdQueryValidator class with validation rules.
        /// </summary>
        public GetPaymentByIdQueryValidator()
        {
            RuleFor(q => q.Id)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotEqual(Guid.Empty).WithMessage("{PropertyName} cannot be empty.");
        }
    }
}