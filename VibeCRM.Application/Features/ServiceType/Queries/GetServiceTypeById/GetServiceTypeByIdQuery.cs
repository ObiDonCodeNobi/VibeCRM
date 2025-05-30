using MediatR;
using VibeCRM.Shared.DTOs.ServiceType;

namespace VibeCRM.Application.Features.ServiceType.Queries.GetServiceTypeById
{
    /// <summary>
    /// Query for retrieving a service type by its ID.
    /// </summary>
    public class GetServiceTypeByIdQuery : IRequest<ServiceTypeDetailsDto>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the service type to retrieve.
        /// </summary>
        public Guid Id { get; set; }
    }
}