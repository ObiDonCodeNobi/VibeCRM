using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using VibeCRM.Application.Features.Quote.Commands.CreateQuote;
using VibeCRM.Application.Features.Quote.Commands.DeleteQuote;
using VibeCRM.Application.Features.Quote.Commands.UpdateQuote;
using VibeCRM.Application.Features.Quote.DTOs;
using VibeCRM.Application.Features.Quote.Mappings;
using VibeCRM.Application.Features.Quote.Queries.GetAllQuotes;
using VibeCRM.Application.Features.Quote.Queries.GetQuoteById;
using VibeCRM.Application.Features.Quote.Validators;

namespace VibeCRM.Application.Extensions
{
    /// <summary>
    /// Provides extension methods for registering Quote feature services in the dependency injection container.
    /// </summary>
    public static class QuoteFeatureExtensions
    {
        /// <summary>
        /// Registers all services required by the Quote feature.
        /// </summary>
        /// <param name="services">The service collection to add the services to.</param>
        /// <returns>The service collection with all Quote feature services added.</returns>
        public static IServiceCollection AddQuoteFeature(this IServiceCollection services)
        {
            // Register command validators
            services.AddScoped<IValidator<CreateQuoteCommand>, CreateQuoteCommandValidator>();
            services.AddScoped<IValidator<UpdateQuoteCommand>, UpdateQuoteCommandValidator>();
            services.AddScoped<IValidator<DeleteQuoteCommand>, DeleteQuoteCommandValidator>();

            // Register query validators
            services.AddScoped<IValidator<GetQuoteByIdQuery>, GetQuoteByIdQueryValidator>();
            services.AddScoped<IValidator<GetAllQuotesQuery>, GetAllQuotesQueryValidator>();

            // Register DTO validators
            services.AddScoped<IValidator<QuoteDto>, QuoteDtoValidator>();
            services.AddScoped<IValidator<QuoteDetailsDto>, QuoteDetailsDtoValidator>();
            services.AddScoped<IValidator<QuoteListDto>, QuoteListDtoValidator>();

            // Register mapping profiles
            services.AddSingleton(provider => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new QuoteMappingProfile());
            }).CreateMapper());

            // Register MediatR handlers
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblyContaining<GetQuoteByIdQuery>();
            });

            return services;
        }
    }
}