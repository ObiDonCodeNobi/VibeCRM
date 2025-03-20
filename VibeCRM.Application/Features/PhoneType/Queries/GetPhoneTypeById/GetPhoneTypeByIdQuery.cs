using MediatR;
using VibeCRM.Application.Features.PhoneType.DTOs;

namespace VibeCRM.Application.Features.PhoneType.Queries.GetPhoneTypeById
{
    /// <summary>
    /// Query to retrieve a phone type by its ID.
    /// </summary>
    public class GetPhoneTypeByIdQuery : IRequest<PhoneTypeDetailsDto>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the phone type to retrieve.
        /// </summary>
        public Guid Id { get; set; }
    }
}
