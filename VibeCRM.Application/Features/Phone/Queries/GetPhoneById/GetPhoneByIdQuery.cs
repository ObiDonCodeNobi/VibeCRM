using MediatR;
using VibeCRM.Shared.DTOs.Phone;

namespace VibeCRM.Application.Features.Phone.Queries.GetPhoneById
{
    /// <summary>
    /// Query to retrieve a phone by its unique identifier
    /// </summary>
    public class GetPhoneByIdQuery : IRequest<PhoneDetailsDto>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the phone to retrieve
        /// </summary>
        public Guid Id { get; set; }
    }
}