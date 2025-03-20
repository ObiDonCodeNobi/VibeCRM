using FluentValidation;

namespace VibeCRM.Application.Features.EmailAddressType.Queries.GetEmailAddressTypeById
{
    /// <summary>
    /// Validator for the GetEmailAddressTypeByIdQuery.
    /// </summary>
    public class GetEmailAddressTypeByIdQueryValidator : AbstractValidator<GetEmailAddressTypeByIdQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetEmailAddressTypeByIdQueryValidator"/> class.
        /// </summary>
        public GetEmailAddressTypeByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Email address type ID is required.")
                .NotEqual(Guid.Empty)
                .WithMessage("Email address type ID cannot be empty.");
        }
    }
}