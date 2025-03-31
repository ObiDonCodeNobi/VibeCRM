using FluentValidation;
using VibeCRM.Shared.DTOs.Activity;

namespace VibeCRM.Application.Features.Activity.Validators
{
    /// <summary>
    /// Validator for the ActivityListDto.
    /// </summary>
    public class ActivityListDtoValidator : AbstractValidator<ActivityListDto>
    {
        public ActivityListDtoValidator()
        {
            // Define validation rules
        }
    }
}