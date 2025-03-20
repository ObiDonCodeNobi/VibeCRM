using FluentValidation;

namespace VibeCRM.Application.Features.AddressType.Queries.GetAddressTypeByType
{
    /// <summary>
    /// Validator for the GetAddressTypeByTypeQuery class.
    /// Defines validation rules for retrieving an address type by its type name.
    /// </summary>
    public class GetAddressTypeByTypeQueryValidator : AbstractValidator<GetAddressTypeByTypeQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetAddressTypeByTypeQueryValidator"/> class.
        /// Configures validation rules for GetAddressTypeByTypeQuery properties.
        /// </summary>
        public GetAddressTypeByTypeQueryValidator()
        {
            RuleFor(x => x.Type)
                .NotEmpty()
                .WithMessage("Address type name is required.")
                .MaximumLength(50)
                .WithMessage("Address type name cannot exceed 50 characters.");
        }
    }
}