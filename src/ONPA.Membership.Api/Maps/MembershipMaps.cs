using AutoMapper;
using ONPA.Membership.Api.Application.Commands;
using ONPA.Membership.Api.Application.Queries;
using ONPA.Membership.Contract.Requests;

namespace ONPA.Membership.Api.Maps;

public class MembershipMaps : Profile
{
    public MembershipMaps()
    {
        this.CreateMap<GetMembersRequest, GetMembersByStatusQuery>()
            .ForCtorParam(nameof(GetMembersByStatusQuery.Status), o =>
                o.MapFrom(s => s.Status))
            .ForCtorParam(nameof(GetMembersByStatusQuery.Limit), o =>
                o.MapFrom(s => s.Limit))
            .ForCtorParam(nameof(GetMembersByStatusQuery.Page), o =>
                o.MapFrom(s => s.Page));

        this.CreateMap<GetMemberRequest, GetMemberByIdQuery>()
            .ForCtorParam(nameof(GetMemberByIdQuery.MemberId), o =>
                o.MapFrom(s => s.MemberId));

        this.CreateMap<GetMemberFeesRequest, GetMemberFeesQuery>()
            .ForCtorParam(nameof(GetMemberFeesQuery.MemberId), o =>
                o.MapFrom(s => s.MemberId))
            .ForCtorParam(nameof(GetMemberFeesQuery.Limit), o =>
                o.MapFrom(s => s.Limit))
            .ForCtorParam(nameof(GetMemberFeesQuery.Page), o =>
                o.MapFrom(s => s.Page));
        
        this.CreateMap<RegisterMembershipFeePaymentRequest, RegisterMemberFeePaymentCommand>()
            .ForCtorParam(nameof(RegisterMemberFeePaymentCommand.MemberId), o =>
                o.MapFrom(s => s.MemberId))
            .ForCtorParam(nameof(RegisterMemberFeePaymentCommand.FeeId), o =>
                o.MapFrom(s => s.FeeId))
            .ForCtorParam(nameof(RegisterMemberFeePaymentCommand.FeeAmount), o =>
                o.MapFrom(s => s.Payment.Amount))
            .ForCtorParam(nameof(RegisterMemberFeePaymentCommand.PaidDate), o =>
                o.MapFrom(s => s.Payment.PaymentDate));
        
        this.CreateMap<MemberSuspensionRequest, SuspendMemberCommand>()
            .ForCtorParam(nameof(SuspendMemberCommand.MemberId), o =>
                o.MapFrom(s => s.MemberId))
            .ForCtorParam(nameof(SuspendMemberCommand.SuspendDate), o =>
                o.MapFrom(s => s.Suspension.SuspensionDate))
            .ForCtorParam(nameof(SuspendMemberCommand.SuspendUntil), o =>
                o.MapFrom(s => s.Suspension.ReinstatementDate))
            .ForCtorParam(nameof(SuspendMemberCommand.Justification), o =>
                o.MapFrom(s => s.Suspension.Justification))
            .ForCtorParam(nameof(SuspendMemberCommand.DaysToAppeal), o =>
                o.MapFrom(s => s.Suspension.DaysToAppeal));
        
        this.CreateMap<MemberSuspensionAppealRequest, AppealMemberSuspensionCommand>()
            .ForCtorParam(nameof(AppealMemberSuspensionCommand.MemberId), o =>
                o.MapFrom(s => s.MemberId))
            .ForCtorParam(nameof(AppealMemberSuspensionCommand.AppealDate), o =>
                o.MapFrom(s => s.Appeal.AppealDate))
            .ForCtorParam(nameof(AppealMemberSuspensionCommand.Justification), o =>
                o.MapFrom(s => s.Appeal.Justification));
        
        this.CreateMap<AcceptMemberSuspensionAppealRequest,AcceptSuspensionAppealCommand>()
            .ForCtorParam(nameof(AcceptSuspensionAppealCommand.MemberId), o =>
                o.MapFrom(s => s.MemberId))
            .ForCtorParam(nameof(AcceptSuspensionAppealCommand.DecisionDate), o =>
                o.MapFrom(s => s.Decision.DecisionDate))
            .ForCtorParam(nameof(AcceptSuspensionAppealCommand.Justification), o =>
                o.MapFrom(s => s.Decision.Justification));
        
        this.CreateMap<RejectMemberSuspensionAppealRequest,DismissSuspensionAppealCommand>()
            .ForCtorParam(nameof(DismissSuspensionAppealCommand.MemberId), o =>
                o.MapFrom(s => s.MemberId))
            .ForCtorParam(nameof(DismissSuspensionAppealCommand.DecisionDate), o =>
                o.MapFrom(s => s.Decision.DecisionDate))
            .ForCtorParam(nameof(DismissSuspensionAppealCommand.Justification), o =>
                o.MapFrom(s => s.Decision.Justification));
        
        this.CreateMap<GetMemberSuspensionsRequest,GetMemberSuspensionsQuery>()
            .ForCtorParam(nameof(GetMemberSuspensionsQuery.MemberId), o =>
                o.MapFrom(s => s.MemberId))
            .ForCtorParam(nameof(GetMemberSuspensionsQuery.Limit), o =>
                o.MapFrom(s => s.Limit))
            .ForCtorParam(nameof(GetMemberSuspensionsQuery.Page), o =>
                o.MapFrom(s => s.Page));
        
this.CreateMap<MemberExpelRequest, ExpelMemberCommand>()
            .ForCtorParam(nameof(ExpelMemberCommand.MemberId), o =>
                o.MapFrom(s => s.MemberId))
   .ForCtorParam(nameof(ExpelMemberCommand.ExpelDate), o =>
                o.MapFrom(s => s.Expel.ExpelDate))
            .ForCtorParam(nameof(ExpelMemberCommand.Justification), o =>
                o.MapFrom(s => s.Expel.Justification))
            .ForCtorParam(nameof(ExpelMemberCommand.DaysToAppeal), o =>
                o.MapFrom(s => s.Expel.DaysToAppeal));
        
        this.CreateMap<MemberExpelAppealRequest, AppealMemberExpelCommand>()
            .ForCtorParam(nameof(AppealMemberExpelCommand.MemberId), o =>
                o.MapFrom(s => s.MemberId))
            .ForCtorParam(nameof(AppealMemberExpelCommand.AppealDate), o =>
                o.MapFrom(s => s.Appeal.AppealDate))
            .ForCtorParam(nameof(AppealMemberExpelCommand.Justification), o =>
                o.MapFrom(s => s.Appeal.Justification));
        
        this.CreateMap<AcceptMemberExpelAppealRequest,AcceptExpelAppealCommand>()
            .ForCtorParam(nameof(AcceptExpelAppealCommand.MemberId), o =>
                o.MapFrom(s => s.MemberId))
            .ForCtorParam(nameof(AcceptExpelAppealCommand.DecisionDate), o =>
                o.MapFrom(s => s.Decision.DecisionDate))
            .ForCtorParam(nameof(AcceptExpelAppealCommand.Justification), o =>
                o.MapFrom(s => s.Decision.Justification));
        
        this.CreateMap<RejectMemberExpelAppealRequest,DismissExpelAppealCommand>()
            .ForCtorParam(nameof(DismissExpelAppealCommand.MemberId), o =>
                o.MapFrom(s => s.MemberId))
            .ForCtorParam(nameof(DismissExpelAppealCommand.DecisionDate), o =>
                o.MapFrom(s => s.Decision.DecisionDate))
            .ForCtorParam(nameof(DismissExpelAppealCommand.Justification), o =>
                o.MapFrom(s => s.Decision.Justification));
        
        this.CreateMap<GetMemberExpelsRequest,GetMemberExpelsQuery>()
            .ForCtorParam(nameof(GetMemberExpelsQuery.MemberId), o =>
                o.MapFrom(s => s.MemberId))
            .ForCtorParam(nameof(GetMemberExpelsQuery.Limit), o =>
                o.MapFrom(s => s.Limit))
            .ForCtorParam(nameof(GetMemberExpelsQuery.Page), o =>
                o.MapFrom(s => s.Page));
      
    }
}