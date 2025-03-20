using MediatR;
using VibeCRM.Application.Features.ServiceType.DTOs;

namespace VibeCRM.Application.Features.ServiceType.Queries.GetDefaultServiceType
{
    /// <summary>
    /// Query for retrieving the default service type (the one with the lowest ordinal position).
    /// </summary>
    public class GetDefaultServiceTypeQuery : IRequest<ServiceTypeDto>
    {
        // No parameters needed for retrieving the default service type
    }
}
