using FluentValidation;

namespace VibeCRM.Application.Features.ProductGroup.Queries.GetProductGroupsByParentId
{
    /// <summary>
    /// Validator for the <see cref="GetProductGroupsByParentIdQuery"/> class.
    /// Defines validation rules for retrieving product groups by parent ID.
    /// </summary>
    public class GetProductGroupsByParentIdQueryValidator : AbstractValidator<GetProductGroupsByParentIdQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetProductGroupsByParentIdQueryValidator"/> class.
        /// Sets up validation rules for retrieving product groups by parent ID.
        /// </summary>
        public GetProductGroupsByParentIdQueryValidator()
        {
            RuleFor(x => x.ParentId)
                .NotEmpty().WithMessage("Parent product group ID is required.")
                .NotEqual(Guid.Empty).WithMessage("A valid parent product group ID must be provided.");
        }
    }
}