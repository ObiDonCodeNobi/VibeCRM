using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using VibeCRM.Application.Features.ContactType.Commands.CreateContactType;
using VibeCRM.Application.Features.ContactType.Commands.DeleteContactType;
using VibeCRM.Application.Features.ContactType.Commands.UpdateContactType;
using VibeCRM.Application.Features.ContactType.Mappings;
using VibeCRM.Application.Features.ContactType.Queries.GetContactTypeById;
using VibeCRM.Application.Features.ContactType.Queries.GetAllContactTypes;
using VibeCRM.Application.Features.ContactType.Validators;
using VibeCRM.Shared.DTOs.ContactType;

namespace VibeCRM.Application.Extensions
{
    /// <summary>
    /// Provides extension methods for registering ContactType feature services in the dependency injection container.
    /// </summary>
    public static class ContactTypeFeatureExtensions
    {
        /// <summary>
        /// Registers all services required by the ContactType feature.
        /// </summary>
        /// <param name="services">The service collection to add the services to.</param>
        /// <returns>The service collection with all ContactType feature services added.</returns>
        public static IServiceCollection AddContactTypeFeature(this IServiceCollection services)
        {
            // Register command validators
            services.AddScoped<IValidator<CreateContactTypeCommand>, CreateContactTypeCommandValidator>();
            services.AddScoped<IValidator<UpdateContactTypeCommand>, UpdateContactTypeCommandValidator>();
            services.AddScoped<IValidator<DeleteContactTypeCommand>, DeleteContactTypeCommandValidator>();

            // Register query validators
            services.AddScoped<IValidator<GetContactTypeByIdQuery>, GetContactTypeByIdQueryValidator>();
            services.AddScoped<IValidator<GetAllContactTypesQuery>, GetAllContactTypesQueryValidator>();

            // Register DTO validators
            services.AddScoped<IValidator<ContactTypeDto>, ContactTypeDtoValidator>();
            services.AddScoped<IValidator<ContactTypeDetailsDto>, ContactTypeDetailsDtoValidator>();
            services.AddScoped<IValidator<ContactTypeListDto>, ContactTypeListDtoValidator>();

            // Register mapping profiles
            services.AddSingleton(provider => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ContactTypeMappingProfile());
            }).CreateMapper());

            // Register MediatR handlers
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblyContaining<GetContactTypeByIdQuery>();
            });

            return services;
        }
    }
}
