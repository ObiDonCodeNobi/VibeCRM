using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using VibeCRM.Application.Features.EmailAddress.Commands.CreateEmailAddress;
using VibeCRM.Application.Features.EmailAddress.Commands.DeleteEmailAddress;
using VibeCRM.Application.Features.EmailAddress.Commands.UpdateEmailAddress;
using VibeCRM.Application.Features.EmailAddress.Mappings;
using VibeCRM.Application.Features.EmailAddress.Queries.GetAllEmailAddresses;
using VibeCRM.Application.Features.EmailAddress.Queries.GetEmailAddressById;
using VibeCRM.Application.Features.EmailAddress.Queries.GetEmailAddressesByType;
using VibeCRM.Application.Features.EmailAddress.Validators;
using VibeCRM.Shared.DTOs.EmailAddress;

namespace VibeCRM.Application.Extensions
{
    /// <summary>
    /// Provides extension methods for registering EmailAddress feature services in the dependency injection container.
    /// </summary>
    public static class EmailAddressFeatureExtensions
    {
        /// <summary>
        /// Registers all services required by the EmailAddress feature.
        /// </summary>
        /// <param name="services">The service collection to add the services to.</param>
        /// <returns>The service collection with all EmailAddress feature services added.</returns>
        public static IServiceCollection AddEmailAddressFeature(this IServiceCollection services)
        {
            // Register command validators
            services.AddScoped<IValidator<CreateEmailAddressCommand>, CreateEmailAddressCommandValidator>();
            services.AddScoped<IValidator<UpdateEmailAddressCommand>, UpdateEmailAddressCommandValidator>();
            services.AddScoped<IValidator<DeleteEmailAddressCommand>, DeleteEmailAddressCommandValidator>();

            // Register query validators
            services.AddScoped<IValidator<GetEmailAddressByIdQuery>, GetEmailAddressByIdQueryValidator>();
            services.AddScoped<IValidator<GetAllEmailAddressesQuery>, GetAllEmailAddressesQueryValidator>();
            services.AddScoped<IValidator<GetEmailAddressesByTypeQuery>, GetEmailAddressesByTypeQueryValidator>();

            // Register DTO validators
            services.AddScoped<IValidator<EmailAddressDto>, EmailAddressDtoValidator>();
            services.AddScoped<IValidator<EmailAddressDetailsDto>, EmailAddressDetailsDtoValidator>();

            // Register mapping profiles
            services.AddSingleton(provider => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new EmailAddressMappingProfile());
            }).CreateMapper());

            return services;
        }
    }
}