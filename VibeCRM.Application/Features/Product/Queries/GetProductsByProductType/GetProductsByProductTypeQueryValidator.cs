using FluentValidation;
using System;

namespace VibeCRM.Application.Features.Product.Queries.GetProductsByProductType
{
    /// <summary>
    /// Validator for the GetProductsByProductTypeQuery class
    /// </summary>
    public class GetProductsByProductTypeQueryValidator : AbstractValidator<GetProductsByProductTypeQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetProductsByProductTypeQueryValidator"/> class with validation rules.
        /// </summary>
        public GetProductsByProductTypeQueryValidator()
        {
            RuleFor(q => q.ProductTypeId)
                .NotEmpty().WithMessage("Product Type ID is required.")
                .Must(id => id != Guid.Empty).WithMessage("Product Type ID cannot be empty.");
        }
    }
}
