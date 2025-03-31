using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using VibeCRM.Application.Features.ServiceType.Commands.CreateServiceType;
using VibeCRM.Application.Features.ServiceType.Commands.DeleteServiceType;
using VibeCRM.Application.Features.ServiceType.Commands.UpdateServiceType;
using VibeCRM.Application.Features.ServiceType.Mappings;
using VibeCRM.Application.Features.ServiceType.Queries.GetServiceTypeById;
using VibeCRM.Application.Features.ServiceType.Queries.GetAllServiceTypes;
using VibeCRM.Application.Features.ServiceType.Validators;
using VibeCRM.Shared.DTOs.ServiceType;

namespace VibeCRM.Application.Extensions
{
    /// <summary>
    /// Provides extension methods for registering ServiceType feature services in the dependency injection container.
    /// </summary>
    public static class ServiceTypeFeatureExtensions
    {
        /// <summary>
        /// Registers all services required by the ServiceType feature.
        /// </summary>
        /// <param name="services">The service collection to add the services to.</param>
        /// <returns>The service collection with all ServiceType feature services added.</returns>
        public static IServiceCollection AddServiceTypeFeature(this IServiceCollection services)
        {
            // Register command validators
            services.AddScoped<IValidator<CreateServiceTypeCommand>, CreateServiceTypeCommandValidator>();
            services.AddScoped<IValidator<UpdateServiceTypeCommand>, UpdateServiceTypeCommandValidator>();
            services.AddScoped<IValidator<DeleteServiceTypeCommand>, DeleteServiceTypeCommandValidator>();

            // Register query validators
            services.AddScoped<IValidator<GetServiceTypeByIdQuery>, GetServiceTypeByIdQueryValidator>();
            services.AddScoped<IValidator<GetAllServiceTypesQuery>, GetAllServiceTypesQueryValidator>();

            // Register DTO validators
            services.AddScoped<IValidator<ServiceTypeDto>, ServiceTypeDtoValidator>();
            services.AddScoped<IValidator<ServiceTypeDetailsDto>, ServiceTypeDetailsDtoValidator>();
            services.AddScoped<IValidator<ServiceTypeListDto>, ServiceTypeListDtoValidator>();

            // Register mapping profiles
            services.AddSingleton(provider => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ServiceTypeMappingProfile());
            }).CreateMapper());

            // Register MediatR handlers
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblyContaining<GetServiceTypeByIdQuery>();
            });
            
            return services;
        }
    }
}
