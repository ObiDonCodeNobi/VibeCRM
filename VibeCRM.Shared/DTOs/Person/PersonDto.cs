namespace VibeCRM.Shared.DTOs.Person
{
    /// <summary>
    /// Base Data Transfer Object for Person entities.
    /// Contains the essential properties needed for basic operations.
    /// </summary>
    public class PersonDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the person.
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
        /// Gets the full name of the person (computed property).
        /// </summary>
        public string FullName => string.IsNullOrEmpty(MiddleInitial)
            ? $"{Firstname} {Lastname}"
            : $"{Firstname} {MiddleInitial}. {Lastname}";

        /// <summary>
        /// Gets or sets whether this person is active (not soft-deleted).
        /// When true, the person is active and visible in queries.
        /// When false, the person is considered deleted but remains in the database.
        /// </summary>
        public bool Active { get; set; } = true;
    }
}