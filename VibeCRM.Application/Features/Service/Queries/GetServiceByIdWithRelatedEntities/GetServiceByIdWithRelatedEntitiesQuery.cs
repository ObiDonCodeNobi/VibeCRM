using MediatR;
using VibeCRM.Application.Features.Service.DTOs;

namespace VibeCRM.Application.Features.Service.Queries.GetServiceByIdWithRelatedEntities
{
    /// <summary>
    /// Query to retrieve a service by ID with all related entities
    /// </summary>
    public class GetServiceByIdWithRelatedEntitiesQuery : IRequest<ServiceDetailsDto?>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the service to retrieve
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetServiceByIdWithRelatedEntitiesQuery"/> class
        /// </summary>
        public GetServiceByIdWithRelatedEntitiesQuery()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetServiceByIdWithRelatedEntitiesQuery"/> class with the specified service ID
        /// </summary>
        /// <param name="id">The unique identifier of the service to retrieve</param>
        public GetServiceByIdWithRelatedEntitiesQuery(Guid id)
        {
            Id = id;
        }
    }
}