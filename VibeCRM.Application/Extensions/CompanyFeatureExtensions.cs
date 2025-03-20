using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using VibeCRM.Application.Features.Company.Commands.CreateCompany;
using VibeCRM.Application.Features.Company.Commands.DeleteCompany;
using VibeCRM.Application.Features.Company.Commands.UpdateCompany;
using VibeCRM.Application.Features.Company.DTOs;
using VibeCRM.Application.Features.Company.Mappings;
using VibeCRM.Application.Features.Company.Queries.GetAllCompanies;
using VibeCRM.Application.Features.Company.Queries.GetCompanyById;
using VibeCRM.Application.Features.Company.Validators;

namespace VibeCRM.Application.Extensions
{
    /// <summary>
    /// Provides extension methods for registering Company feature services in the dependency injection container.
    /// </summary>
    public static class CompanyFeatureExtensions
    {
        /// <summary>
        /// Registers all services required by the Company feature.
        /// </summary>
        /// <param name="services">The service collection to add the services to.</param>
        /// <returns>The service collection with all Company feature services added.</returns>
        public static IServiceCollection AddCompanyFeature(this IServiceCollection services)
        {
            // Register command validators
            services.AddScoped<IValidator<CreateCompanyCommand>, CreateCompanyCommandValidator>();
            services.AddScoped<IValidator<UpdateCompanyCommand>, UpdateCompanyCommandValidator>();
            services.AddScoped<IValidator<DeleteCompanyCommand>, DeleteCompanyCommandValidator>();

            // Register query validators
            services.AddScoped<IValidator<GetCompanyByIdQuery>, GetCompanyByIdQueryValidator>();
            services.AddScoped<IValidator<GetAllCompaniesQuery>, GetAllCompaniesQueryValidator>();

            // Register DTO validators
            services.AddScoped<IValidator<CompanyDto>, CompanyDtoValidator>();
            services.AddScoped<IValidator<CompanyDetailsDto>, CompanyDetailsDtoValidator>();
            services.AddScoped<IValidator<CompanyListDto>, CompanyListDtoValidator>();

            // Register mapping profiles
            services.AddSingleton(provider => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new CompanyMappingProfile());
            }).CreateMapper());

            return services;
        }
    }
}