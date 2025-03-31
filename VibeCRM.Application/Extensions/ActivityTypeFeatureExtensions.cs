using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using VibeCRM.Application.Features.ActivityType.Commands.CreateActivityType;
using VibeCRM.Application.Features.ActivityType.Commands.DeleteActivityType;
using VibeCRM.Application.Features.ActivityType.Commands.UpdateActivityType;
using VibeCRM.Application.Features.ActivityType.Mappings;
using VibeCRM.Application.Features.ActivityType.Queries.GetActivityTypeById;
using VibeCRM.Application.Features.ActivityType.Queries.GetAllActivityTypes;
using VibeCRM.Application.Features.ActivityType.Validators;
using VibeCRM.Shared.DTOs.ActivityType;

namespace VibeCRM.Application.Extensions
{
    /// <summary>
    /// Provides extension methods for registering ActivityType feature services in the dependency injection container.
    /// </summary>
    public static class ActivityTypeFeatureExtensions
    {
        /// <summary>
        /// Registers all services required by the ActivityType feature.
        /// </summary>
        /// <param name="services">The service collection to add the services to.</param>
        /// <returns>The service collection with all ActivityType feature services added.</returns>
        public static IServiceCollection AddActivityTypeFeature(this IServiceCollection services)
        {
            // Register command validators
            services.AddScoped<IValidator<CreateActivityTypeCommand>, CreateActivityTypeCommandValidator>();
            services.AddScoped<IValidator<UpdateActivityTypeCommand>, UpdateActivityTypeCommandValidator>();
            services.AddScoped<IValidator<DeleteActivityTypeCommand>, DeleteActivityTypeCommandValidator>();

            // Register query validators
            services.AddScoped<IValidator<GetActivityTypeByIdQuery>, GetActivityTypeByIdQueryValidator>();
            services.AddScoped<IValidator<GetAllActivityTypesQuery>, GetAllActivityTypesQueryValidator>();

            // Register DTO validators
            services.AddScoped<IValidator<ActivityTypeDto>, ActivityTypeDtoValidator>();
            services.AddScoped<IValidator<ActivityTypeDetailsDto>, ActivityTypeDetailsDtoValidator>();
            services.AddScoped<IValidator<ActivityTypeListDto>, ActivityTypeListDtoValidator>();

            // Register mapping profiles
            services.AddSingleton(provider => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ActivityTypeMappingProfile());
            }).CreateMapper());

            // Register MediatR handlers
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblyContaining<GetActivityTypeByIdQuery>();
            });

            return services;
        }
    }
}
