using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using VibeCRM.Application.Features.ActivityStatus.Commands.CreateActivityStatus;
using VibeCRM.Application.Features.ActivityStatus.Commands.DeleteActivityStatus;
using VibeCRM.Application.Features.ActivityStatus.Commands.UpdateActivityStatus;
using VibeCRM.Application.Features.ActivityStatus.Mappings;
using VibeCRM.Application.Features.ActivityStatus.Queries.GetActivityStatusById;
using VibeCRM.Application.Features.ActivityStatus.Queries.GetAllActivityStatuses;
using VibeCRM.Application.Features.ActivityStatus.Validators;
using VibeCRM.Shared.DTOs.ActivityStatus;
using MediatR;

namespace VibeCRM.Application.Extensions
{
    /// <summary>
    /// Provides extension methods for registering ActivityStatus feature services in the dependency injection container.
    /// </summary>
    public static class ActivityStatusFeatureExtensions
    {
        /// <summary>
        /// Registers all services required by the ActivityStatus feature.
        /// </summary>
        /// <param name="services">The service collection to add the services to.</param>
        /// <returns>The service collection with all ActivityStatus feature services added.</returns>
        public static IServiceCollection AddActivityStatusFeature(this IServiceCollection services)
        {
            // Register command validators
            services.AddScoped<IValidator<CreateActivityStatusCommand>, CreateActivityStatusCommandValidator>();
            services.AddScoped<IValidator<UpdateActivityStatusCommand>, UpdateActivityStatusCommandValidator>();
            services.AddScoped<IValidator<DeleteActivityStatusCommand>, DeleteActivityStatusCommandValidator>();

            // Register query validators
            services.AddScoped<IValidator<GetActivityStatusByIdQuery>, GetActivityStatusByIdQueryValidator>();
            services.AddScoped<IValidator<GetAllActivityStatusesQuery>, GetAllActivityStatusesQueryValidator>();

            // Register DTO validators
            services.AddScoped<IValidator<ActivityStatusDto>, ActivityStatusDtoValidator>();
            services.AddScoped<IValidator<ActivityStatusDetailsDto>, ActivityStatusDetailsDtoValidator>();
            services.AddScoped<IValidator<ActivityStatusListDto>, ActivityStatusListDtoValidator>();

            // Register mapping profiles
            services.AddSingleton(provider => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ActivityStatusMappingProfile());
            }).CreateMapper());

            // Register MediatR handlers
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblyContaining<GetActivityStatusByIdQuery>();
            });

            return services;
        }
    }
}
