using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using VibeCRM.Application.Features.Payment.Commands.CreatePayment;
using VibeCRM.Application.Features.Payment.Commands.DeletePayment;
using VibeCRM.Application.Features.Payment.Commands.UpdatePayment;
using VibeCRM.Application.Features.Payment.Mappings;
using VibeCRM.Application.Features.Payment.Queries.GetAllPayments;
using VibeCRM.Application.Features.Payment.Queries.GetPaymentById;
using VibeCRM.Application.Features.Payment.Validators;
using VibeCRM.Shared.DTOs.Payment;

namespace VibeCRM.Application.Extensions
{
    /// <summary>
    /// Provides extension methods for registering Payment feature services in the dependency injection container.
    /// </summary>
    public static class PaymentFeatureExtensions
    {
        /// <summary>
        /// Registers all services required by the Payment feature.
        /// </summary>
        /// <param name="services">The service collection to add the services to.</param>
        /// <returns>The service collection with all Payment feature services added.</returns>
        public static IServiceCollection AddPaymentFeature(this IServiceCollection services)
        {
            // Register command validators
            services.AddScoped<IValidator<CreatePaymentCommand>, CreatePaymentCommandValidator>();
            services.AddScoped<IValidator<UpdatePaymentCommand>, UpdatePaymentCommandValidator>();
            services.AddScoped<IValidator<DeletePaymentCommand>, DeletePaymentCommandValidator>();

            // Register query validators
            services.AddScoped<IValidator<GetPaymentByIdQuery>, GetPaymentByIdQueryValidator>();
            services.AddScoped<IValidator<GetAllPaymentsQuery>, GetAllPaymentsQueryValidator>();

            // Register DTO validators
            services.AddScoped<IValidator<PaymentDto>, PaymentDtoValidator>();
            services.AddScoped<IValidator<PaymentDetailsDto>, PaymentDetailsDtoValidator>();

            // Register mapping profiles
            services.AddSingleton(provider => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new PaymentMappingProfile());
            }).CreateMapper());

            return services;
        }
    }
}