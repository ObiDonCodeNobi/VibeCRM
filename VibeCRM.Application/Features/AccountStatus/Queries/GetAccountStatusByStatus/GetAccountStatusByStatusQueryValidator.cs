using FluentValidation;

namespace VibeCRM.Application.Features.AccountStatus.Queries.GetAccountStatusByStatus
{
    /// <summary>
    /// Validator for the GetAccountStatusByStatusQuery.
    /// Defines validation rules for account status retrieval by status name queries.
    /// </summary>
    public class GetAccountStatusByStatusQueryValidator : AbstractValidator<GetAccountStatusByStatusQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetAccountStatusByStatusQueryValidator"/> class
        /// and configures the validation rules.
        /// </summary>
        public GetAccountStatusByStatusQueryValidator()
        {
            RuleFor(q => q.Status)
                .NotEmpty().WithMessage("Status name is required.")
                .MaximumLength(100).WithMessage("Status name must not exceed 100 characters.");
        }
    }
}