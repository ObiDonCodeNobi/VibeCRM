using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using VibeCRM.Application.Features.AccountStatus.Commands.CreateAccountStatus;
using VibeCRM.Application.Features.AccountStatus.Commands.DeleteAccountStatus;
using VibeCRM.Application.Features.AccountStatus.Commands.UpdateAccountStatus;
using VibeCRM.Application.Features.AccountStatus.DTOs;
using VibeCRM.Application.Features.AccountStatus.Mappings;
using VibeCRM.Application.Features.AccountStatus.Queries.GetAccountStatusById;
using VibeCRM.Application.Features.AccountStatus.Queries.GetAllAccountStatuses;
using VibeCRM.Application.Features.AccountStatus.Validators;

namespace VibeCRM.Application.Extensions
{
    /// <summary>
    /// Provides extension methods for registering AccountStatus feature services in the dependency injection container.
    /// </summary>
    public static class AccountStatusFeatureExtensions
    {
        /// <summary>
        /// Registers all services required by the AccountStatus feature.
        /// </summary>
        /// <param name="services">The service collection to add the services to.</param>
        /// <returns>The service collection with all AccountStatus feature services added.</returns>
        public static IServiceCollection AddAccountStatusFeature(this IServiceCollection services)
        {
            // Register command validators
            services.AddScoped<IValidator<CreateAccountStatusCommand>, CreateAccountStatusCommandValidator>();
            services.AddScoped<IValidator<UpdateAccountStatusCommand>, UpdateAccountStatusCommandValidator>();
            services.AddScoped<IValidator<DeleteAccountStatusCommand>, DeleteAccountStatusCommandValidator>();

            // Register query validators
            services.AddScoped<IValidator<GetAccountStatusByIdQuery>, GetAccountStatusByIdQueryValidator>();
            services.AddScoped<IValidator<GetAllAccountStatusesQuery>, GetAllAccountStatusesQueryValidator>();

            // Register DTO validators
            services.AddScoped<IValidator<AccountStatusDto>, AccountStatusDtoValidator>();
            services.AddScoped<IValidator<AccountStatusDetailsDto>, AccountStatusDetailsDtoValidator>();
            services.AddScoped<IValidator<AccountStatusListDto>, AccountStatusListDtoValidator>();

            // Register mapping profiles
            services.AddSingleton(provider => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AccountStatusMappingProfile());
            }).CreateMapper());

            // Register MediatR handlers
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblyContaining<GetAccountStatusByIdQuery>();
            });

            return services;
        }
    }
}