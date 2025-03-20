using MediatR;
using System;

namespace VibeCRM.Application.Features.PersonType.Commands.CreatePersonType
{
    /// <summary>
    /// Command for creating a new person type.
    /// Implements the CQRS command pattern for creating a person type entity.
    /// </summary>
    public class CreatePersonTypeCommand : IRequest<Guid>
    {
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
        /// Gets or sets the identifier of the user who created the person type.
        /// </summary>
        public string CreatedBy { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the identifier of the user who last modified the person type.
        /// </summary>
        public string ModifiedBy { get; set; } = string.Empty;
    }
}
