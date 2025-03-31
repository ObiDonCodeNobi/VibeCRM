using MediatR;
using VibeCRM.Shared.DTOs.ServiceType;

namespace VibeCRM.Application.Features.ServiceType.Commands.CreateServiceType
{
    /// <summary>
    /// Command for creating a new service type.
    /// </summary>
    public class CreateServiceTypeCommand : IRequest<ServiceTypeDto>
    {
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