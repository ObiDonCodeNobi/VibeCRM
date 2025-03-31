using MediatR;
using VibeCRM.Shared.DTOs.Person;

namespace VibeCRM.Application.Features.Person.Commands.UpdatePerson
{
    /// <summary>
    /// Command to update an existing person in the system.
    /// This is used in the CQRS pattern as the request object for person updates.
    /// </summary>
    public class UpdatePersonCommand : IRequest<PersonDetailsDto>
    {
        /// <summary>
        /// Gets or sets the unique identifier for the person to update.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the first name of the person.
        /// </summary>
        public string Firstname { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the middle initial of the person.
        /// </summary>
        public string MiddleInitial { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the last name of the person.
        /// </summary>
        public string Lastname { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the title of the person.
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the identifier of the user modifying this person.
        /// </summary>
        public Guid ModifiedBy { get; set; }
    }
}