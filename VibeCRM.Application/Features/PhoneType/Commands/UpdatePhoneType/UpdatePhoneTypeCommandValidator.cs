using FluentValidation;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.PhoneType.Commands.UpdatePhoneType
{
    /// <summary>
    /// Validator for the UpdatePhoneTypeCommand class.
    /// Defines validation rules for updating an existing phone type.
    /// </summary>
    public class UpdatePhoneTypeCommandValidator : AbstractValidator<UpdatePhoneTypeCommand>
    {
        private readonly IPhoneTypeRepository _phoneTypeRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdatePhoneTypeCommandValidator"/> class.
        /// Configures validation rules for UpdatePhoneTypeCommand properties.
        /// </summary>
        /// <param name="phoneTypeRepository">The phone type repository for validating existence and uniqueness.</param>
        public UpdatePhoneTypeCommandValidator(IPhoneTypeRepository phoneTypeRepository)
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

            RuleFor(x => x.Type)
                .NotEmpty()
                .WithMessage("Phone type name is required.")
                .MaximumLength(50)
                .WithMessage("Phone type name cannot exceed 50 characters.")
                .MustAsync(async (command, type, cancellation) => 
                {
                    var existingTypes = await _phoneTypeRepository.GetByTypeAsync(type, cancellation);
                    return !existingTypes.Any(pt => pt.Id != command.Id);
                })
                .WithMessage("Another phone type with this name already exists.");

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Description is required.")
                .MaximumLength(500)
                .WithMessage("Description cannot exceed 500 characters.");

            RuleFor(x => x.OrdinalPosition)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Ordinal position must be a non-negative number.");
        }
    }
}
