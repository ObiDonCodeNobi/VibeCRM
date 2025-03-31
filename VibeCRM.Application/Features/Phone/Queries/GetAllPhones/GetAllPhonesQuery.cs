using MediatR;
using VibeCRM.Shared.DTOs.Phone;

namespace VibeCRM.Application.Features.Phone.Queries.GetAllPhones
{
    /// <summary>
    /// Query to retrieve all active phones
    /// </summary>
    public class GetAllPhonesQuery : IRequest<IEnumerable<PhoneListDto>>
    {
        // No parameters needed for retrieving all phones
    }
}