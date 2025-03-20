using FluentValidation;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.Phone.Queries.GetPhonesByPhoneType
{
    /// <summary>
    /// Validator for the GetPhonesByPhoneTypeQuery
    /// </summary>
    public class GetPhonesByPhoneTypeQueryValidator : AbstractValidator<GetPhonesByPhoneTypeQuery>
    {
        private readonly IPhoneTypeRepository _phoneTypeRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetPhonesByPhoneTypeQueryValidator"/> class
        /// </summary>
        /// <param name="phoneTypeRepository">The phone type repository for validating phone type existence</param>
        public GetPhonesByPhoneTypeQueryValidator(IPhoneTypeRepository phoneTypeRepository)
        {
            _phoneTypeRepository = phoneTypeRepository;

            RuleFor(p => p.PhoneTypeId)
                .NotEmpty().WithMessage("Phone type ID is required")
                .MustAsync(async (id, cancellation) =>
                    await _phoneTypeRepository.ExistsAsync(id, cancellation))
                .WithMessage("The specified phone type does not exist");
        }
    }
}