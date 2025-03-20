using MediatR;
using VibeCRM.Application.Features.Service.DTOs;

namespace VibeCRM.Application.Features.Service.Queries.GetAllServicesWithRelatedEntities
{
    /// <summary>
    /// Query to retrieve all services with their related entities
    /// </summary>
    public class GetAllServicesWithRelatedEntitiesQuery : IRequest<List<ServiceDetailsDto>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllServicesWithRelatedEntitiesQuery"/> class
        /// </summary>
        public GetAllServicesWithRelatedEntitiesQuery()
        {
        }
    }
}
