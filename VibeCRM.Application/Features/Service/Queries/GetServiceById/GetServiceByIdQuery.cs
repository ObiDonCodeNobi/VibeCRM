using MediatR;
using VibeCRM.Application.Features.Service.DTOs;

namespace VibeCRM.Application.Features.Service.Queries.GetServiceById
{
    /// <summary>
    /// Query to retrieve a specific service by its unique identifier.
    /// This is used in the CQRS pattern as the request object for fetching a single service.
    /// </summary>
    public class GetServiceByIdQuery : IRequest<ServiceDetailsDto?>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the service to retrieve.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetServiceByIdQuery"/> class.
        /// </summary>
        /// <param name="id">The unique identifier of the service to retrieve.</param>
        public GetServiceByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}