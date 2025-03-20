using FluentValidation;

namespace VibeCRM.Application.Features.EmailAddress.Queries.GetEmailAddressesByType
{
    /// <summary>
    /// Validator for the GetEmailAddressesByTypeQuery to ensure the query contains valid parameters.
    /// </summary>
    public class GetEmailAddressesByTypeQueryValidator : AbstractValidator<GetEmailAddressesByTypeQuery>
    {
        /// <summary>
        /// Initializes a new instance of the GetEmailAddressesByTypeQueryValidator class with validation rules.
        /// </summary>
        public GetEmailAddressesByTypeQueryValidator()
        {
            RuleFor(q => q.EmailAddressTypeId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotEqual(Guid.Empty).WithMessage("{PropertyName} cannot be empty.");

            RuleFor(q => q.PageNumber)
                .GreaterThanOrEqualTo(1).WithMessage("{PropertyName} must be greater than or equal to 1.");

            RuleFor(q => q.PageSize)
                .GreaterThanOrEqualTo(1).WithMessage("{PropertyName} must be greater than or equal to 1.")
                .LessThanOrEqualTo(100).WithMessage("{PropertyName} must be less than or equal to 100.");
        }
    }
}