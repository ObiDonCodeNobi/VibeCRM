using MediatR;
using VibeCRM.Application.Features.ActivityType.DTOs;

namespace VibeCRM.Application.Features.ActivityType.Commands.UpdateActivityType
{
    /// <summary>
    /// Command for updating an existing activity type.
    /// </summary>
    public class UpdateActivityTypeCommand : IRequest<ActivityTypeDto>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the activity type to update.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the type name (e.g., "Meeting", "Call", "Email").
        /// </summary>
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the description of the activity type.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the ordinal position for sorting activity types in listings and dropdowns.
        /// </summary>
        public int OrdinalPosition { get; set; }
    }
}