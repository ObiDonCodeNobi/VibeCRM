using MediatR;
using VibeCRM.Shared.DTOs.EmailAddress;

namespace VibeCRM.Application.Features.EmailAddress.Queries.GetEmailAddressById
{
    /// <summary>
    /// Query for retrieving an email address by its unique identifier.
    /// Implements IRequest to return an EmailAddressDetailsDto object.
    /// </summary>
    public class GetEmailAddressByIdQuery : IRequest<EmailAddressDetailsDto>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the email address to retrieve.
        /// </summary>
        public Guid Id { get; set; }
    }
}