using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using VibeCRM.Application.Features.PaymentMethod.Commands.CreatePaymentMethod;
using VibeCRM.Application.Features.PaymentMethod.Commands.DeletePaymentMethod;
using VibeCRM.Application.Features.PaymentMethod.Commands.UpdatePaymentMethod;
using VibeCRM.Application.Features.PaymentMethod.Mappings;
using VibeCRM.Application.Features.PaymentMethod.Queries.GetPaymentMethodById;
using VibeCRM.Application.Features.PaymentMethod.Queries.GetAllPaymentMethods;
using VibeCRM.Application.Features.PaymentMethod.Validators;
using VibeCRM.Shared.DTOs.PaymentMethod;
using MediatR;

namespace VibeCRM.Application.Extensions
{
    /// <summary>
    /// Provides extension methods for registering PaymentMethod feature services in the dependency injection container.
    /// </summary>
    public static class PaymentMethodFeatureExtensions
    {
        /// <summary>
        /// Registers all services required by the PaymentMethod feature.
        /// </summary>
        /// <param name="services">The service collection to add the services to.</param>
        /// <returns>The service collection with all PaymentMethod feature services added.</returns>
        public static IServiceCollection AddPaymentMethodFeature(this IServiceCollection services)
        {
            // Register command validators
            services.AddScoped<IValidator<CreatePaymentMethodCommand>, CreatePaymentMethodCommandValidator>();
            services.AddScoped<IValidator<UpdatePaymentMethodCommand>, UpdatePaymentMethodCommandValidator>();
            services.AddScoped<IValidator<DeletePaymentMethodCommand>, DeletePaymentMethodCommandValidator>();

            // Register query validators
            services.AddScoped<IValidator<GetPaymentMethodByIdQuery>, GetPaymentMethodByIdQueryValidator>();
            services.AddScoped<IValidator<GetAllPaymentMethodsQuery>, GetAllPaymentMethodsQueryValidator>();

            // Register DTO validators
            services.AddScoped<IValidator<PaymentMethodDto>, PaymentMethodDtoValidator>();
            services.AddScoped<IValidator<PaymentMethodDetailsDto>, PaymentMethodDetailsDtoValidator>();
            services.AddScoped<IValidator<PaymentMethodListDto>, PaymentMethodListDtoValidator>();

            // Register mapping profiles
            services.AddSingleton(provider => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new PaymentMethodMappingProfile());
            }).CreateMapper());

            // Register MediatR handlers
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblyContaining<GetPaymentMethodByIdQuery>();
            });
            
            return services;
        }
    }
}
