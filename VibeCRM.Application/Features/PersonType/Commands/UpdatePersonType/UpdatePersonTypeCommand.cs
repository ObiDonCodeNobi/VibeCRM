using MediatR;
using System;

namespace VibeCRM.Application.Features.PersonType.Commands.UpdatePersonType
{
    /// <summary>
    /// Command for updating an existing person type.
    /// Implements the CQRS command pattern for updating a person type entity.
    /// </summary>
    public class UpdatePersonTypeCommand : IRequest<bool>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the person type to update.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the type name (e.g., "Customer", "Vendor", "Employee").
        /// </summary>
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the description of the person type.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the ordinal position for sorting person types in listings and dropdowns.
        /// </summary>
        public int OrdinalPosition { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user who last modified the person type.
        /// </summary>
        public string ModifiedBy { get; set; } = string.Empty;
    }
}
