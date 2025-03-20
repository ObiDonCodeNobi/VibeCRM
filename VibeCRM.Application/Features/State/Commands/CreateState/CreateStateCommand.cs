using MediatR;
using VibeCRM.Application.Features.State.DTOs;

namespace VibeCRM.Application.Features.State.Commands.CreateState
{
    /// <summary>
    /// Command for creating a new state.
    /// </summary>
    public class CreateStateCommand : IRequest<StateDto>
    {
        /// <summary>
        /// Gets or sets the unique identifier for the state.
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Gets or sets the name of the state or province.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the state or province abbreviation code (e.g., "CA" for California).
        /// </summary>
        public string Abbreviation { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the country code to which this state belongs (e.g., "US" for United States).
        /// </summary>
        public string CountryCode { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the ordinal position for sorting states in listings and dropdowns.
        /// </summary>
        public int OrdinalPosition { get; set; }
    }
}