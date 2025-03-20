using MediatR;

namespace VibeCRM.Application.Features.PersonStatus.Commands.UpdatePersonStatus
{
    /// <summary>
    /// Command for updating an existing person status.
    /// Implements the CQRS command pattern for updating a person status entity.
    /// </summary>
    public class UpdatePersonStatusCommand : IRequest<bool>
    {
        /// <summary>
        /// Gets or sets the unique identifier for the person status to update.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the status name (e.g., "Active", "Inactive", "Prospect").
        /// </summary>
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the description of the person status.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the ordinal position for sorting person statuses in listings and dropdowns.
        /// </summary>
        public int OrdinalPosition { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user who last modified the person status.
        /// </summary>
        public string ModifiedBy { get; set; } = string.Empty;
    }
}