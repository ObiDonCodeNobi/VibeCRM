using MediatR;

namespace VibeCRM.Application.Features.AccountType.Commands.DeleteAccountType
{
    /// <summary>
    /// Command to delete (soft delete) an account type in the system.
    /// </summary>
    public class DeleteAccountTypeCommand : IRequest<bool>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the account type to delete.
        /// </summary>
        public Guid Id { get; set; }
    }
}