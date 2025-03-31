using MediatR;
using VibeCRM.Shared.DTOs.CallDirection;

namespace VibeCRM.Application.Features.CallDirection.Queries.GetCallDirectionByDirection
{
    /// <summary>
    /// Query to retrieve a call direction by its direction name.
    /// </summary>
    public class GetCallDirectionByDirectionQuery : IRequest<CallDirectionDetailsDto>
    {
        /// <summary>
        /// Gets or sets the direction name of the call direction to retrieve.
        /// </summary>
        public string Direction { get; set; } = string.Empty;
    }
}