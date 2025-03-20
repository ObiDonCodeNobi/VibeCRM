using FluentValidation;
using VibeCRM.Application.Features.Activity.DTOs;

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