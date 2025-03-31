using MediatR;
using VibeCRM.Shared.DTOs.EmailAddressType;

namespace VibeCRM.Application.Features.EmailAddressType.Queries.GetEmailAddressTypeByType
{
    /// <summary>
    /// Query to retrieve email address types by their type name.
    /// </summary>
    public class GetEmailAddressTypeByTypeQuery : IRequest<IEnumerable<EmailAddressTypeDto>>
    {
        /// <summary>
        /// Gets or sets the type name to search for.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetEmailAddressTypeByTypeQuery"/> class.
        /// </summary>
        /// <param name="type">The type name to search for.</param>
        public GetEmailAddressTypeByTypeQuery(string type)
        {
            Type = type;
        }
    }
}