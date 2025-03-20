using FluentValidation;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.PhoneType.Queries.GetPhoneTypeById
{
    /// <summary>
    /// Validator for the GetPhoneTypeByIdQuery class.
    /// Defines validation rules for retrieving a phone type by ID.
    /// </summary>
    public class GetPhoneTypeByIdQueryValidator : AbstractValidator<GetPhoneTypeByIdQuery>
    {
        private readonly IPhoneTypeRepository _phoneTypeRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetPhoneTypeByIdQueryValidator"/> class.
        /// Configures validation rules for GetPhoneTypeByIdQuery properties.
        /// </summary>
        /// <param name="phoneTypeRepository">The phone type repository for validating existence.</param>
        public GetPhoneTypeByIdQueryValidator(IPhoneTypeRepository phoneTypeRepository)
        {
            _phoneTypeRepository = phoneTypeRepository ?? throw new ArgumentNullException(nameof(phoneTypeRepository));

            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Phone type ID is required.")
                .MustAsync(async (id, cancellation) =>
                {
                    return await _phoneTypeRepository.ExistsAsync(id, cancellation);
                })
                .WithMessage("Phone type with the specified ID does not exist.");
        }
    }
}