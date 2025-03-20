using VibeCRM.Domain.Common.Base;
using VibeCRM.Domain.Common.Interfaces;

namespace VibeCRM.Domain.Entities.BusinessEntities
{
    /// <summary>
    /// Represents a user of the system who can log in and perform actions.
    /// </summary>
    public class User : BaseAuditableEntity<Guid>, ISoftDelete
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        public User()
        {
            Id = Guid.NewGuid();
            LoginName = string.Empty;
            LoginPassword = string.Empty;
            LastLogin = DateTime.MinValue;
        }

        /// <summary>
        /// Gets or sets the user identifier that directly maps to the UserId database column.
        /// </summary>
        /// <remarks>This property maps to the Id property from the base class.</remarks>
        public Guid UserId { get => Id; set => Id = value; }

        /// <summary>
        /// Gets or sets the unique identifier of the person associated with this user.
        /// </summary>
        public Guid PersonId { get; set; }

        /// <summary>
        /// Gets or sets the login name used for authentication.
        /// </summary>
        public string LoginName { get; set; }

        /// <summary>
        /// Gets or sets the password used for authentication.
        /// </summary>
        /// <remarks>
        /// This should store a hashed version of the password, never the plaintext password.
        /// </remarks>
        public string LoginPassword { get; set; }

        /// <summary>
        /// Gets or sets the date and time of the user's last successful login.
        /// </summary>
        public DateTime LastLogin { get; set; }
    }
}