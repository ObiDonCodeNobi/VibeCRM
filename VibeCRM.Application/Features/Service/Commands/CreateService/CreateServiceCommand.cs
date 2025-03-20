using MediatR;
using VibeCRM.Application.Features.Service.DTOs;

namespace VibeCRM.Application.Features.Service.Commands.CreateService
{
    /// <summary>
    /// Command to create a new service in the system.
    /// This is used in the CQRS pattern as the request object for service creation.
    /// </summary>
    public class CreateServiceCommand : IRequest<ServiceDetailsDto>
    {
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
        /// Gets or sets the identifier of the user creating this service.
        /// </summary>
        public Guid CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user modifying this service.
        /// </summary>
        public Guid ModifiedBy { get; set; }
    }
}