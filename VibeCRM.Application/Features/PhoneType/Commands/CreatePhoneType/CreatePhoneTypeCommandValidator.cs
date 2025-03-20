using FluentValidation;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.PhoneType.Commands.CreatePhoneType
{
    /// <summary>
    /// Validator for the CreatePhoneTypeCommand class.
    /// Defines validation rules for creating a new phone type.
    /// </summary>
    public class CreatePhoneTypeCommandValidator : AbstractValidator<CreatePhoneTypeCommand>
    {
        private readonly IPhoneTypeRepository _phoneTypeRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreatePhoneTypeCommandValidator"/> class.
        /// Configures validation rules for CreatePhoneTypeCommand properties.
        /// </summary>
        /// <param name="phoneTypeRepository">The phone type repository for validating uniqueness.</param>
        public CreatePhoneTypeCommandValidator(IPhoneTypeRepository phoneTypeRepository)
        {
            _phoneTypeRepository = phoneTypeRepository ?? throw new ArgumentNullException(nameof(phoneTypeRepository));

            RuleFor(x => x.Type)
                .NotEmpty()
                .WithMessage("Phone type name is required.")
                .MaximumLength(50)
                .WithMessage("Phone type name cannot exceed 50 characters.")
                .MustAsync(async (type, cancellation) =>
                {
                    var existingTypes = await _phoneTypeRepository.GetByTypeAsync(type, cancellation);
                    return !existingTypes.Any();
                })
                .WithMessage("A phone type with this name already exists.");

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
