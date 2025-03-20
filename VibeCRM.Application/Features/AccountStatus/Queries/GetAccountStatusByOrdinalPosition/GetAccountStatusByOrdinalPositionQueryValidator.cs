using FluentValidation;

namespace VibeCRM.Application.Features.AccountStatus.Queries.GetAccountStatusByOrdinalPosition
{
    /// <summary>
    /// Validator for the GetAccountStatusByOrdinalPositionQuery.
    /// Defines validation rules for account status retrieval by ordinal position queries.
    /// </summary>
    public class GetAccountStatusByOrdinalPositionQueryValidator : AbstractValidator<GetAccountStatusByOrdinalPositionQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetAccountStatusByOrdinalPositionQueryValidator"/> class
        /// and configures the validation rules.
        /// </summary>
        public GetAccountStatusByOrdinalPositionQueryValidator()
        {
            RuleFor(q => q.OrdinalPosition)
                .GreaterThanOrEqualTo(0).WithMessage("Ordinal position must be a non-negative number.");
        }
    }
}