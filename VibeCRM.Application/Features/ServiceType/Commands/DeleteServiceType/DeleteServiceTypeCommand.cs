using System;
using MediatR;

namespace VibeCRM.Application.Features.ServiceType.Commands.DeleteServiceType
{
    /// <summary>
    /// Command for deleting an existing service type.
    /// </summary>
    public class DeleteServiceTypeCommand : IRequest<bool>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the service type to delete.
        /// </summary>
        public Guid Id { get; set; }
    }
}
