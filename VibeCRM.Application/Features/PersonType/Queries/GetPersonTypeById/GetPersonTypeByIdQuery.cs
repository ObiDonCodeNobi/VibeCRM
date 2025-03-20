using MediatR;
using System;
using VibeCRM.Application.Features.PersonType.DTOs;

namespace VibeCRM.Application.Features.PersonType.Queries.GetPersonTypeById
{
    /// <summary>
    /// Query to retrieve a person type by its unique identifier.
    /// Implements the CQRS query pattern for retrieving a specific person type entity.
    /// </summary>
    public class GetPersonTypeByIdQuery : IRequest<PersonTypeDetailsDto>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the person type to retrieve.
        /// </summary>
        public Guid Id { get; set; }
    }
}
