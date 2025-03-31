using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using VibeCRM.Application.DependencyInjection;

namespace VibeCRM.Application.Extensions
{
    /// <summary>
    /// Provides extension methods for registering application services in the dependency injection container.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers all application services required by the VibeCRM system.
        /// </summary>
        /// <param name="services">The service collection to add the services to.</param>
        /// <returns>The service collection with all application services added.</returns>
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Register MediatR for all handlers in the application assembly
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            // Register feature-specific services
            services.AddAccountStatusFeature();
            services.AddAccountTypeFeature();
            services.AddActivityDefinitionFeature();
            services.AddActivityFeature();
            services.AddActivityStatusFeature();
            services.AddActivityTypeFeature();
            services.AddAddressFeature();
            services.AddAddressTypeFeature();
            services.AddAttachmentFeature();
            services.AddAttachmentTypeFeature();
            services.AddCallFeature();
            services.AddCallDirectionFeature();
            services.AddCallTypeFeature();
            services.AddCompanyFeature();
            services.AddContactTypeFeature();
            services.AddEmailAddressFeature();
            services.AddEmailAddressTypeFeature();
            services.AddInvoiceFeature();
            //services.AddInvoiceLineItemFeature();
            services.AddInvoiceStatusFeature();
            services.AddNoteFeature();
            services.AddNoteTypeFeature();
            services.AddPaymentFeature();
            services.AddPaymentLineItemFeature();
            services.AddPaymentMethodFeature();
            services.AddPaymentStatusFeature();
            services.AddPersonFeature();
            services.AddPersonStatusFeature();
            services.AddPersonTypeFeature();
            services.AddPhoneFeature();
            services.AddPhoneTypeFeature();
            services.AddProductFeature();
            services.AddProductGroupFeature();
            services.AddProductTypeFeature();
            services.AddQuoteFeature();
            services.AddQuoteLineItemFeature();
            services.AddQuoteStatusFeature();
            services.AddRoleFeature();
            services.AddSalesOrderFeature();
            services.AddSalesOrderLineItemFeature();
            services.AddSalesOrderStatusFeature();
            services.AddServiceFeature();
            services.AddServiceTypeFeature();
            services.AddShipMethodFeature();
            services.AddStateFeature();
            services.AddTeamFeature();
            services.AddUserFeature();
            services.AddWorkflowFeature();
            //services.AddWorkflowTypeFeature();

            return services;
        }
    }
}