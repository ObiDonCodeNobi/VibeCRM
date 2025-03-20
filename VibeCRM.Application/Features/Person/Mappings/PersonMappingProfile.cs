using AutoMapper;
using VibeCRM.Application.Features.Person.Commands.CreatePerson;
using VibeCRM.Application.Features.Person.Commands.UpdatePerson;
using VibeCRM.Application.Features.Person.DTOs;
using VibeCRM.Domain.Entities.JunctionEntities;

namespace VibeCRM.Application.Features.Person.Mappings
{
    /// <summary>
    /// AutoMapper profile for mapping between Person entities and related DTOs.
    /// Defines mapping configurations for all Person-related objects.
    /// </summary>
    public class PersonMappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PersonMappingProfile"/> class
        /// and configures the mapping relationships between entities and DTOs.
        /// </summary>
        public PersonMappingProfile()
        {
            // Entity to DTO mappings
            CreateMap<VibeCRM.Domain.Entities.BusinessEntities.Person, PersonDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PersonId));

            CreateMap<VibeCRM.Domain.Entities.BusinessEntities.Person, PersonDetailsDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PersonId))
                .ForMember(dest => dest.Companies, opt => opt.MapFrom(src => src.Companies))
                .ForMember(dest => dest.Addresses, opt => opt.MapFrom(src => src.Addresses))
                .ForMember(dest => dest.PhoneNumbers, opt => opt.MapFrom(src => src.PhoneNumbers))
                .ForMember(dest => dest.EmailAddresses, opt => opt.MapFrom(src => src.EmailAddresses))
                .ForMember(dest => dest.Activities, opt => opt.MapFrom(src => src.Activities))
                .ForMember(dest => dest.Attachments, opt => opt.MapFrom(src => src.Attachments))
                .ForMember(dest => dest.Notes, opt => opt.MapFrom(src => src.PersonNotes))
                .ForMember(dest => dest.Calls, opt => opt.MapFrom(src => src.Calls));

            CreateMap<VibeCRM.Domain.Entities.BusinessEntities.Person, PersonListDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PersonId))
                .ForMember(dest => dest.PrimaryCompanyName, opt => opt.Ignore())
                .ForMember(dest => dest.PrimaryEmail, opt => opt.Ignore())
                .ForMember(dest => dest.PrimaryPhone, opt => opt.Ignore())
                .ForMember(dest => dest.PrimaryAddress, opt => opt.Ignore());

            // Join entity to DTO mappings
            CreateMap<Company_Person, CompanyReferenceDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Company != null ? src.Company.CompanyId : Guid.Empty))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Company != null ? src.Company.Name : string.Empty));

            CreateMap<Person_Address, AddressReferenceDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Address != null ? src.Address.AddressId : Guid.Empty))
                .ForMember(dest => dest.AddressLine1, opt => opt.MapFrom(src => src.Address != null ? src.Address.Line1 : string.Empty))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address != null ? src.Address.City : string.Empty))
                .ForMember(dest => dest.StateProvince, opt => opt.MapFrom(src => src.Address != null && src.Address.State != null ? src.Address.State.Name : string.Empty))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.Address != null ? src.Address.Zip : string.Empty));

            CreateMap<Person_Phone, PhoneReferenceDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Phone != null ? src.Phone.PhoneId : Guid.Empty))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Phone != null ? src.Phone.FormattedPhoneNumber : string.Empty))
                .ForMember(dest => dest.PhoneType, opt => opt.MapFrom(src => src.Phone != null && src.Phone.PhoneType != null ? src.Phone.PhoneType.Type : string.Empty));

            CreateMap<Person_EmailAddress, EmailAddressReferenceDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.EmailAddress != null ? src.EmailAddress.EmailAddressId : Guid.Empty))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.EmailAddress != null ? src.EmailAddress.Address : string.Empty))
                .ForMember(dest => dest.EmailType, opt => opt.MapFrom(src => src.EmailAddress != null && src.EmailAddress.EmailAddressType != null ? src.EmailAddress.EmailAddressType.Type : string.Empty));

            CreateMap<Person_Activity, ActivityReferenceDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Activity != null ? src.Activity.ActivityId : Guid.Empty))
                .ForMember(dest => dest.Subject, opt => opt.MapFrom(src => src.Activity != null ? src.Activity.Subject : string.Empty))
                .ForMember(dest => dest.ActivityType, opt => opt.MapFrom(src => src.Activity != null && src.Activity.ActivityType != null ? src.Activity.ActivityType.Type : string.Empty))
                .ForMember(dest => dest.ScheduledDateTime, opt => opt.MapFrom(src => src.Activity != null ? src.Activity.StartDate : DateTime.MinValue));

            CreateMap<Person_Attachment, AttachmentReferenceDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Attachment != null ? src.Attachment.AttachmentId : Guid.Empty))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Attachment != null ? src.Attachment.Subject : string.Empty))
                .ForMember(dest => dest.FileType, opt => opt.MapFrom(src => src.Attachment != null ? src.Attachment.Filename : string.Empty))
                .ForMember(dest => dest.AttachmentType, opt => opt.MapFrom(src => src.Attachment != null && src.Attachment.AttachmentType != null ? src.Attachment.AttachmentType.Type : string.Empty));

            CreateMap<Person_Note, NoteReferenceDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Note != null ? src.Note.NoteId : Guid.Empty))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Note != null ? src.Note.Subject : string.Empty))
                .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Note != null ? src.Note.NoteText : string.Empty))
                .ForMember(dest => dest.NoteType, opt => opt.MapFrom(src => src.Note != null && src.Note.NoteType != null ? src.Note.NoteType.Type : string.Empty));

            CreateMap<Person_Call, CallReferenceDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Call != null ? src.Call.CallId : Guid.Empty))
                .ForMember(dest => dest.Subject, opt => opt.MapFrom(src => src.Call != null ? src.Call.Description : string.Empty))
                .ForMember(dest => dest.CallType, opt => opt.MapFrom(src => "Call")) // Placeholder until we have a proper CallType entity
                .ForMember(dest => dest.CallDateTime, opt => opt.MapFrom(src => src.Call != null ? src.Call.CreatedDate : DateTime.MinValue))
                .ForMember(dest => dest.DurationMinutes, opt => opt.MapFrom(src => src.Call != null ? src.Call.Duration / 60 : 0)); // Convert seconds to minutes

            // Command to Entity mappings
            CreateMap<CreatePersonCommand, VibeCRM.Domain.Entities.BusinessEntities.Person>()
                .ForMember(dest => dest.PersonId, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.Active, opt => opt.MapFrom(src => true))
                .ForMember(dest => dest.Companies, opt => opt.Ignore())
                .ForMember(dest => dest.Addresses, opt => opt.Ignore())
                .ForMember(dest => dest.PhoneNumbers, opt => opt.Ignore())
                .ForMember(dest => dest.EmailAddresses, opt => opt.Ignore())
                .ForMember(dest => dest.Activities, opt => opt.Ignore())
                .ForMember(dest => dest.Attachments, opt => opt.Ignore())
                .ForMember(dest => dest.PersonNotes, opt => opt.Ignore())
                .ForMember(dest => dest.Calls, opt => opt.Ignore());

            CreateMap<UpdatePersonCommand, VibeCRM.Domain.Entities.BusinessEntities.Person>()
                .ForMember(dest => dest.PersonId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.Active, opt => opt.Ignore())
                .ForMember(dest => dest.Companies, opt => opt.Ignore())
                .ForMember(dest => dest.Addresses, opt => opt.Ignore())
                .ForMember(dest => dest.PhoneNumbers, opt => opt.Ignore())
                .ForMember(dest => dest.EmailAddresses, opt => opt.Ignore())
                .ForMember(dest => dest.Activities, opt => opt.Ignore())
                .ForMember(dest => dest.Attachments, opt => opt.Ignore())
                .ForMember(dest => dest.PersonNotes, opt => opt.Ignore())
                .ForMember(dest => dest.Calls, opt => opt.Ignore());
        }
    }
}