using MediatR;
using VibeCRM.Shared.DTOs.PhoneType;

namespace VibeCRM.Application.Features.PhoneType.Queries.GetAllPhoneTypes
{
    /// <summary>
    /// Query to retrieve all phone types.
    /// </summary>
    public class GetAllPhoneTypesQuery : IRequest<IEnumerable<PhoneTypeListDto>>
    {
        // No parameters needed for this query
    }
}