using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using VibeCRM.Application.Features.Phone.Commands.CreatePhone;
using VibeCRM.Application.Features.Phone.Commands.DeletePhone;
using VibeCRM.Application.Features.Phone.Commands.UpdatePhone;
using VibeCRM.Application.Features.Phone.DTOs;
using VibeCRM.Application.Features.Phone.Mappings;
using VibeCRM.Application.Features.Phone.Queries.GetAllPhones;
using VibeCRM.Application.Features.Phone.Queries.GetPhoneById;
using VibeCRM.Application.Features.Phone.Validators;

namespace VibeCRM.Application.Extensions
{
    /// <summary>
    /// Provides extension methods for registering Phone feature services in the dependency injection container.
    /// </summary>
    public static class PhoneFeatureExtensions
    {
        /// <summary>
        /// Registers all services required by the Phone feature.
        /// </summary>
        /// <param name="services">The service collection to add the services to.</param>
        /// <returns>The service collection with all Phone feature services added.</returns>
        public static IServiceCollection AddPhoneFeature(this IServiceCollection services)
        {
            // Register command validators
            services.AddScoped<IValidator<CreatePhoneCommand>, CreatePhoneCommandValidator>();
            services.AddScoped<IValidator<UpdatePhoneCommand>, UpdatePhoneCommandValidator>();
            services.AddScoped<IValidator<DeletePhoneCommand>, DeletePhoneCommandValidator>();

            // Register query validators
            services.AddScoped<IValidator<GetPhoneByIdQuery>, GetPhoneByIdQueryValidator>();
            services.AddScoped<IValidator<GetAllPhonesQuery>, GetAllPhonesQueryValidator>();

            // Register DTO validators
            services.AddScoped<IValidator<PhoneDto>, PhoneDtoValidator>();
            services.AddScoped<IValidator<PhoneDetailsDto>, PhoneDetailsDtoValidator>();
            services.AddScoped<IValidator<PhoneListDto>, PhoneListDtoValidator>();

            // Register mapping profiles
            services.AddSingleton(provider => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new PhoneMappingProfile());
            }).CreateMapper());

            // Register MediatR handlers
            services.AddMediatR(cfg => 
            {
                cfg.RegisterServicesFromAssemblyContaining<GetPhoneByIdQuery>();
            });

            return services;
        }
    }
}
