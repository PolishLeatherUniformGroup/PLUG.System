using AutoMapper;
using ONPA.Gatherings.Api.Application.Commands;
using ONPA.Gatherings.Api.Application.Queries;
using ONPA.Gatherings.Contract.Requests;
using PLUG.System.SharedDomain;

namespace ONPA.Gatherings.Api.Maps;

public class EventsMap : Profile
{
    public EventsMap()
    {
        this.CreateMap<CreateEventRequest, CreateEventCommand>()
            .ForCtorParam(nameof(CreateEventCommand.Name), opt => opt.MapFrom(src => src.Name))
            .ForCtorParam(nameof(CreateEventCommand.Description), opt => opt.MapFrom(src => src.Description))
            .ForCtorParam(nameof(CreateEventCommand.Regulations), opt => opt.MapFrom(src => src.Regulations))
            .ForCtorParam(nameof(CreateEventCommand.ScheduledStart), opt => opt.MapFrom(src => src.ScheduledStart))
            .ForCtorParam(nameof(CreateEventCommand.PlannedCapacity), opt => opt.MapFrom(src => src.PlannedCapacity))
            .ForCtorParam(nameof(CreateEventCommand.PricePerPerson), opt => opt.MapFrom(src => new Money(src.PricePerPerson, src.Currency)))
            .ForCtorParam(nameof(CreateEventCommand.PublishDate), opt => opt.MapFrom(src => src.PublishDate))
            .ForCtorParam(nameof(CreateEventCommand.EnrollmentDeadline), opt => opt.MapFrom(src => src.EnrollmentDeadline));

        this.CreateMap<UpdateEventCapacityRequest, ModifyEventCapacityCommand>()
            .ForCtorParam(nameof(ModifyEventCapacityCommand.EventId), opt => opt.MapFrom(src => src.EventId))
            .ForCtorParam(nameof(ModifyEventCapacityCommand.PlannedCapacity), opt => opt.MapFrom(src => src.Capacity.PlannedCapacity));

        this.CreateMap<ArchiveEventRequest, ArchiveEventCommand>()
            .ForCtorParam(nameof(ArchiveEventCommand.EventId), opt => opt.MapFrom(src => src.EventId));

        this.CreateMap<AcceptEventRequest, AcceptEventCommand>()
            .ForCtorParam(nameof(AcceptEventCommand.EventId), opt => opt.MapFrom(src => src.EventId));
        
        this.CreateMap<PublishEventRequest, PublishEventCommand>()
            .ForCtorParam(nameof(PublishEventCommand.EventId), opt => opt.MapFrom(src => src.EventId));

        this.CreateMap<CancelEventRequest, CancelEventCommand>()
            .ForCtorParam(nameof(CancelEventCommand.EventId), opt => opt.MapFrom(src => src.EventId))
            .ForCtorParam(nameof(CancelEventCommand.CancellationDate), opt => opt.MapFrom(src => src.Cancellation.CancellationDate))
            .ForCtorParam(nameof(CancelEventCommand.CancellationReason), opt => opt.MapFrom(src => src.Cancellation.CancellationReason));
        
        this.CreateMap<UpdateEventDescriptionRequest, ModifyEventDescriptionCommand>()
            .ForCtorParam(nameof(ModifyEventDescriptionCommand.EventId), opt => opt.MapFrom(src => src.EventId))
            .ForCtorParam(nameof(ModifyEventDescriptionCommand.Description), opt => opt.MapFrom(src => src.Description.Description))
            .ForCtorParam(nameof(ModifyEventDescriptionCommand.Name), opt => opt.MapFrom(src => src.Description.Name))
            .ForCtorParam(nameof(ModifyEventDescriptionCommand.Regulations), opt => opt.MapFrom(src => src.Description.Regulations));
        
        this.CreateMap<UpdateEventPriceRequest, ModifyEventPriceCommand>()
            .ForCtorParam(nameof(ModifyEventPriceCommand.EventId), opt => opt.MapFrom(src => src.EventId))
            .ForCtorParam(nameof(ModifyEventPriceCommand.PricePerPerson), opt => opt.MapFrom(src => new Money(src.Price.PricePerPerson, src.Price.Currency)));
        
        this.CreateMap<UpdateEventScheduleRequest, ModifyEventScheduleCommand>()
            .ForCtorParam(nameof(ModifyEventScheduleCommand.EventId), opt => opt.MapFrom(src => src.EventId))
            .ForCtorParam(nameof(ModifyEventScheduleCommand.EnrollmentDeadline), opt => opt.MapFrom(src => src.Schedule.EnrollmentDeadline))
            .ForCtorParam(nameof(ModifyEventScheduleCommand.ScheduledStart), opt => opt.MapFrom(src => src.Schedule.ScheduledStart))
            .ForCtorParam(nameof(ModifyEventScheduleCommand.PublishDate), opt => opt.MapFrom(src => src.Schedule.PublishDate));
        
        this.CreateMap<CreateEventEnrollmentRequest, EnrollToEventCommand>()
            .ForCtorParam(nameof(EnrollToEventCommand.EventId), opt => opt.MapFrom(src => src.EventId))
            .ForCtorParam(nameof(EnrollToEventCommand.FirstName), opt => opt.MapFrom(src => src.Enrollment.FirstName))
            .ForCtorParam(nameof(EnrollToEventCommand.LastName), opt => opt.MapFrom(src => src.Enrollment.LastName))
            .ForCtorParam(nameof(EnrollToEventCommand.Email), opt => opt.MapFrom(src => src.Enrollment.Email))
            .ForCtorParam(nameof(EnrollToEventCommand.EnrollDate), opt => opt.MapFrom(src => src.Enrollment.EnrollDate))
            .ForCtorParam(nameof(EnrollToEventCommand.Participants), opt => opt.MapFrom(src => src.Enrollment.Participants));

        this.CreateMap<Participant, ONPA.Gatherings.Domain.Participant>()
            .ForCtorParam(nameof(Domain.Participant.FirstName), opt => opt.MapFrom(src => src.FirstName))
            .ForCtorParam(nameof(Domain.Participant.LastName), opt => opt.MapFrom(src => src.LastName))
            .ForCtorParam(nameof(Domain.Participant.Email), opt => opt.MapFrom(src => src.Email));
        
        this.CreateMap<RegisterEnrollmentPaymentRequest, RegisterEnrollmentPaymentCommand>()
            .ForCtorParam(nameof(RegisterEnrollmentPaymentCommand.EventId), opt => opt.MapFrom(src => src.EventId))
            .ForCtorParam(nameof(RegisterEnrollmentPaymentCommand.EnrollmentId), opt => opt.MapFrom(src => src.EnrollmentId))
            .ForCtorParam(nameof(RegisterEnrollmentPaymentCommand.PaidDate), opt => opt.MapFrom(src => src.Payment.PaidDate))
            .ForCtorParam(nameof(RegisterEnrollmentPaymentCommand.PaidAmount), opt => opt.MapFrom(src => new Money(src.Payment.PaidAmount, src.Payment.Currency)));
        
        this.CreateMap<CancelEnrollmentRequest, CancelEnrollmentCommand>()
            .ForCtorParam(nameof(CancelEnrollmentCommand.EventId), opt => opt.MapFrom(src => src.EventId))
            .ForCtorParam(nameof(CancelEnrollmentCommand.EnrollmentId), opt => opt.MapFrom(src => src.EnrollmentId))
            .ForCtorParam(nameof(CancelEnrollmentCommand.CancellationDate), opt => opt.MapFrom(src => src.Cancellation.CancellationDate))
            .ForCtorParam(nameof(CancelEnrollmentCommand.RefundableAmount), opt => opt.MapFrom(src =>
                new Money(src.Cancellation.RefundableAmount, src.Cancellation.Currency)));
        
        this.CreateMap<RefundEnrollmentPaymentRequest, RefundCancelledEnrollmentCommand>()
            .ForCtorParam(nameof(RefundCancelledEnrollmentCommand.EventId), opt => opt.MapFrom(src => src.EventId))
            .ForCtorParam(nameof(RefundCancelledEnrollmentCommand.EnrollmentId), opt => opt.MapFrom(src => src.EnrollmentId))
            .ForCtorParam(nameof(RefundCancelledEnrollmentCommand.RefundDate), opt => opt.MapFrom(src => src.Refund.RefundDate))
            .ForCtorParam(nameof(RefundCancelledEnrollmentCommand.RefundedAmount), opt => opt.MapFrom(src =>
                new Money(src.Refund.RefundAmount, src.Refund.Currency)));
        
        this.CreateMap<GetEventRequest, GetEventQuery>()
            .ForCtorParam(nameof(GetEventQuery.EventId), opt => opt.MapFrom(src => src.EventId));
        
        this.CreateMap<GetEventsByStatusRequest, GetEventsByStatusQuery>()
            .ForCtorParam(nameof(GetEventsByStatusQuery.Page), opt => opt.MapFrom(src => src.Page))
            .ForCtorParam(nameof(GetEventsByStatusQuery.Limit), opt => opt.MapFrom(src => src.Limit))
            .ForCtorParam(nameof(GetEventsByStatusQuery.Status), opt => opt.MapFrom(src => src.Status));

        this.CreateMap<GetEventEnrollmentsRequest, GetEventEnrollmentsQuery>()
            .ForCtorParam(nameof(GetEventEnrollmentsQuery.EventId), opt => opt.MapFrom(src => src.EventId))
            .ForCtorParam(nameof(GetEventEnrollmentsQuery.Page), opt => opt.MapFrom(src => src.Page))
            .ForCtorParam(nameof(GetEventEnrollmentsQuery.Limit), opt => opt.MapFrom(src => src.Limit));
        
        this.CreateMap<GetEventParticipantsRequest, GetEventParticipantsQuery>()
            .ForCtorParam(nameof(GetEventParticipantsQuery.EventId), opt => opt.MapFrom(src => src.EventId))
            .ForCtorParam(nameof(GetEventParticipantsQuery.Page), opt => opt.MapFrom(src => src.Page))
            .ForCtorParam(nameof(GetEventParticipantsQuery.Limit), opt => opt.MapFrom(src => src.Limit));
    }
} 