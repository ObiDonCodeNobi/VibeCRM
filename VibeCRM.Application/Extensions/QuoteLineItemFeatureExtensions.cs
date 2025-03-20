using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using VibeCRM.Application.Features.QuoteLineItem.Commands.CreateQuoteLineItem;
using VibeCRM.Application.Features.QuoteLineItem.Commands.DeleteQuoteLineItem;
using VibeCRM.Application.Features.QuoteLineItem.Commands.UpdateQuoteLineItem;
using VibeCRM.Application.Features.QuoteLineItem.DTOs;
using VibeCRM.Application.Features.QuoteLineItem.Mappings;
using VibeCRM.Application.Features.QuoteLineItem.Queries.GetAllQuoteLineItems;
using VibeCRM.Application.Features.QuoteLineItem.Queries.GetQuoteLineItemById;
using VibeCRM.Application.Features.QuoteLineItem.Validators;

namespace VibeCRM.Application.Extensions
{
    /// <summary>
    /// Provides extension methods for registering QuoteLineItem feature services in the dependency injection container.
    /// </summary>
    public static class QuoteLineItemFeatureExtensions
    {
        /// <summary>
        /// Registers all services required by the QuoteLineItem feature.
        /// </summary>
        /// <param name="services">The service collection to add the services to.</param>
        /// <returns>The service collection with all QuoteLineItem feature services added.</returns>
        public static IServiceCollection AddQuoteLineItemFeature(this IServiceCollection services)
        {
            // Register command validators
            services.AddScoped<IValidator<CreateQuoteLineItemCommand>, CreateQuoteLineItemCommandValidator>();
            services.AddScoped<IValidator<UpdateQuoteLineItemCommand>, UpdateQuoteLineItemCommandValidator>();
            services.AddScoped<IValidator<DeleteQuoteLineItemCommand>, DeleteQuoteLineItemCommandValidator>();

            // Register query validators
            services.AddScoped<IValidator<GetQuoteLineItemByIdQuery>, GetQuoteLineItemByIdQueryValidator>();
            services.AddScoped<IValidator<GetAllQuoteLineItemsQuery>, GetAllQuoteLineItemsQueryValidator>();

            // Register DTO validators
            services.AddScoped<IValidator<QuoteLineItemDto>, QuoteLineItemDtoValidator>();
            services.AddScoped<IValidator<QuoteLineItemDetailsDto>, QuoteLineItemDetailsDtoValidator>();
            services.AddScoped<IValidator<QuoteLineItemListDto>, QuoteLineItemListDtoValidator>();

            // Register mapping profiles
            services.AddSingleton(provider => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new QuoteLineItemMappingProfile());
            }).CreateMapper());

            // Register MediatR handlers
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblyContaining<GetQuoteLineItemByIdQuery>();
            });

            return services;
        }
    }
}