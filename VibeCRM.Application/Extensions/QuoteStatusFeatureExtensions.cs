using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using VibeCRM.Application.Features.QuoteStatus.Commands.CreateQuoteStatus;
using VibeCRM.Application.Features.QuoteStatus.Commands.DeleteQuoteStatus;
using VibeCRM.Application.Features.QuoteStatus.Commands.UpdateQuoteStatus;
using VibeCRM.Application.Features.QuoteStatus.Mappings;
using VibeCRM.Application.Features.QuoteStatus.Queries.GetQuoteStatusById;
using VibeCRM.Application.Features.QuoteStatus.Queries.GetAllQuoteStatuses;
using VibeCRM.Application.Features.QuoteStatus.Validators;
using VibeCRM.Shared.DTOs.QuoteStatus;

namespace VibeCRM.Application.Extensions
{
    /// <summary>
    /// Provides extension methods for registering QuoteStatus feature services in the dependency injection container.
    /// </summary>
    public static class QuoteStatusFeatureExtensions
    {
        /// <summary>
        /// Registers all services required by the QuoteStatus feature.
        /// </summary>
        /// <param name="services">The service collection to add the services to.</param>
        /// <returns>The service collection with all QuoteStatus feature services added.</returns>
        public static IServiceCollection AddQuoteStatusFeature(this IServiceCollection services)
        {
            // Register command validators
            services.AddScoped<IValidator<CreateQuoteStatusCommand>, CreateQuoteStatusCommandValidator>();
            services.AddScoped<IValidator<UpdateQuoteStatusCommand>, UpdateQuoteStatusCommandValidator>();
            services.AddScoped<IValidator<DeleteQuoteStatusCommand>, DeleteQuoteStatusCommandValidator>();

            // Register query validators
            services.AddScoped<IValidator<GetQuoteStatusByIdQuery>, GetQuoteStatusByIdQueryValidator>();
            services.AddScoped<IValidator<GetAllQuoteStatusesQuery>, GetAllQuoteStatusesQueryValidator>();

            // Register DTO validators
            services.AddScoped<IValidator<QuoteStatusDto>, QuoteStatusDtoValidator>();
            services.AddScoped<IValidator<QuoteStatusDetailsDto>, QuoteStatusDetailsDtoValidator>();
            services.AddScoped<IValidator<QuoteStatusListDto>, QuoteStatusListDtoValidator>();

            // Register mapping profiles
            services.AddSingleton(provider => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new QuoteStatusMappingProfile());
            }).CreateMapper());

            // Register MediatR handlers
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblyContaining<GetQuoteStatusByIdQuery>();
            });
            
            return services;
        }
    }
}
