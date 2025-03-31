using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using VibeCRM.Application.Features.Person.Commands.CreatePerson;
using VibeCRM.Application.Features.Person.Commands.DeletePerson;
using VibeCRM.Application.Features.Person.Commands.UpdatePerson;
using VibeCRM.Application.Features.Person.Mappings;
using VibeCRM.Application.Features.Person.Queries.GetAllPersons;
using VibeCRM.Application.Features.Person.Queries.GetPersonById;
using VibeCRM.Application.Features.Person.Validators;
using VibeCRM.Shared.DTOs.Person;

namespace VibeCRM.Application.Extensions
{
    /// <summary>
    /// Provides extension methods for registering Person feature services in the dependency injection container.
    /// </summary>
    public static class PersonFeatureExtensions
    {
        /// <summary>
        /// Registers all services required by the Person feature.
        /// </summary>
        /// <param name="services">The service collection to add the services to.</param>
        /// <returns>The service collection with all Person feature services added.</returns>
        public static IServiceCollection AddPersonFeature(this IServiceCollection services)
        {
            // Register validators
            services.AddScoped<IValidator<PersonDto>, PersonDtoValidator>();
            services.AddScoped<IValidator<PersonDetailsDto>, PersonDetailsDtoValidator>();
            services.AddScoped<IValidator<PersonListDto>, PersonListDtoValidator>();

            // Register command validators
            services.AddScoped<IValidator<CreatePersonCommand>, CreatePersonCommandValidator>();
            services.AddScoped<IValidator<UpdatePersonCommand>, UpdatePersonCommandValidator>();
            services.AddScoped<IValidator<DeletePersonCommand>, DeletePersonCommandValidator>();

            // Register query validators
            services.AddScoped<IValidator<GetPersonByIdQuery>, GetPersonByIdQueryValidator>();
            services.AddScoped<IValidator<GetAllPersonsQuery>, GetAllPersonsQueryValidator>();

            // Register AutoMapper profiles
            services.AddSingleton(provider => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new PersonMappingProfile());
            }).CreateMapper());

            return services;
        }
    }
}