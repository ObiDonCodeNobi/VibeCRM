using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using VibeCRM.Domain.Interfaces.Repositories.Business;
using VibeCRM.Domain.Interfaces.Repositories.Junction;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;
using VibeCRM.Infrastructure.Persistence.Database;
using VibeCRM.Infrastructure.Repositories.Business;
using VibeCRM.Infrastructure.Repositories.Junction;
using VibeCRM.Infrastructure.Repositories.TypeStatus;

namespace VibeCRM.Infrastructure
{
    /// <summary>
    /// Extension methods for configuring the Infrastructure layer dependencies
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Adds infrastructure services to the specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The service collection to add services to</param>
        /// <param name="configuration">The configuration instance</param>
        /// <returns>The same service collection so that multiple calls can be chained</returns>
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Database
            services.AddSingleton<ISQLConnectionFactory, SQLConnectionFactory>();

            // Business repositories
            services.AddScoped<IActivityDefinitionRepository, ActivityDefinitionRepository>();
            services.AddScoped<IActivityRepository, ActivityRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<IAttachmentRepository, AttachmentRepository>();
            services.AddScoped<ICallRepository, CallRepository>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<IEmailAddressRepository, EmailAddressRepository>();
            services.AddScoped<IInvoiceRepository, InvoiceRepository>();
            services.AddScoped<INoteRepository, NoteRepository>();
            services.AddScoped<IPaymentLineItemRepository, PaymentLineItemRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IPhoneRepository, PhoneRepository>();
            services.AddScoped<IProductGroupRepository, ProductGroupRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IQuoteLineItemRepository, QuoteLineItemRepository>();
            services.AddScoped<IQuoteRepository, QuoteRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<ISalesOrderLineItemRepository, SalesOrderLineItemRepository>();
            services.AddScoped<ISalesOrderRepository, SalesOrderRepository>();
            services.AddScoped<IServiceRepository, ServiceRepository>();
            services.AddScoped<ITeamRepository, TeamRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IWorkflowRepository, WorkflowRepository>();

            // Junction repositories
            services.AddScoped<IActivityAttachmentRepository, ActivityAttachmentRepository>();
            services.AddScoped<IActivityNoteRepository, ActivityNoteRepository>();
            services.AddScoped<ICompanyActivityRepository, CompanyActivityRepository>();
            services.AddScoped<ICompanyAddressRepository, CompanyAddressRepository>();
            services.AddScoped<ICompanyAttachmentRepository, CompanyAttachmentRepository>();
            services.AddScoped<ICompanyCallRepository, CompanyCallRepository>();
            services.AddScoped<ICompanyEmailAddressRepository, CompanyEmailAddressRepository>();
            services.AddScoped<ICompanyInvoiceRepository, CompanyInvoiceRepository>();
            services.AddScoped<ICompanyNoteRepository, CompanyNoteRepository>();
            services.AddScoped<ICompanyPaymentRepository, CompanyPaymentRepository>();
            services.AddScoped<ICompanyPersonRepository, CompanyPersonRepository>();
            services.AddScoped<ICompanyPhoneRepository, CompanyPhoneRepository>();
            services.AddScoped<ICompanyQuoteRepository, CompanyQuoteRepository>();
            services.AddScoped<ICompanySalesOrderRepository, CompanySalesOrderRepository>();
            services.AddScoped<IInvoiceActivityRepository, InvoiceActivityRepository>();
            services.AddScoped<IInvoiceAttachmentRepository, InvoiceAttachmentRepository>();
            services.AddScoped<IInvoiceNoteRepository, InvoiceNoteRepository>();
            services.AddScoped<IPaymentActivityRepository, PaymentActivityRepository>();
            services.AddScoped<IPaymentAttachmentRepository, PaymentAttachmentRepository>();
            services.AddScoped<IPaymentNoteRepository, PaymentNoteRepository>();
            services.AddScoped<IPaymentPaymentLineItemRepository, PaymentPaymentLineItemRepository>();
            services.AddScoped<IPersonActivityRepository, PersonActivityRepository>();
            services.AddScoped<IPersonAddressRepository, PersonAddressRepository>();
            services.AddScoped<IPersonAttachmentRepository, PersonAttachmentRepository>();
            services.AddScoped<IPersonCallRepository, PersonCallRepository>();
            services.AddScoped<IPersonEmailAddressRepository, PersonEmailAddressRepository>();
            services.AddScoped<IPersonInvoiceRepository, PersonInvoiceRepository>();
            services.AddScoped<IPersonNoteRepository, PersonNoteRepository>();
            services.AddScoped<IPersonPaymentRepository, PersonPaymentRepository>();
            services.AddScoped<IPersonPhoneRepository, PersonPhoneRepository>();
            services.AddScoped<IPersonQuoteRepository, PersonQuoteRepository>();
            services.AddScoped<IPersonSalesOrderRepository, PersonSalesOrderRepository>();
            services.AddScoped<IQuoteActivityRepository, QuoteActivityRepository>();
            services.AddScoped<IQuoteAttachmentRepository, QuoteAttachmentRepository>();
            services.AddScoped<IQuoteNoteRepository, QuoteNoteRepository>();
            services.AddScoped<IQuoteQuoteLineItemRepository, QuoteQuoteLineItemRepository>();
            services.AddScoped<ISalesOrderActivityRepository, SalesOrderActivityRepository>();
            services.AddScoped<ISalesOrderAttachmentRepository, SalesOrderAttachmentRepository>();
            services.AddScoped<ISalesOrderLineItemServiceRepository, SalesOrderLineItemServiceRepository>();
            services.AddScoped<ISalesOrderNoteRepository, SalesOrderNoteRepository>();
            services.AddScoped<ISalesOrderSalesOrderLineItemRepository, SalesOrderSalesOrderLineItemRepository>();
            services.AddScoped<ITeamUserRepository, TeamUserRepository>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            services.AddScoped<IWorkflowActivityRepository, WorkflowActivityRepository>();

