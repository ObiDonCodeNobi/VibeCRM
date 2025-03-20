using FluentValidation;

namespace VibeCRM.Application.Features.CallType.Queries.GetCallTypeById
{
    /// <summary>
    /// Validator for the GetCallTypeByIdQuery.
    /// Defines validation rules for retrieving a call type by ID.
    /// </summary>
    public class GetCallTypeByIdQueryValidator : AbstractValidator<GetCallTypeByIdQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetCallTypeByIdQueryValidator"/> class.
        /// Sets up validation rules for the GetCallTypeByIdQuery properties.
        /// </summary>
        public GetCallTypeByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required.");
        }
    }
}