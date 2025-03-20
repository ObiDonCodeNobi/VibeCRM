using MediatR;
using VibeCRM.Application.Features.CallDirection.DTOs;

namespace VibeCRM.Application.Features.CallDirection.Queries.GetCallDirectionById
{
    /// <summary>
    /// Query to retrieve a call direction by its unique identifier.
    /// </summary>
    public class GetCallDirectionByIdQuery : IRequest<CallDirectionDetailsDto>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the call direction to retrieve.
        /// </summary>
        public Guid Id { get; set; }
    }
}