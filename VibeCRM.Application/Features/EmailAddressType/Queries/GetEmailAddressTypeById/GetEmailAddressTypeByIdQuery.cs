using MediatR;
using VibeCRM.Shared.DTOs.EmailAddressType;

namespace VibeCRM.Application.Features.EmailAddressType.Queries.GetEmailAddressTypeById
{
    /// <summary>
    /// Query to retrieve an email address type by its unique identifier.
    /// </summary>
    public class GetEmailAddressTypeByIdQuery : IRequest<EmailAddressTypeDetailsDto>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the email address type to retrieve.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetEmailAddressTypeByIdQuery"/> class.
        /// </summary>
        /// <param name="id">The unique identifier of the email address type to retrieve.</param>
        public GetEmailAddressTypeByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}