using FluentValidation;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.PhoneType.Commands.DeletePhoneType
{
    /// <summary>
    /// Validator for the DeletePhoneTypeCommand class.
    /// Defines validation rules for deleting an existing phone type.
    /// </summary>
    public class DeletePhoneTypeCommandValidator : AbstractValidator<DeletePhoneTypeCommand>
    {
        private readonly IPhoneTypeRepository _phoneTypeRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeletePhoneTypeCommandValidator"/> class.
        /// Configures validation rules for DeletePhoneTypeCommand properties.
        /// </summary>
        /// <param name="phoneTypeRepository">The phone type repository for validating existence.</param>
        public DeletePhoneTypeCommandValidator(IPhoneTypeRepository phoneTypeRepository)
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
