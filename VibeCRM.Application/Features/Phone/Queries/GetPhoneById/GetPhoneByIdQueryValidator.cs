using FluentValidation;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.Phone.Queries.GetPhoneById
{
    /// <summary>
    /// Validator for the GetPhoneByIdQuery
    /// </summary>
    public class GetPhoneByIdQueryValidator : AbstractValidator<GetPhoneByIdQuery>
    {
        private readonly IPhoneRepository _phoneRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetPhoneByIdQueryValidator"/> class
        /// </summary>
        /// <param name="phoneRepository">The phone repository for validating phone existence</param>
        public GetPhoneByIdQueryValidator(IPhoneRepository phoneRepository)
        {
            _phoneRepository = phoneRepository;

            RuleFor(p => p.Id)
                .NotEmpty().WithMessage("Phone ID is required")
                .MustAsync(async (id, cancellation) =>
                    await _phoneRepository.ExistsAsync(id, cancellation))
                .WithMessage("The specified phone does not exist");
        }
    }
}