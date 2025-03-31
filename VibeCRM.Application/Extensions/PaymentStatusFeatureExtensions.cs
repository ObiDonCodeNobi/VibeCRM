using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using VibeCRM.Application.Features.PaymentStatus.Commands.CreatePaymentStatus;
using VibeCRM.Application.Features.PaymentStatus.Commands.DeletePaymentStatus;
using VibeCRM.Application.Features.PaymentStatus.Commands.UpdatePaymentStatus;
using VibeCRM.Application.Features.PaymentStatus.Mappings;
using VibeCRM.Application.Features.PaymentStatus.Queries.GetPaymentStatusById;
using VibeCRM.Application.Features.PaymentStatus.Queries.GetAllPaymentStatuses;
using VibeCRM.Application.Features.PaymentStatus.Validators;
using VibeCRM.Shared.DTOs.PaymentStatus;
using MediatR;

namespace VibeCRM.Application.Extensions
{
    /// <summary>
    /// Provides extension methods for registering PaymentStatus feature services in the dependency injection container.
    /// </summary>
    public static class PaymentStatusFeatureExtensions
    {
        /// <summary>
        /// Registers all services required by the PaymentStatus feature.
        /// </summary>
        /// <param name="services">The service collection to add the services to.</param>
        /// <returns>The service collection with all PaymentStatus feature services added.</returns>
        public static IServiceCollection AddPaymentStatusFeature(this IServiceCollection services)
        {
            // Register command validators
            services.AddScoped<IValidator<CreatePaymentStatusCommand>, CreatePaymentStatusCommandValidator>();
            services.AddScoped<IValidator<UpdatePaymentStatusCommand>, UpdatePaymentStatusCommandValidator>();
            services.AddScoped<IValidator<DeletePaymentStatusCommand>, DeletePaymentStatusCommandValidator>();

            // Register query validators
            services.AddScoped<IValidator<GetPaymentStatusByIdQuery>, GetPaymentStatusByIdQueryValidator>();
            services.AddScoped<IValidator<GetAllPaymentStatusesQuery>, GetAllPaymentStatusesQueryValidator>();

            // Register DTO validators
            services.AddScoped<IValidator<PaymentStatusDto>, PaymentStatusDtoValidator>();
            services.AddScoped<IValidator<PaymentStatusDetailsDto>, PaymentStatusDetailsDtoValidator>();
            services.AddScoped<IValidator<PaymentStatusListDto>, PaymentStatusListDtoValidator>();

            // Register mapping profiles
            services.AddSingleton(provider => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new PaymentStatusMappingProfile());
            }).CreateMapper());

            // Register MediatR handlers
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblyContaining<GetPaymentStatusByIdQuery>();
            });

            return services;
        }
    }
}
