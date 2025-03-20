# VibeCRM Database Table Categories

## Entity Tables
These are the main business entities in the system:

1. Activity
2. ActivityDefinition
3. Address
4. Attachment
5. Company
6. Invoice
7. InvoiceLineItem
8. Note
9. Payment
10. PaymentLineItem
11. PaymentMethod
12. Person
13. Product
14. ProductGroup
15. Quote
16. QuoteLineItem
17. Role
18. SalesOrder
19. SalesOrderLineItem
20. Service
21. Team
22. User
23. Workflow

## Type/Status Tables
These tables define types, statuses, and other reference data:

1. AccountStatus
2. AccountType
3. ActivityStatus
4. ActivityType
5. AddressType
6. AttachmentType
7. CallDirection
8. ContactType
9. InvoiceStatus
10. NoteType
11. PaymentMethodType
12. PaymentStatus
13. PaymentType
14. ProductType
15. QuoteStatus
16. SalesOrderStatus
17. ServiceType
18. ShipMethod
19. State
20. WorkflowStepType
21. WorkflowType

## Junction Tables
These tables create many-to-many relationships between entities:

1. Activity_Attachment (Activity ↔ Attachment)
2. Activity_Note (Activity ↔ Note)
3. Company_Activity (Company ↔ Activity)
4. Company_Address (Company ↔ Address)
5. Company_Attachment (Company ↔ Attachment)
6. Company_Contact (Company ↔ Contact)
7. Company_Invoice (Company ↔ Invoice)
8. Company_Note (Company ↔ Note)
9. Company_Payment (Company ↔ Payment)
10. Company_Person (Company ↔ Person)
11. Company_Quote (Company ↔ Quote)
12. Company_SalesOrder (Company ↔ SalesOrder)
13. Invoice_Activity (Invoice ↔ Activity)
14. Invoice_Attachment (Invoice ↔ Attachment)
15. Invoice_InvoiceLineItem (Invoice ↔ InvoiceLineItem)
16. Invoice_Note (Invoice ↔ Note)
17. Payment_Activity (Payment ↔ Activity)
18. Payment_Attachment (Payment ↔ Attachment)
19. Payment_Note (Payment ↔ Note)
20. Payment_PaymentLineItem (Payment ↔ PaymentLineItem)
21. Person_Activity (Person ↔ Activity)
22. Person_Address (Person ↔ Address)
23. Person_Attachment (Person ↔ Attachment)
24. Person_Contact (Person ↔ Contact)
25. Person_Invoice (Person ↔ Invoice)
26. Person_Note (Person ↔ Note)
27. Person_Payment (Person ↔ Payment)
28. Person_Quote (Person ↔ Quote)
29. Person_SalesOrder (Person ↔ SalesOrder)
30. Quote_Activity (Quote ↔ Activity)
31. Quote_Attachment (Quote ↔ Attachment)
32. Quote_Note (Quote ↔ Note)
33. Quote_QuoteLineItem (Quote ↔ QuoteLineItem)
34. SalesOrder_Activity (SalesOrder ↔ Activity)
35. SalesOrder_Attachment (SalesOrder ↔ Attachment)
36. SalesOrder_Note (SalesOrder ↔ Note)
37. SalesOrder_SalesOrderLineItem (SalesOrder ↔ SalesOrderLineItem)
38. SalesOrderLineItem_Service (SalesOrderLineItem ↔ Service)
39. Team_User (Team ↔ User)
40. User_Role (User ↔ Role)
41. Workflow_Activity (Workflow ↔ Activity)
42. WorkflowStep_Activity (WorkflowStep ↔ Activity)
