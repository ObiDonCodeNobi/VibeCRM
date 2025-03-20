using MediatR;
using VibeCRM.Application.Features.CallType.DTOs;

namespace VibeCRM.Application.Features.CallType.Queries.GetCallTypeById
{
    /// <summary>
    /// Query for retrieving a call type by its unique identifier.
    /// Implements IRequest to return a CallTypeDetailsDto.
    /// </summary>
    public class GetCallTypeByIdQuery : IRequest<CallTypeDetailsDto>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the call type to retrieve.
        /// </summary>
        public Guid Id { get; set; }
    }
}