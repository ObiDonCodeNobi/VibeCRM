using FluentValidation;

namespace VibeCRM.Application.Features.EmailAddress.Queries.GetAllEmailAddresses
{
    /// <summary>
    /// Validator for the GetAllEmailAddressesQuery to ensure the query contains valid pagination parameters.
    /// </summary>
    public class GetAllEmailAddressesQueryValidator : AbstractValidator<GetAllEmailAddressesQuery>
    {
        /// <summary>
        /// Initializes a new instance of the GetAllEmailAddressesQueryValidator class with validation rules.
        /// </summary>
        public GetAllEmailAddressesQueryValidator()
        {
            RuleFor(q => q.PageNumber)
                .GreaterThanOrEqualTo(1).WithMessage("{PropertyName} must be greater than or equal to 1.");

            RuleFor(q => q.PageSize)
                .GreaterThanOrEqualTo(1).WithMessage("{PropertyName} must be greater than or equal to 1.")
                .LessThanOrEqualTo(100).WithMessage("{PropertyName} must not exceed 100.");
        }
    }
}