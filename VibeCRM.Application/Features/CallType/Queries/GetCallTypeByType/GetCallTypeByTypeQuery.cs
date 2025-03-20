using MediatR;
using VibeCRM.Application.Features.CallType.DTOs;

namespace VibeCRM.Application.Features.CallType.Queries.GetCallTypeByType
{
    /// <summary>
    /// Query for retrieving call types by type name.
    /// Implements IRequest to return a collection of CallTypeListDto.
    /// </summary>
    public class GetCallTypeByTypeQuery : IRequest<IEnumerable<CallTypeListDto>>
    {
        /// <summary>
        /// Gets or sets the type name to search for.
        /// </summary>
        public string Type { get; set; } = string.Empty;
    }
}