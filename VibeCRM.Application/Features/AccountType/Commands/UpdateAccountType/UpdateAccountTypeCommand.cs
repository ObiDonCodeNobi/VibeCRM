using MediatR;

namespace VibeCRM.Application.Features.AccountType.Commands.UpdateAccountType
{
    /// <summary>
    /// Command to update an existing account type in the system.
    /// </summary>
    public class UpdateAccountTypeCommand : IRequest<bool>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the account type to update.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the type name (e.g., "Customer", "Vendor", "Partner").
        /// </summary>
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the description of the account type.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the ordinal position for sorting account types in listings and dropdowns.
        /// </summary>
        public int OrdinalPosition { get; set; }
    }
}