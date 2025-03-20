using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using VibeCRM.Application.Features.Activity.Commands.CreateActivity;
using VibeCRM.Application.Features.Activity.Commands.DeleteActivity;
using VibeCRM.Application.Features.Activity.Commands.UpdateActivity;
using VibeCRM.Application.Features.Activity.DTOs;
using VibeCRM.Application.Features.Activity.Mappings;
using VibeCRM.Application.Features.Activity.Queries.GetActivityById;
using VibeCRM.Application.Features.Activity.Queries.GetAllActivities;
using VibeCRM.Application.Features.Activity.Validators;

namespace VibeCRM.Application.Extensions
{
    /// <summary>
    /// Provides extension methods for registering Activity feature services in the dependency injection container.
    /// </summary>
    public static class ActivityFeatureExtensions
    {
        /// <summary>
        /// Registers all services required by the Activity feature.
        /// </summary>
        /// <param name="services">The service collection to add the services to.</param>
        /// <returns>The service collection with Activity feature services added.</returns>
        public static IServiceCollection AddActivityFeature(this IServiceCollection services)
        {
            // Register command validators
            services.AddScoped<IValidator<CreateActivityCommand>, CreateActivityCommandValidator>();
            services.AddScoped<IValidator<UpdateActivityCommand>, UpdateActivityCommandValidator>();
            services.AddScoped<IValidator<DeleteActivityCommand>, DeleteActivityCommandValidator>();

            // Register query validators
            services.AddScoped<IValidator<GetActivityByIdQuery>, GetActivityByIdQueryValidator>();
            services.AddScoped<IValidator<GetAllActivitiesQuery>, GetAllActivitiesQueryValidator>();

            // Register DTO validators
            services.AddScoped<IValidator<ActivityDto>, ActivityDtoValidator>();
            services.AddScoped<IValidator<ActivityDetailsDto>, ActivityDetailsDtoValidator>();
            services.AddScoped<IValidator<ActivityListDto>, ActivityListDtoValidator>();

            // Register mapping profiles
            services.AddSingleton(provider => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ActivityMappingProfile());
            }).CreateMapper());

            return services;
        }
    }
}