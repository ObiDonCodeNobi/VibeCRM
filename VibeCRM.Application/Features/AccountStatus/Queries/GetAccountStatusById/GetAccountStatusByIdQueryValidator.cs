using FluentValidation;

namespace VibeCRM.Application.Features.AccountStatus.Queries.GetAccountStatusById
{
    /// <summary>
    /// Validator for the GetAccountStatusByIdQuery.
    /// Defines validation rules for account status retrieval by ID queries.
    /// </summary>
    public class GetAccountStatusByIdQueryValidator : AbstractValidator<GetAccountStatusByIdQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetAccountStatusByIdQueryValidator"/> class
        /// and configures the validation rules.
        /// </summary>
        public GetAccountStatusByIdQueryValidator()
        {
            RuleFor(q => q.Id)
                .NotEmpty().WithMessage("Account status ID is required.");
        }
    }
}