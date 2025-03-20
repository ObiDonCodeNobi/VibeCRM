using FluentValidation;

namespace VibeCRM.Application.Features.Service.Queries.GetAllServices
{
    /// <summary>
    /// Validator for the GetAllServicesQuery
    /// </summary>
    public class GetAllServicesQueryValidator : AbstractValidator<GetAllServicesQuery>
    {
        /// <summary>
        /// Initializes a new instance of the GetAllServicesQueryValidator class
        /// </summary>
        public GetAllServicesQueryValidator()
        {
            RuleFor(q => q.PageNumber)
                .GreaterThanOrEqualTo(1).WithMessage("{PropertyName} must be greater than or equal to 1.");

            RuleFor(q => q.PageSize)
                .GreaterThanOrEqualTo(1).WithMessage("{PropertyName} must be greater than or equal to 1.")
                .LessThanOrEqualTo(100).WithMessage("{PropertyName} must not exceed 100.");
        }
    }
}