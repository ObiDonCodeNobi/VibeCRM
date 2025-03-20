using MediatR;
using VibeCRM.Application.Features.Person.DTOs;

namespace VibeCRM.Application.Features.Person.Commands.CreatePerson
{
    /// <summary>
    /// Command to create a new person in the system.
    /// This is used in the CQRS pattern as the request object for person creation.
    /// </summary>
    public class CreatePersonCommand : IRequest<PersonDetailsDto>
    {
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
        /// Gets or sets the identifier of the user creating this person.
        /// </summary>
        public Guid CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user modifying this person.
        /// </summary>
        public Guid ModifiedBy { get; set; }
    }
}