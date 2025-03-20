using MediatR;
using VibeCRM.Application.Features.Phone.DTOs;

namespace VibeCRM.Application.Features.Phone.Commands.UpdatePhone
{
    /// <summary>
    /// Command for updating an existing phone record
    /// </summary>
    public class UpdatePhoneCommand : IRequest<PhoneDto>
    {
        /// <summary>
        /// Gets or sets the unique identifier for the phone
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the area code
        /// </summary>
        public int AreaCode { get; set; }

        /// <summary>
        /// Gets or sets the prefix
        /// </summary>
        public int Prefix { get; set; }

        /// <summary>
        /// Gets or sets the line number
        /// </summary>
        public int LineNumber { get; set; }

        /// <summary>
        /// Gets or sets the extension
        /// </summary>
        public int? Extension { get; set; }

        /// <summary>
        /// Gets or sets the phone type identifier
        /// </summary>
        public Guid PhoneTypeId { get; set; }
    }
}