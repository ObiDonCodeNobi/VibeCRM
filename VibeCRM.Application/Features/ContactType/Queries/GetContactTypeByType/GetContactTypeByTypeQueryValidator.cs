using FluentValidation;

namespace VibeCRM.Application.Features.ContactType.Queries.GetContactTypeByType
{
    /// <summary>
    /// Validator for the <see cref="GetContactTypeByTypeQuery"/> class.
    /// </summary>
    public class GetContactTypeByTypeQueryValidator : AbstractValidator<GetContactTypeByTypeQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetContactTypeByTypeQueryValidator"/> class.
        /// </summary>
        public GetContactTypeByTypeQueryValidator()
        {
            RuleFor(q => q.Type)
                .NotEmpty().WithMessage("Contact type name is required.")
                .MaximumLength(100).WithMessage("Contact type name cannot exceed 100 characters.");
        }
    }
}