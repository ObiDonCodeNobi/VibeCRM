using MediatR;
using VibeCRM.Application.Features.Service.DTOs;

namespace VibeCRM.Application.Features.Service.Commands.UpdateService
{
    /// <summary>
    /// Command to update an existing service in the system.
    /// This is used in the CQRS pattern as the request object for service updates.
    /// </summary>
    public class UpdateServiceCommand : IRequest<ServiceDetailsDto>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the service to update.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the service type.
        /// </summary>
        public Guid ServiceTypeId { get; set; }

        /// <summary>
        /// Gets or sets the name of the service.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the description of the service.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user modifying this service.
        /// </summary>
        public Guid ModifiedBy { get; set; }

        /// <summary>
        /// Gets or sets whether this service is active (not soft-deleted).
        /// </summary>
        public bool Active { get; set; } = true;
    }
}