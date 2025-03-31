//using AutoMapper;
//using FluentValidation;using Microsoft.Extensions.DependencyInjection;
//using VibeCRM.Application.Features.InvoiceLineItem.Commands.CreateInvoiceLineItem;
//using VibeCRM.Application.Features.InvoiceLineItem.Commands.DeleteInvoiceLineItem;
//using VibeCRM.Application.Features.InvoiceLineItem.Commands.UpdateInvoiceLineItem;
//using VibeCRM.Application.Features.InvoiceLineItem.Mappings;
//using VibeCRM.Application.Features.InvoiceLineItem.Queries.GetInvoiceLineItemById;
//using VibeCRM.Application.Features.InvoiceLineItem.Queries.GetAllInvoiceLineItems;
//using VibeCRM.Application.Features.InvoiceLineItem.Validators;
//using VibeCRM.Shared.DTOs.InvoiceLineItem;

//namespace VibeCRM.Application.Extensions
//{
//    /// <summary>
//    /// Provides extension methods for registering InvoiceLineItem feature services in the dependency injection container.
//    /// </summary>
//    public static class InvoiceLineItemFeatureExtensions
//    {
//        /// <summary>
//        /// Registers all services required by the InvoiceLineItem feature.
//        /// </summary>
//        /// <param name="services">The service collection to add the services to.</param>
//        /// <returns>The service collection with all InvoiceLineItem feature services added.</returns>
//        public static IServiceCollection AddInvoiceLineItemFeature(this IServiceCollection services)
//        {
//            // Register command validators
//            services.AddScoped<IValidator<CreateInvoiceLineItemCommand>, CreateInvoiceLineItemCommandValidator>();
//            services.AddScoped<IValidator<UpdateInvoiceLineItemCommand>, UpdateInvoiceLineItemCommandValidator>();
//            services.AddScoped<IValidator<DeleteInvoiceLineItemCommand>, DeleteInvoiceLineItemCommandValidator>();

//            // Register query validators
//            services.AddScoped<IValidator<GetInvoiceLineItemByIdQuery>, GetInvoiceLineItemByIdQueryValidator>();
//            services.AddScoped<IValidator<GetAllInvoiceLineItemsQuery>, GetAllInvoiceLineItemsQueryValidator>();

//            // Register DTO validators
//            services.AddScoped<IValidator<InvoiceLineItemDto>, InvoiceLineItemDtoValidator>();
//            services.AddScoped<IValidator<InvoiceLineItemDetailsDto>, InvoiceLineItemDetailsDtoValidator>();
//            services.AddScoped<IValidator<InvoiceLineItemListDto>, InvoiceLineItemListDtoValidator>();

//            // Register mapping profiles
//            services.AddSingleton(provider => new MapperConfiguration(cfg =>
//            {
//                cfg.AddProfile(new InvoiceLineItemMappingProfile());
//            }).CreateMapper());

//            // Register MediatR handlers
//            services.AddMediatR(cfg =>
//            {
//                cfg.RegisterServicesFromAssemblyContaining<GetInvoiceLineItemByIdQuery>();
//            });
            
//            return services;
//        }
//    }
//}
