using MediatR;

namespace VibeCRM.Application.Features.Company.Commands.DeleteCompany
{
    /// <summary>
    /// Command for soft-deleting an existing company in the system.
    /// Implements the CQRS command pattern for company deletion.
    /// </summary>
    public class DeleteCompanyCommand : IRequest<bool>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteCompanyCommand"/> class.
        /// </summary>
        public DeleteCompanyCommand()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteCompanyCommand"/> class with specified parameters.
        /// </summary>
        /// <param name="id">The unique identifier of the company to delete.</param>
        /// <param name="modifiedBy">The ID of the user performing the deletion.</param>
        public DeleteCompanyCommand(Guid id, Guid modifiedBy)
        {
            Id = id;
            ModifiedBy = modifiedBy;
        }

        /// <summary>
        /// Gets or sets the unique identifier of the company to delete.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the ID of the user who is performing the deletion.
        /// </summary>
        public Guid ModifiedBy { get; set; }
    }
}