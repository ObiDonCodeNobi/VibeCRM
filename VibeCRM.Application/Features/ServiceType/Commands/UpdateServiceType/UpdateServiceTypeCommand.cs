using System;
using MediatR;
using VibeCRM.Application.Features.ServiceType.DTOs;

namespace VibeCRM.Application.Features.ServiceType.Commands.UpdateServiceType
{
    /// <summary>
    /// Command for updating an existing service type.
    /// </summary>
    public class UpdateServiceTypeCommand : IRequest<ServiceTypeDto>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the service type to update.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the type name (e.g., "Consulting", "Implementation", "Training").
        /// </summary>
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the detailed description of the service type with information about
        /// the purpose and features of services in this category.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the ordinal position for sorting service types in listings and dropdowns.
        /// </summary>
        public int OrdinalPosition { get; set; }
    }
}
