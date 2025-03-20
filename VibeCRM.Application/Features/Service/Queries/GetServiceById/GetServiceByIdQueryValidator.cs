using FluentValidation;

namespace VibeCRM.Application.Features.Service.Queries.GetServiceById
{
    /// <summary>
    /// Validator for the GetServiceByIdQuery
    /// </summary>
    public class GetServiceByIdQueryValidator : AbstractValidator<GetServiceByIdQuery>
    {
        /// <summary>
        /// Initializes a new instance of the GetServiceByIdQueryValidator class
        /// </summary>
        public GetServiceByIdQueryValidator()
        {
            RuleFor(q => q.Id)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotEqual(Guid.Empty).WithMessage("{PropertyName} cannot be empty.");
        }
    }
}