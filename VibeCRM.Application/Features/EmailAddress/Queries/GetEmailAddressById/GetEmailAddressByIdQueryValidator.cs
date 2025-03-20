using FluentValidation;

namespace VibeCRM.Application.Features.EmailAddress.Queries.GetEmailAddressById
{
    /// <summary>
    /// Validator for the GetEmailAddressByIdQuery to ensure the query contains valid parameters.
    /// </summary>
    public class GetEmailAddressByIdQueryValidator : AbstractValidator<GetEmailAddressByIdQuery>
    {
        /// <summary>
        /// Initializes a new instance of the GetEmailAddressByIdQueryValidator class with validation rules.
        /// </summary>
        public GetEmailAddressByIdQueryValidator()
        {
            RuleFor(q => q.Id)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotEqual(Guid.Empty).WithMessage("{PropertyName} cannot be empty.");
        }
    }
}