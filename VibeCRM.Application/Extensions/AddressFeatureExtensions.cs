using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using VibeCRM.Application.Features.Address.Commands.CreateAddress;
using VibeCRM.Application.Features.Address.Commands.DeleteAddress;
using VibeCRM.Application.Features.Address.Commands.UpdateAddress;
using VibeCRM.Application.Features.Address.DTOs;
using VibeCRM.Application.Features.Address.Mappings;
using VibeCRM.Application.Features.Address.Queries.GetAddressById;
using VibeCRM.Application.Features.Address.Queries.GetAllAddresses;
using VibeCRM.Application.Features.Address.Validators;

namespace VibeCRM.Application.Extensions
{
    /// <summary>
    /// Provides extension methods for registering Address feature services in the dependency injection container.
    /// </summary>
    public static class AddressFeatureExtensions
    {
        /// <summary>
        /// Registers all services required by the Address feature.
        /// </summary>
        /// <param name="services">The service collection to add the services to.</param>
        /// <returns>The service collection with all Address feature services added.</returns>
        public static IServiceCollection AddAddressFeature(this IServiceCollection services)
        {
            // Register command validators
            services.AddScoped<IValidator<CreateAddressCommand>, CreateAddressCommandValidator>();
            services.AddScoped<IValidator<UpdateAddressCommand>, UpdateAddressCommandValidator>();
            services.AddScoped<IValidator<DeleteAddressCommand>, DeleteAddressCommandValidator>();

            // Register query validators
            services.AddScoped<IValidator<GetAddressByIdQuery>, GetAddressByIdQueryValidator>();
            services.AddScoped<IValidator<GetAllAddressesQuery>, GetAllAddressesQueryValidator>();

            // Register DTO validators
            services.AddScoped<IValidator<AddressDto>, AddressDtoValidator>();
            services.AddScoped<IValidator<AddressDetailsDto>, AddressDetailsDtoValidator>();

            // Register mapping profiles
            services.AddSingleton(provider => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AddressMappingProfile());
            }).CreateMapper());

            return services;
        }
    }
}