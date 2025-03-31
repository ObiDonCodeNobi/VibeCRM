using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using VibeCRM.Application.Features.AddressType.Commands.CreateAddressType;
using VibeCRM.Application.Features.AddressType.Commands.DeleteAddressType;
using VibeCRM.Application.Features.AddressType.Commands.UpdateAddressType;
using VibeCRM.Application.Features.AddressType.Mappings;
using VibeCRM.Application.Features.AddressType.Queries.GetAddressTypeById;
using VibeCRM.Application.Features.AddressType.Queries.GetAllAddressTypes;
using VibeCRM.Application.Features.AddressType.Validators;
using VibeCRM.Shared.DTOs.AddressType;
using MediatR;

namespace VibeCRM.Application.Extensions
{
    /// <summary>
    /// Provides extension methods for registering AddressType feature services in the dependency injection container.
    /// </summary>
    public static class AddressTypeFeatureExtensions
    {
        /// <summary>
        /// Registers all services required by the AddressType feature.
        /// </summary>
        /// <param name="services">The service collection to add the services to.</param>
        /// <returns>The service collection with all AddressType feature services added.</returns>
        public static IServiceCollection AddAddressTypeFeature(this IServiceCollection services)
        {
            // Register command validators
            services.AddScoped<IValidator<CreateAddressTypeCommand>, CreateAddressTypeCommandValidator>();
            services.AddScoped<IValidator<UpdateAddressTypeCommand>, UpdateAddressTypeCommandValidator>();
            services.AddScoped<IValidator<DeleteAddressTypeCommand>, DeleteAddressTypeCommandValidator>();

            // Register query validators
            services.AddScoped<IValidator<GetAddressTypeByIdQuery>, GetAddressTypeByIdQueryValidator>();
            services.AddScoped<IValidator<GetAllAddressTypesQuery>, GetAllAddressTypesQueryValidator>();

            // Register DTO validators
            services.AddScoped<IValidator<AddressTypeDto>, AddressTypeDtoValidator>();
            services.AddScoped<IValidator<AddressTypeDetailsDto>, AddressTypeDetailsDtoValidator>();
            services.AddScoped<IValidator<AddressTypeListDto>, AddressTypeListDtoValidator>();

            // Register mapping profiles
            services.AddSingleton(provider => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AddressTypeMappingProfile());
            }).CreateMapper());

            // Register MediatR handlers
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblyContaining<GetAddressTypeByIdQuery>();
            });

            return services;
        }
    }
}
