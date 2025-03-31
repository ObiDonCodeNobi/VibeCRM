using MediatR;
using VibeCRM.Shared.DTOs.ServiceType;

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