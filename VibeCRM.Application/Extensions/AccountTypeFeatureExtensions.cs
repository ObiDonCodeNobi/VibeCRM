using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using VibeCRM.Application.Features.AccountType.Commands.CreateAccountType;
using VibeCRM.Application.Features.AccountType.Commands.DeleteAccountType;
using VibeCRM.Application.Features.AccountType.Commands.UpdateAccountType;
using VibeCRM.Application.Features.AccountType.Mappings;
using VibeCRM.Application.Features.AccountType.Queries.GetAccountTypeById;
using VibeCRM.Application.Features.AccountType.Queries.GetAllAccountTypes;
using VibeCRM.Application.Features.AccountType.Validators;
using VibeCRM.Shared.DTOs.AccountType;

namespace VibeCRM.Application.Extensions
{
    /// <summary>
    /// Provides extension methods for registering AccountType feature services in the dependency injection container.
    /// </summary>
    public static class AccountTypeFeatureExtensions
    {
        /// <summary>
        /// Registers all services required by the AccountType feature.
        /// </summary>
        /// <param name="services">The service collection to add the services to.</param>
        /// <returns>The service collection with all AccountType feature services added.</returns>
        public static IServiceCollection AddAccountTypeFeature(this IServiceCollection services)
        {
            // Register command validators
            services.AddScoped<IValidator<CreateAccountTypeCommand>, CreateAccountTypeCommandValidator>();
            services.AddScoped<IValidator<UpdateAccountTypeCommand>, UpdateAccountTypeCommandValidator>();
            services.AddScoped<IValidator<DeleteAccountTypeCommand>, DeleteAccountTypeCommandValidator>();

            // Register query validators
            services.AddScoped<IValidator<GetAccountTypeByIdQuery>, GetAccountTypeByIdQueryValidator>();
            services.AddScoped<IValidator<GetAllAccountTypesQuery>, GetAllAccountTypesQueryValidator>();

            // Register DTO validators
            services.AddScoped<IValidator<AccountTypeDto>, AccountTypeDtoValidator>();
            services.AddScoped<IValidator<AccountTypeDetailsDto>, AccountTypeDetailsDtoValidator>();
            services.AddScoped<IValidator<AccountTypeListDto>, AccountTypeListDtoValidator>();

            // Register mapping profiles
            services.AddSingleton(provider => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AccountTypeMappingProfile());
            }).CreateMapper());

            // Register MediatR handlers
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblyContaining<GetAccountTypeByIdQuery>();
            });

            return services;
        }
    }
}