            // TypeStatus repositories
            services.AddScoped<IAccountStatusRepository, AccountStatusRepository>();
            services.AddScoped<IAccountTypeRepository, AccountTypeRepository>();
            services.AddScoped<IActivityStatusRepository, ActivityStatusRepository>();
            services.AddScoped<IActivityTypeRepository, ActivityTypeRepository>();
            services.AddScoped<IAddressTypeRepository, AddressTypeRepository>();
            services.AddScoped<IAttachmentTypeRepository, AttachmentTypeRepository>();
            services.AddScoped<ICallDirectionRepository, CallDirectionRepository>();
            services.AddScoped<ICallTypeRepository, CallTypeRepository>();
            services.AddScoped<IContactTypeRepository, ContactTypeRepository>();
            services.AddScoped<IEmailAddressTypeRepository, EmailAddressTypeRepository>();
            services.AddScoped<IInvoiceStatusRepository, InvoiceStatusRepository>();
            services.AddScoped<INoteTypeRepository, NoteTypeRepository>();
            services.AddScoped<IPaymentMethodRepository, PaymentMethodRepository>();
            services.AddScoped<IPaymentStatusRepository, PaymentStatusRepository>();
            services.AddScoped<IPersonStatusRepository, PersonStatusRepository>();
            services.AddScoped<IPersonTypeRepository, PersonTypeRepository>();
            services.AddScoped<IPhoneTypeRepository, PhoneTypeRepository>();
            services.AddScoped<IProductTypeRepository, ProductTypeRepository>();
            services.AddScoped<IQuoteStatusRepository, QuoteStatusRepository>();
            services.AddScoped<ISalesOrderStatusRepository, SalesOrderStatusRepository>();
            services.AddScoped<IServiceTypeRepository, ServiceTypeRepository>();
            services.AddScoped<IShipMethodRepository, ShipMethodRepository>();
            services.AddScoped<IStateRepository, StateRepository>();
            services.AddScoped<IWorkflowTypeRepository, WorkflowTypeRepository>();

            // Add Serilog
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            // Add logging
            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.ClearProviders();
                loggingBuilder.AddSerilog(Log.Logger, dispose: true);
            });

            return services;
        }
    }
}