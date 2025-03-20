using FluentValidation;

namespace VibeCRM.Application.Features.EmailAddressType.Queries.GetEmailAddressTypeByType
{
    /// <summary>
    /// Validator for the GetEmailAddressTypeByTypeQuery.
    /// </summary>
    public class GetEmailAddressTypeByTypeQueryValidator : AbstractValidator<GetEmailAddressTypeByTypeQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetEmailAddressTypeByTypeQueryValidator"/> class.
        /// </summary>
        public GetEmailAddressTypeByTypeQueryValidator()
        {
            RuleFor(x => x.Type)
                .NotEmpty()
                .WithMessage("Email address type name is required.")
                .MaximumLength(100)
                .WithMessage("Email address type name cannot exceed 100 characters.");
        }
    }
}