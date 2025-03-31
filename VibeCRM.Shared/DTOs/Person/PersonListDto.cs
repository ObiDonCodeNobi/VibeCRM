namespace VibeCRM.Shared.DTOs.Person
{
    /// <summary>
    /// List Data Transfer Object for Person entities.
    /// Contains properties needed for displaying persons in lists.
    /// </summary>
    public class PersonListDto : PersonDto
    {
        /// <summary>
        /// Gets or sets the primary company name associated with this person, if any.
        /// </summary>
        public string? PrimaryCompanyName { get; set; }

        /// <summary>
        /// Gets or sets the primary email address associated with this person, if any.
        /// </summary>
        public string? PrimaryEmail { get; set; }

        /// <summary>
        /// Gets or sets the primary phone number associated with this person, if any.
        /// </summary>
        public string? PrimaryPhone { get; set; }

        /// <summary>
        /// Gets or sets the primary address associated with this person, if any.
        /// </summary>
        public string? PrimaryAddress { get; set; }
    }
}