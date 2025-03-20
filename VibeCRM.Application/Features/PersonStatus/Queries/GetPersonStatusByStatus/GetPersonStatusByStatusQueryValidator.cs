using FluentValidation;

namespace VibeCRM.Application.Features.PersonStatus.Queries.GetPersonStatusByStatus
{
    /// <summary>
    /// Validator for the GetPersonStatusByStatusQuery.
    /// Defines validation rules for retrieving person status by status name queries.
    /// </summary>
    public class GetPersonStatusByStatusQueryValidator : AbstractValidator<GetPersonStatusByStatusQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetPersonStatusByStatusQueryValidator"/> class
        /// and configures the validation rules.
        /// </summary>
        public GetPersonStatusByStatusQueryValidator()
        {
            RuleFor(q => q.Status)
                .NotEmpty().WithMessage("Status name is required.")
                .MaximumLength(100).WithMessage("Status name must not exceed 100 characters.");
        }
    }
}