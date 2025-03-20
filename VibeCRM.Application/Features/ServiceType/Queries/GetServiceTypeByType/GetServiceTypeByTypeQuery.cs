using System.Collections.Generic;
using MediatR;
using VibeCRM.Application.Features.ServiceType.DTOs;

namespace VibeCRM.Application.Features.ServiceType.Queries.GetServiceTypeByType
{
    /// <summary>
    /// Query for retrieving service types by their type name.
    /// </summary>
    public class GetServiceTypeByTypeQuery : IRequest<IEnumerable<ServiceTypeListDto>>
    {
        /// <summary>
        /// Gets or sets the type name to search for.
        /// </summary>
        public string Type { get; set; } = string.Empty;
    }
}
