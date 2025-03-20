using FluentValidation;

namespace VibeCRM.Application.Features.PhoneType.Queries.GetPhoneTypeByType
{
    /// <summary>
    /// Validator for the GetPhoneTypeByTypeQuery class.
    /// Defines validation rules for retrieving a phone type by type name.
    /// </summary>
    public class GetPhoneTypeByTypeQueryValidator : AbstractValidator<GetPhoneTypeByTypeQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetPhoneTypeByTypeQueryValidator"/> class.
        /// Configures validation rules for GetPhoneTypeByTypeQuery properties.
        /// </summary>
        public GetPhoneTypeByTypeQueryValidator()
        {
            RuleFor(x => x.Type)
                .NotEmpty()
                .WithMessage("Phone type name is required.")
                .MaximumLength(50)
                .WithMessage("Phone type name cannot exceed 50 characters.");
        }
    }
}