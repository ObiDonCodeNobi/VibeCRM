using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using VibeCRM.Application.Features.ActivityDefinition.Commands.CreateActivityDefinition;
using VibeCRM.Application.Features.ActivityDefinition.Commands.DeleteActivityDefinition;
using VibeCRM.Application.Features.ActivityDefinition.Commands.UpdateActivityDefinition;
using VibeCRM.Application.Features.ActivityDefinition.DTOs;
using VibeCRM.Application.Features.ActivityDefinition.Mappings;
using VibeCRM.Application.Features.ActivityDefinition.Queries.GetActivityDefinitionById;
using VibeCRM.Application.Features.ActivityDefinition.Queries.GetAllActivityDefinitions;
using VibeCRM.Application.Features.ActivityDefinition.Validators;

namespace VibeCRM.Application.Extensions
{
    /// <summary>
    /// Provides extension methods for registering ActivityDefinition feature services in the dependency injection container.
    /// </summary>
    public static class ActivityDefinitionFeatureExtensions
    {
        /// <summary>
        /// Registers all services required by the ActivityDefinition feature.
        /// </summary>
        /// <param name="services">The service collection to add the services to.</param>
        /// <returns>The service collection with all ActivityDefinition feature services added.</returns>
        public static IServiceCollection AddActivityDefinitionFeature(this IServiceCollection services)
        {
            // Register command validators
            services.AddScoped<IValidator<CreateActivityDefinitionCommand>, CreateActivityDefinitionCommandValidator>();
            services.AddScoped<IValidator<UpdateActivityDefinitionCommand>, UpdateActivityDefinitionCommandValidator>();
            services.AddScoped<IValidator<DeleteActivityDefinitionCommand>, DeleteActivityDefinitionCommandValidator>();

            // Register query validators
            services.AddScoped<IValidator<GetActivityDefinitionByIdQuery>, GetActivityDefinitionByIdQueryValidator>();
            services.AddScoped<IValidator<GetAllActivityDefinitionsQuery>, GetAllActivityDefinitionsQueryValidator>();

            // Register DTO validators
            services.AddScoped<IValidator<ActivityDefinitionDto>, ActivityDefinitionDtoValidator>();
            services.AddScoped<IValidator<ActivityDefinitionDetailsDto>, ActivityDefinitionDetailsDtoValidator>();

            // Register mapping profiles
            services.AddSingleton(provider => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ActivityDefinitionMappingProfile());
            }).CreateMapper());

            return services;
        }
    }
}