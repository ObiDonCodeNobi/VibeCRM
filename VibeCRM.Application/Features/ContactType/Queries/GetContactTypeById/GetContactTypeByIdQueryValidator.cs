using FluentValidation;

namespace VibeCRM.Application.Features.ContactType.Queries.GetContactTypeById
{
    /// <summary>
    /// Validator for the <see cref="GetContactTypeByIdQuery"/> class.
    /// </summary>
    public class GetContactTypeByIdQueryValidator : AbstractValidator<GetContactTypeByIdQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetContactTypeByIdQueryValidator"/> class.
        /// </summary>
        public GetContactTypeByIdQueryValidator()
        {
            RuleFor(q => q.Id)
                .NotEmpty().WithMessage("Contact type ID is required.");
        }
    }
}