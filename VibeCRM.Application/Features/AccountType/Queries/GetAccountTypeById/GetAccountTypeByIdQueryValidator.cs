using FluentValidation;

namespace VibeCRM.Application.Features.AccountType.Queries.GetAccountTypeById
{
    /// <summary>
    /// Validator for the GetAccountTypeByIdQuery class.
    /// Defines validation rules for retrieving an account type by ID.
    /// </summary>
    public class GetAccountTypeByIdQueryValidator : AbstractValidator<GetAccountTypeByIdQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetAccountTypeByIdQueryValidator"/> class.
        /// Configures validation rules for GetAccountTypeByIdQuery properties.
        /// </summary>
        public GetAccountTypeByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty).WithMessage("Account type ID is required.");
        }
    }
}