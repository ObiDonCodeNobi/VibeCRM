using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using VibeCRM.Application.Features.ShipMethod.Commands.CreateShipMethod;
using VibeCRM.Application.Features.ShipMethod.Commands.DeleteShipMethod;
using VibeCRM.Application.Features.ShipMethod.Commands.UpdateShipMethod;
using VibeCRM.Application.Features.ShipMethod.Mappings;
using VibeCRM.Application.Features.ShipMethod.Queries.GetShipMethodById;
using VibeCRM.Application.Features.ShipMethod.Queries.GetAllShipMethods;
using VibeCRM.Application.Features.ShipMethod.Validators;
using VibeCRM.Shared.DTOs.ShipMethod;

namespace VibeCRM.Application.Extensions
{
    /// <summary>
    /// Provides extension methods for registering ShipMethod feature services in the dependency injection container.
    /// </summary>
    public static class ShipMethodFeatureExtensions
    {
        /// <summary>
        /// Registers all services required by the ShipMethod feature.
        /// </summary>
        /// <param name="services">The service collection to add the services to.</param>
        /// <returns>The service collection with all ShipMethod feature services added.</returns>
        public static IServiceCollection AddShipMethodFeature(this IServiceCollection services)
        {
            // Register command validators
            services.AddScoped<IValidator<CreateShipMethodCommand>, CreateShipMethodCommandValidator>();
            services.AddScoped<IValidator<UpdateShipMethodCommand>, UpdateShipMethodCommandValidator>();
            services.AddScoped<IValidator<DeleteShipMethodCommand>, DeleteShipMethodCommandValidator>();

            // Register query validators
            services.AddScoped<IValidator<GetShipMethodByIdQuery>, GetShipMethodByIdQueryValidator>();
            services.AddScoped<IValidator<GetAllShipMethodsQuery>, GetAllShipMethodsQueryValidator>();

            // Register DTO validators
            services.AddScoped<IValidator<ShipMethodDto>, ShipMethodDtoValidator>();
            services.AddScoped<IValidator<ShipMethodDetailsDto>, ShipMethodDetailsDtoValidator>();
            services.AddScoped<IValidator<ShipMethodListDto>, ShipMethodListDtoValidator>();

            // Register mapping profiles
            services.AddSingleton(provider => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ShipMethodMappingProfile());
            }).CreateMapper());

            // Register MediatR handlers
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblyContaining<GetShipMethodByIdQuery>();
            });
            
            return services;
        }
    }
}
