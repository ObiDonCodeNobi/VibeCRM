using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using VibeCRM.Application.Features.PersonStatus.Commands.CreatePersonStatus;
using VibeCRM.Application.Features.PersonStatus.Commands.DeletePersonStatus;
using VibeCRM.Application.Features.PersonStatus.Commands.UpdatePersonStatus;
using VibeCRM.Application.Features.PersonStatus.Mappings;
using VibeCRM.Application.Features.PersonStatus.Queries.GetPersonStatusById;
using VibeCRM.Application.Features.PersonStatus.Queries.GetAllPersonStatuses;
using VibeCRM.Application.Features.PersonStatus.Validators;
using VibeCRM.Shared.DTOs.PersonStatus;

namespace VibeCRM.Application.Extensions
{
    /// <summary>
    /// Provides extension methods for registering PersonStatus feature services in the dependency injection container.
    /// </summary>
    public static class PersonStatusFeatureExtensions
    {
        /// <summary>
        /// Registers all services required by the PersonStatus feature.
        /// </summary>
        /// <param name="services">The service collection to add the services to.</param>
        /// <returns>The service collection with all PersonStatus feature services added.</returns>
        public static IServiceCollection AddPersonStatusFeature(this IServiceCollection services)
        {
            // Register command validators
            services.AddScoped<IValidator<CreatePersonStatusCommand>, CreatePersonStatusCommandValidator>();
            services.AddScoped<IValidator<UpdatePersonStatusCommand>, UpdatePersonStatusCommandValidator>();
            services.AddScoped<IValidator<DeletePersonStatusCommand>, DeletePersonStatusCommandValidator>();

            // Register query validators
            services.AddScoped<IValidator<GetPersonStatusByIdQuery>, GetPersonStatusByIdQueryValidator>();
            services.AddScoped<IValidator<GetAllPersonStatusesQuery>, GetAllPersonStatusesQueryValidator>();

            // Register DTO validators
            services.AddScoped<IValidator<PersonStatusDto>, PersonStatusDtoValidator>();
            services.AddScoped<IValidator<PersonStatusDetailsDto>, PersonStatusDetailsDtoValidator>();
            services.AddScoped<IValidator<PersonStatusListDto>, PersonStatusListDtoValidator>();

            // Register mapping profiles
            services.AddSingleton(provider => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new PersonStatusMappingProfile());
            }).CreateMapper());

            // Register MediatR handlers
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblyContaining<GetPersonStatusByIdQuery>();
            });
            
            return services;
        }
    }
}
