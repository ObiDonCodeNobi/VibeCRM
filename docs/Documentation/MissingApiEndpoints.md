# Missing API Endpoints

This document lists all identified missing API endpoints based on the CQRS application layer features. These endpoints need to be implemented to fully expose all the available commands and queries in the API.

## AccountType

*Note: All AccountType endpoints have been implemented.*

## AddressType

*Note: All AddressType endpoints have been implemented.*

## AttachmentType

*Note: All AttachmentType endpoints have been implemented.*

## EmailAddressType

*Note: All EmailAddressType endpoints have been implemented.*

## Note

*Note: All Note endpoints have been implemented.*

## NoteType

*Note: All NoteType endpoints have been implemented.*

## PaymentMethod

*Note: All PaymentMethod endpoints have been implemented.*

## PaymentStatus

*Note: All PaymentStatus endpoints have been implemented.*

## Missing Controllers for Existing Features

The following features have handlers in the application layer, but no corresponding controllers in the API project:

### PhoneType

*Note: All PhoneType endpoints have been implemented.*

### ProductGroup

*Note: All ProductGroup endpoints have been implemented.*

### ProductType

*Note: All ProductType endpoints have been implemented.*

### QuoteLineItem

*Note: All QuoteLineItem endpoints have been implemented.*

### SalesOrderLineItem

*Note: All SalesOrderLineItem endpoints have been implemented.*

### ServiceType

*Note: All ServiceType endpoints have been implemented.*

## Potential Missing Features

The following features have handlers in the application layer, but no corresponding controllers in the API project:

1. **PhoneType** - Fully implemented.
2. **ProductGroup** - Fully implemented.
3. **ProductType** - Fully implemented.
4. **QuoteLineItem** - Fully implemented.
5. **SalesOrderLineItem** - Fully implemented.
6. **ServiceType** - Fully implemented.

## Summary of Missing API Endpoints

In total, we've identified:
- 0 missing controllers that need to be implemented
- 0 missing API endpoints across this controller

The most complex controllers to implement would be:
 None

The simplest controllers to implement would be:
 None

## Implementation Status

The following controllers have been implemented:

1. **PaymentMethodController** - Implemented with 8 endpoints:
   - Create, Update, Delete, GetById, GetAll, GetByName, GetByOrdinalPosition, GetDefault

2. **PaymentStatusController** - Implemented with 6 endpoints:
   - Create, Update, Delete, GetById, GetAll, GetByStatus

3. **PhoneTypeController** - Implemented with 8 endpoints:
   - Create, Update, Delete, GetById, GetAll, GetByType, GetByOrdinalPosition, GetDefault

4. **ProductGroupController** - Implemented with 7 endpoints:
   - Create, Update, Delete, GetById, GetAll, GetByParentId, GetRoot

5. **ProductTypeController** - Implemented with 8 endpoints:
   - Create, Update, Delete, GetById, GetAll, GetByType, GetByOrdinalPosition, GetDefault

6. **QuoteLineItemController** - Implemented with 10 endpoints:
   - Create, Update, Delete, GetById, GetAll, GetByQuote, GetByProduct, GetByService, GetByDateRange, GetTotalForQuote

7. **SalesOrderLineItemController** - Implemented with 9 endpoints:
   - Create, Update, Delete, GetById, GetAll, GetBySalesOrder, GetByProduct, GetByService, GetTotalForSalesOrder

8. **ServiceTypeController** - Implemented with 7 endpoints:
   - Create, Update, Delete, GetById, GetAll, GetByType, GetDefault

9. **AccountStatusController** - Implemented with 7 endpoints:
   - Create, Update, Delete, GetById, GetAll, GetByStatus, GetByOrdinalPosition

10. **AddressController** - Implemented with 5 endpoints:
    - Create, Update, Delete, GetById, GetAll

11. **AddressTypeController** - Updated with 8 endpoints:
    - Create, Update, Delete, GetById, GetAll, GetByType, GetByOrdinalPosition, GetDefault

12. **EmailAddressTypeController** - Updated with 8 endpoints:
    - Create, Update, Delete, GetById, GetAll, GetByType, GetByOrdinalPosition, GetDefault

13. **AccountTypeController** - Updated with 7 endpoints:
    - Create, Update, Delete, GetById, GetAll, GetByType, GetByOrdinalPosition

14. **PhoneController** - Implemented with 12 endpoints:
    - Create, Update, Delete, GetById, GetAll, GetByCompany, GetByPerson, GetByPhoneType, SearchByNumber, AddToCompany, RemoveFromCompany, AddToPerson, RemoveFromPerson

15. **EmailAddressController** - Implemented with 7 endpoints:
    - Create, Update, Delete, GetById, GetAll, GetByType, Search

16. **StateController** - Implemented with 8 endpoints:
    - Create, Update, Delete, GetById, GetAll, GetByName, GetByAbbreviation, GetDefault

17. **AttachmentTypeController** - Implemented with 9 endpoints:
    - Create, Update, Delete, GetById, GetAll, GetByType, GetByFileExtension, GetByOrdinalPosition, GetDefault

18. **NoteController** - Implemented with 8 endpoints:
    - Create, Update, Delete, GetById, GetAll, GetByType, GetByOrdinalPosition, GetDefault

19. **NoteTypeController** - Implemented with 8 endpoints:
    - Create, Update, Delete, GetById, GetAll, GetByType, GetByOrdinalPosition, GetDefault

## Implementation Recommendations

When implementing the missing endpoints, follow these guidelines:

1. Use the existing controller patterns for consistency
2. Ensure proper XML documentation for all endpoints
3. Include appropriate response types and status codes
4. Implement validation using FluentValidation
5. Follow the CQRS pattern with MediatR
6. Ensure all endpoints return standardized `ApiResponse` objects
7. Include comprehensive Swagger documentation

## Next Steps

1. Review and update this document as endpoints are implemented
