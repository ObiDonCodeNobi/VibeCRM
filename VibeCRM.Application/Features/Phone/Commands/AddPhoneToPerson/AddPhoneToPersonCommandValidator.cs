using FluentValidation;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.Phone.Commands.AddPhoneToPerson
{
    /// <summary>
    /// Validator for the AddPhoneToPersonCommand
    /// </summary>
    public class AddPhoneToPersonCommandValidator : AbstractValidator<AddPhoneToPersonCommand>
    {
        private readonly IPhoneRepository _phoneRepository;
        private readonly IPersonRepository _personRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddPhoneToPersonCommandValidator"/> class
        /// </summary>
        /// <param name="phoneRepository">The phone repository for validating phone existence</param>
        /// <param name="personRepository">The person repository for validating person existence</param>
        public AddPhoneToPersonCommandValidator(IPhoneRepository phoneRepository, IPersonRepository personRepository)
        {
            _phoneRepository = phoneRepository;
            _personRepository = personRepository;

            RuleFor(p => p.PhoneId)
                .NotEmpty().WithMessage("Phone ID is required")
                .MustAsync(async (id, cancellation) =>
                    await _phoneRepository.ExistsAsync(id, cancellation))
                .WithMessage("The specified phone does not exist");

            RuleFor(p => p.PersonId)
                .NotEmpty().WithMessage("Person ID is required")
                .MustAsync(async (id, cancellation) =>
                    await _personRepository.ExistsAsync(id, cancellation))
                .WithMessage("The specified person does not exist");

            RuleFor(p => p)
                .MustAsync(async (command, cancellation) =>
                    !await _phoneRepository.IsPhoneAssociatedWithPersonAsync(command.PhoneId, command.PersonId, cancellation))
                .WithMessage("This phone is already associated with the specified person");
        }
    }
}