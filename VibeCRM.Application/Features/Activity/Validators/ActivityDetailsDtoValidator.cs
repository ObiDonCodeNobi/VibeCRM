using FluentValidation;
using VibeCRM.Application.Features.Activity.DTOs;

namespace VibeCRM.Application.Features.Activity.Validators
{
    /// <summary>
    /// Validator for the ActivityDetailsDto.
    /// </summary>
    public class ActivityDetailsDtoValidator : AbstractValidator<ActivityDetailsDto>
    {
        public ActivityDetailsDtoValidator()
        {
            // Define validation rules
        }
    }
}