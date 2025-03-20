using FluentValidation;

namespace VibeCRM.Application.Features.Address.Queries.GetAllAddresses
{
    /// <summary>
    /// Validator for the GetAllAddressesQuery
    /// </summary>
    public class GetAllAddressesQueryValidator : AbstractValidator<GetAllAddressesQuery>
    {
        /// <summary>
        /// Initializes a new instance of the GetAllAddressesQueryValidator class
        /// </summary>
        public GetAllAddressesQueryValidator()
        {
            RuleFor(q => q.PageNumber)
                .GreaterThanOrEqualTo(1).WithMessage("{PropertyName} must be greater than or equal to 1.");

            RuleFor(q => q.PageSize)
                .GreaterThanOrEqualTo(1).WithMessage("{PropertyName} must be greater than or equal to 1.")
                .LessThanOrEqualTo(100).WithMessage("{PropertyName} must not exceed 100.");
        }
    }
}