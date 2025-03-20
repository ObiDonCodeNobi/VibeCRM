using FluentValidation;

namespace VibeCRM.Application.Features.AttachmentType.Queries.GetAttachmentTypeByType
{
    /// <summary>
    /// Validator for the GetAttachmentTypeByTypeQuery.
    /// Defines validation rules for retrieving an attachment type by its type name.
    /// </summary>
    public class GetAttachmentTypeByTypeQueryValidator : AbstractValidator<GetAttachmentTypeByTypeQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetAttachmentTypeByTypeQueryValidator"/> class.
        /// Sets up validation rules for the GetAttachmentTypeByTypeQuery properties.
        /// </summary>
        public GetAttachmentTypeByTypeQueryValidator()
        {
            RuleFor(x => x.Type)
                .NotEmpty().WithMessage("Type name is required.")
                .MaximumLength(100).WithMessage("Type name must not exceed 100 characters.");
        }
    }
}