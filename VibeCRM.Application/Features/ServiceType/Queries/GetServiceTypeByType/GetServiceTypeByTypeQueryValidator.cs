using FluentValidation;

namespace VibeCRM.Application.Features.ServiceType.Queries.GetServiceTypeByType
{
    /// <summary>
    /// Validator for the GetServiceTypeByTypeQuery.
    /// Defines validation rules for retrieving service types by type name.
    /// </summary>
    public class GetServiceTypeByTypeQueryValidator : AbstractValidator<GetServiceTypeByTypeQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetServiceTypeByTypeQueryValidator"/> class.
        /// Configures validation rules for the GetServiceTypeByTypeQuery properties.
        /// </summary>
        public GetServiceTypeByTypeQueryValidator()
        {
            RuleFor(x => x.Type)
                .NotEmpty()
                .WithMessage("Type name to search for is required.")
                .MaximumLength(50)
                .WithMessage("Type name cannot exceed 50 characters.");
        }
    }
}