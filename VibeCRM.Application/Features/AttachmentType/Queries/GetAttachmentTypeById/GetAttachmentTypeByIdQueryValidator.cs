using FluentValidation;

namespace VibeCRM.Application.Features.AttachmentType.Queries.GetAttachmentTypeById
{
    /// <summary>
    /// Validator for the GetAttachmentTypeByIdQuery.
    /// Defines validation rules for retrieving an attachment type by ID.
    /// </summary>
    public class GetAttachmentTypeByIdQueryValidator : AbstractValidator<GetAttachmentTypeByIdQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetAttachmentTypeByIdQueryValidator"/> class.
        /// Sets up validation rules for the GetAttachmentTypeByIdQuery properties.
        /// </summary>
        public GetAttachmentTypeByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Attachment type ID is required.");
        }
    }
}