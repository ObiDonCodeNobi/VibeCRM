using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using VibeCRM.Application.Features.PaymentLineItem.Commands.CreatePaymentLineItem;
using VibeCRM.Application.Features.PaymentLineItem.Commands.DeletePaymentLineItem;
using VibeCRM.Application.Features.PaymentLineItem.Commands.UpdatePaymentLineItem;
using VibeCRM.Application.Features.PaymentLineItem.DTOs;
using VibeCRM.Application.Features.PaymentLineItem.Mappings;
using VibeCRM.Application.Features.PaymentLineItem.Queries.GetAllPaymentLineItems;
using VibeCRM.Application.Features.PaymentLineItem.Queries.GetPaymentLineItemById;
using VibeCRM.Application.Features.PaymentLineItem.Validators;

namespace VibeCRM.Application.Extensions
{
    /// <summary>
    /// Provides extension methods for registering PaymentLineItem feature services in the dependency injection container.
    /// </summary>
    public static class PaymentLineItemFeatureExtensions
    {
        /// <summary>
        /// Registers all services required by the PaymentLineItem feature.
        /// </summary>
        /// <param name="services">The service collection to add the services to.</param>
        /// <returns>The service collection with all PaymentLineItem feature services added.</returns>
        public static IServiceCollection AddPaymentLineItemFeature(this IServiceCollection services)
        {
            // Register command validators
            services.AddScoped<IValidator<CreatePaymentLineItemCommand>, CreatePaymentLineItemCommandValidator>();
            services.AddScoped<IValidator<UpdatePaymentLineItemCommand>, UpdatePaymentLineItemCommandValidator>();
            services.AddScoped<IValidator<DeletePaymentLineItemCommand>, DeletePaymentLineItemCommandValidator>();

            // Register query validators
            services.AddScoped<IValidator<GetPaymentLineItemByIdQuery>, GetPaymentLineItemByIdQueryValidator>();
            services.AddScoped<IValidator<GetAllPaymentLineItemsQuery>, GetAllPaymentLineItemsQueryValidator>();

            // Register DTO validators
            services.AddScoped<IValidator<PaymentLineItemDto>, PaymentLineItemDtoValidator>();
            services.AddScoped<IValidator<PaymentLineItemDetailsDto>, PaymentLineItemDetailsDtoValidator>();

            // Register mapping profiles
            services.AddSingleton(provider => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new PaymentLineItemMappingProfile());
            }).CreateMapper());

            return services;
        }
    }
}