using FluentValidation;

namespace VibeCRM.Application.Features.AccountType.Queries.GetAccountTypeByType
{
    /// <summary>
    /// Validator for the GetAccountTypeByTypeQuery class.
    /// Defines validation rules for retrieving account types by type name.
    /// </summary>
    public class GetAccountTypeByTypeQueryValidator : AbstractValidator<GetAccountTypeByTypeQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetAccountTypeByTypeQueryValidator"/> class.
        /// Configures validation rules for GetAccountTypeByTypeQuery properties.
        /// </summary>
        public GetAccountTypeByTypeQueryValidator()
        {
            RuleFor(x => x.Type)
                .NotEmpty().WithMessage("Type name is required.")
                .MaximumLength(100).WithMessage("Type name cannot exceed 100 characters.");
        }
    }
}