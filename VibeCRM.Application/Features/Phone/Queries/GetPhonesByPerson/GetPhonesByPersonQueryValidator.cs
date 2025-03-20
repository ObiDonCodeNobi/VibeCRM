using FluentValidation;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.Phone.Queries.GetPhonesByPerson
{
    /// <summary>
    /// Validator for the GetPhonesByPersonQuery
    /// </summary>
    public class GetPhonesByPersonQueryValidator : AbstractValidator<GetPhonesByPersonQuery>
    {
        private readonly IPersonRepository _personRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetPhonesByPersonQueryValidator"/> class
        /// </summary>
        /// <param name="personRepository">The person repository for validating person existence</param>
        public GetPhonesByPersonQueryValidator(IPersonRepository personRepository)
        {
            _personRepository = personRepository;

            RuleFor(p => p.PersonId)
                .NotEmpty().WithMessage("Person ID is required")
                .MustAsync(async (id, cancellation) =>
                    await _personRepository.ExistsAsync(id, cancellation))
                .WithMessage("The specified person does not exist");
        }
    }
}