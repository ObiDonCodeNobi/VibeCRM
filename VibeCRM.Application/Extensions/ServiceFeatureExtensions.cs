using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using VibeCRM.Application.Features.Service.Commands.CreateService;
using VibeCRM.Application.Features.Service.Commands.DeleteService;
using VibeCRM.Application.Features.Service.Commands.UpdateService;
using VibeCRM.Application.Features.Service.DTOs;
using VibeCRM.Application.Features.Service.Mappings;
using VibeCRM.Application.Features.Service.Queries.GetAllServices;
using VibeCRM.Application.Features.Service.Queries.GetServiceById;
using VibeCRM.Application.Features.Service.Validators;

namespace VibeCRM.Application.Extensions
{
    /// <summary>
    /// Provides extension methods for registering Service feature services in the dependency injection container.
    /// </summary>
    public static class ServiceFeatureExtensions
    {
        /// <summary>
        /// Registers all services required for the Service feature.
        /// </summary>
        /// <param name="services">The service collection to add the services to.</param>
        /// <returns>The service collection with Service feature services added.</returns>
        public static IServiceCollection AddServiceFeature(this IServiceCollection services)
        {
            // Register command validators
            services.AddScoped<IValidator<CreateServiceCommand>, CreateServiceCommandValidator>();
            services.AddScoped<IValidator<UpdateServiceCommand>, UpdateServiceCommandValidator>();
            services.AddScoped<IValidator<DeleteServiceCommand>, DeleteServiceCommandValidator>();

            // Register query validators
            services.AddScoped<IValidator<GetServiceByIdQuery>, GetServiceByIdQueryValidator>();
            services.AddScoped<IValidator<GetAllServicesQuery>, GetAllServicesQueryValidator>();

            // Register DTO validators
            services.AddScoped<IValidator<ServiceDto>, ServiceDtoValidator>();
            services.AddScoped<IValidator<ServiceDetailsDto>, ServiceDetailsDtoValidator>();

            // Register mapping profiles
            services.AddSingleton(provider => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ServiceMappingProfile());
            }).CreateMapper());

            return services;
        }
    }
}