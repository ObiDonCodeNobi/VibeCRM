using MediatR;
using VibeCRM.Application.Features.ServiceType.DTOs;

namespace VibeCRM.Application.Features.ServiceType.Queries.GetAllServiceTypes
{
    /// <summary>
    /// Query for retrieving all service types.
    /// </summary>
    public class GetAllServiceTypesQuery : IRequest<IEnumerable<ServiceTypeListDto>>
    {
        // No parameters needed for retrieving all service types
    }
}