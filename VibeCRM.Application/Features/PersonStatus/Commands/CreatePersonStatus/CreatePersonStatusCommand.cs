using MediatR;

namespace VibeCRM.Application.Features.PersonStatus.Commands.CreatePersonStatus
{
    /// <summary>
    /// Command for creating a new person status.
    /// Implements the CQRS command pattern for creating a person status entity.
    /// </summary>
    public class CreatePersonStatusCommand : IRequest<Guid>
    {
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
        /// Gets or sets the identifier of the user who created the person status.
        /// </summary>
        public string CreatedBy { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the identifier of the user who last modified the person status.
        /// </summary>
        public string ModifiedBy { get; set; } = string.Empty;
    }
}