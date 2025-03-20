using FluentValidation;

namespace VibeCRM.Application.Features.Address.Queries.GetAddressById
{
    /// <summary>
    /// Validator for the GetAddressByIdQuery
    /// </summary>
    public class GetAddressByIdQueryValidator : AbstractValidator<GetAddressByIdQuery>
    {
        /// <summary>
        /// Initializes a new instance of the GetAddressByIdQueryValidator class
        /// </summary>
        public GetAddressByIdQueryValidator()
        {
            RuleFor(q => q.Id)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotEqual(Guid.Empty).WithMessage("{PropertyName} cannot be empty.");
        }
    }
}