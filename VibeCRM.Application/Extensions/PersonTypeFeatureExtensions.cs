using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using VibeCRM.Application.Features.PersonType.Commands.CreatePersonType;
using VibeCRM.Application.Features.PersonType.Commands.DeletePersonType;
using VibeCRM.Application.Features.PersonType.Commands.UpdatePersonType;
using VibeCRM.Application.Features.PersonType.Mappings;
using VibeCRM.Application.Features.PersonType.Queries.GetPersonTypeById;
using VibeCRM.Application.Features.PersonType.Queries.GetAllPersonTypes;
using VibeCRM.Application.Features.PersonType.Validators;
using VibeCRM.Shared.DTOs.PersonType;

namespace VibeCRM.Application.Extensions
{
    /// <summary>
    /// Provides extension methods for registering PersonType feature services in the dependency injection container.
    /// </summary>
    public static class PersonTypeFeatureExtensions
    {
        /// <summary>
        /// Registers all services required by the PersonType feature.
        /// </summary>
        /// <param name="services">The service collection to add the services to.</param>
        /// <returns>The service collection with all PersonType feature services added.</returns>
        public static IServiceCollection AddPersonTypeFeature(this IServiceCollection services)
        {
            // Register command validators
            services.AddScoped<IValidator<CreatePersonTypeCommand>, CreatePersonTypeCommandValidator>();
            services.AddScoped<IValidator<UpdatePersonTypeCommand>, UpdatePersonTypeCommandValidator>();
            services.AddScoped<IValidator<DeletePersonTypeCommand>, DeletePersonTypeCommandValidator>();

            // Register query validators
            services.AddScoped<IValidator<GetPersonTypeByIdQuery>, GetPersonTypeByIdQueryValidator>();
            services.AddScoped<IValidator<GetAllPersonTypesQuery>, GetAllPersonTypesQueryValidator>();

            // Register DTO validators
            services.AddScoped<IValidator<PersonTypeDto>, PersonTypeDtoValidator>();
            services.AddScoped<IValidator<PersonTypeDetailsDto>, PersonTypeDetailsDtoValidator>();
            services.AddScoped<IValidator<PersonTypeListDto>, PersonTypeListDtoValidator>();

            // Register mapping profiles
            services.AddSingleton(provider => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new PersonTypeMappingProfile());
            }).CreateMapper());

            // Register MediatR handlers
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblyContaining<GetPersonTypeByIdQuery>();
            });
            
            return services;
        }
    }
}
