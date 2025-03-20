using FluentValidation;

namespace VibeCRM.Application.Features.AddressType.Queries.GetAddressTypeById
{
    /// <summary>
    /// Validator for the GetAddressTypeByIdQuery class.
    /// Defines validation rules for retrieving an address type by ID.
    /// </summary>
    public class GetAddressTypeByIdQueryValidator : AbstractValidator<GetAddressTypeByIdQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetAddressTypeByIdQueryValidator"/> class.
        /// Configures validation rules for GetAddressTypeByIdQuery properties.
        /// </summary>
        public GetAddressTypeByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Address type ID is required.");
        }
    }
}