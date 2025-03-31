namespace VibeCRM.Shared.DTOs.Person
{
    /// <summary>
    /// Detailed Data Transfer Object for Person entities.
    /// Contains all properties needed for detailed views and operations.
    /// </summary>
    public class PersonDetailsDto : PersonDto
    {
        /// <summary>
        /// Gets or sets the collection of companies associated with this person.
        /// </summary>
        public ICollection<CompanyReferenceDto>? Companies { get; set; }

        /// <summary>
        /// Gets or sets the collection of addresses associated with this person.
        /// </summary>
        public ICollection<AddressReferenceDto>? Addresses { get; set; }

        /// <summary>
        /// Gets or sets the collection of phone numbers associated with this person.
        /// </summary>
        public ICollection<PhoneReferenceDto>? PhoneNumbers { get; set; }

        /// <summary>
        /// Gets or sets the collection of email addresses associated with this person.
        /// </summary>
        public ICollection<EmailAddressReferenceDto>? EmailAddresses { get; set; }

        /// <summary>
        /// Gets or sets the collection of activities associated with this person.
        /// </summary>
        public ICollection<ActivityReferenceDto>? Activities { get; set; }

        /// <summary>
        /// Gets or sets the collection of attachments associated with this person.
        /// </summary>
        public ICollection<AttachmentReferenceDto>? Attachments { get; set; }

        /// <summary>
        /// Gets or sets the collection of notes associated with this person.
        /// </summary>
        public ICollection<NoteReferenceDto>? Notes { get; set; }

        /// <summary>
        /// Gets or sets the collection of calls associated with this person.
        /// </summary>
        public ICollection<CallReferenceDto>? Calls { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user who created this person.
        /// </summary>
        public Guid CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the date and time when this person was created.
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user who last modified this person.
        /// </summary>
        public Guid ModifiedBy { get; set; }

        /// <summary>
        /// Gets or sets the date and time when this person was last modified.
        /// </summary>
        public DateTime ModifiedDate { get; set; }
    }

    /// <summary>
    /// Reference Data Transfer Object for Company entities associated with a person.
    /// </summary>
    public class CompanyReferenceDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the company.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the company.
        /// </summary>
        public string Name { get; set; } = string.Empty;
    }

    /// <summary>
    /// Reference Data Transfer Object for Address entities associated with a person.
    /// </summary>
    public class AddressReferenceDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the address.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the address line 1.
        /// </summary>
        public string AddressLine1 { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        public string City { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the state or province.
        /// </summary>
        public string StateProvince { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the postal code.
        /// </summary>
        public string PostalCode { get; set; } = string.Empty;
    }

    /// <summary>
    /// Reference Data Transfer Object for Phone entities associated with a person.
    /// </summary>
    public class PhoneReferenceDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the phone.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the phone number.
        /// </summary>
        public string PhoneNumber { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the phone type (e.g., Mobile, Work, Home).
        /// </summary>
        public string PhoneType { get; set; } = string.Empty;
    }

    /// <summary>
    /// Reference Data Transfer Object for Email Address entities associated with a person.
    /// </summary>
    public class EmailAddressReferenceDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the email address.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the email address.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the email type (e.g., Work, Personal).
        /// </summary>
        public string EmailType { get; set; } = string.Empty;
    }

    /// <summary>
    /// Reference Data Transfer Object for Activity entities associated with a person.
    /// </summary>
    public class ActivityReferenceDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the activity.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the subject of the activity.
        /// </summary>
        public string Subject { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the activity type.
        /// </summary>
        public string ActivityType { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the scheduled date and time for the activity.
        /// </summary>
        public DateTime ScheduledDateTime { get; set; }
    }

    /// <summary>
    /// Reference Data Transfer Object for Attachment entities associated with a person.
    /// </summary>
    public class AttachmentReferenceDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the attachment.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the attachment.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the file type of the attachment.
        /// </summary>
        public string FileType { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the attachment type.
        /// </summary>
        public string AttachmentType { get; set; } = string.Empty;
    }

    /// <summary>
    /// Reference Data Transfer Object for Note entities associated with a person.
    /// </summary>
    public class NoteReferenceDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the note.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the title of the note.
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the content of the note.
        /// </summary>
        public string Content { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the note type.
        /// </summary>
        public string NoteType { get; set; } = string.Empty;
    }

    /// <summary>
    /// Reference Data Transfer Object for Call entities associated with a person.
    /// </summary>
    public class CallReferenceDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the call.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the subject of the call.
        /// </summary>
        public string Subject { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the call type.
        /// </summary>
        public string CallType { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the date and time of the call.
        /// </summary>
        public DateTime CallDateTime { get; set; }

        /// <summary>
        /// Gets or sets the duration of the call in minutes.
        /// </summary>
        public int DurationMinutes { get; set; }
    }
}