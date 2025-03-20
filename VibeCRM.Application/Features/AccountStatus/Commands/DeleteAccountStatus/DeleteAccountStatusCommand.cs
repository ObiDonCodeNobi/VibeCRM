using MediatR;

namespace VibeCRM.Application.Features.AccountStatus.Commands.DeleteAccountStatus
{
    /// <summary>
    /// Command for deleting (soft delete) an account status in the system.
    /// Implements the CQRS command pattern for account status deletion.
    /// </summary>
    public class DeleteAccountStatusCommand : IRequest<bool>
    {
        /// <summary>
        /// Gets or sets the unique identifier for the account status to delete.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the ID of the user who is performing the delete operation.
        /// </summary>
        public Guid ModifiedBy { get; set; }
    }
}