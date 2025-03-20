using FluentValidation;

namespace VibeCRM.Application.Features.ProductGroup.Queries.GetProductGroupById
{
    /// <summary>
    /// Validator for the <see cref="GetProductGroupByIdQuery"/> class.
    /// Defines validation rules for retrieving a product group by ID.
    /// </summary>
    public class GetProductGroupByIdQueryValidator : AbstractValidator<GetProductGroupByIdQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetProductGroupByIdQueryValidator"/> class.
        /// Sets up validation rules for retrieving a product group by ID.
        /// </summary>
        public GetProductGroupByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Product group ID is required.")
                .NotEqual(Guid.Empty).WithMessage("A valid product group ID must be provided.");
        }
    }
}