using FluentValidation;

namespace VibeCRM.Application.Features.CallType.Queries.GetCallTypeByType
{
    /// <summary>
    /// Validator for the GetCallTypeByTypeQuery.
    /// Defines validation rules for retrieving call types by type name.
    /// </summary>
    public class GetCallTypeByTypeQueryValidator : AbstractValidator<GetCallTypeByTypeQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetCallTypeByTypeQueryValidator"/> class.
        /// Sets up validation rules for the GetCallTypeByTypeQuery properties.
        /// </summary>
        public GetCallTypeByTypeQueryValidator()
        {
            RuleFor(x => x.Type)
                .NotEmpty().WithMessage("Type is required.")
                .MaximumLength(100).WithMessage("Type must not exceed 100 characters.");
        }
    }
}