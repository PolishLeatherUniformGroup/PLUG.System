using AutoMapper;
using ONPA.Membership.Api.Application.Commands;
using ONPA.Membership.Api.Application.Queries;
using ONPA.Membership.Contract.Requests;
using ONPA.Membership.Contract.Responses;
using ONPA.Membership.Infrastructure.ReadModel;
using PLUG.System.SharedDomain;

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
            .ForCtorParam(nameof(RegisterMemberFeePaymentCommand.TenantId), o =>
                o.MapFrom(s => s.TenantId))
            .ForCtorParam(nameof(RegisterMemberFeePaymentCommand.MemberId), o =>
                o.MapFrom(s => s.MemberId))
            .ForCtorParam(nameof(RegisterMemberFeePaymentCommand.FeeId), o =>
                o.MapFrom(s => s.FeeId))
            .ForCtorParam(nameof(RegisterMemberFeePaymentCommand.FeeAmount), o =>
                o.MapFrom(s => new Money(s.Payment.Amount, s.Payment.Currency)))
            .ForCtorParam(nameof(RegisterMemberFeePaymentCommand.PaidDate), o =>
                o.MapFrom(s => s.Payment.PaymentDate));

        this.CreateMap<MemberSuspensionRequest, SuspendMemberCommand>()
            .ForCtorParam(nameof(SuspendMemberCommand.TenantId), o =>
                o.MapFrom(s => s.TenantId))
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
            .ForCtorParam(nameof(AppealMemberSuspensionCommand.TenantId), o =>
                o.MapFrom(s => s.TenantId))
            .ForCtorParam(nameof(AppealMemberSuspensionCommand.MemberId), o =>
                o.MapFrom(s => s.MemberId))
            .ForCtorParam(nameof(AppealMemberSuspensionCommand.AppealDate), o =>
                o.MapFrom(s => s.Appeal.AppealDate))
            .ForCtorParam(nameof(AppealMemberSuspensionCommand.Justification), o =>
                o.MapFrom(s => s.Appeal.Justification));

        this.CreateMap<AcceptMemberSuspensionAppealRequest, AcceptSuspensionAppealCommand>()
            .ForCtorParam(nameof(AcceptSuspensionAppealCommand.TenantId), o =>
                o.MapFrom(s => s.TenantId))
            .ForCtorParam(nameof(AcceptSuspensionAppealCommand.MemberId), o =>
                o.MapFrom(s => s.MemberId))
            .ForCtorParam(nameof(AcceptSuspensionAppealCommand.DecisionDate), o =>
                o.MapFrom(s => s.Decision.DecisionDate))
            .ForCtorParam(nameof(AcceptSuspensionAppealCommand.Justification), o =>
                o.MapFrom(s => s.Decision.Justification));

        this.CreateMap<RejectMemberSuspensionAppealRequest, DismissSuspensionAppealCommand>()
            .ForCtorParam(nameof(DismissSuspensionAppealCommand.TenantId), o =>
                o.MapFrom(s => s.TenantId))
            .ForCtorParam(nameof(DismissSuspensionAppealCommand.MemberId), o =>
                o.MapFrom(s => s.MemberId))
            .ForCtorParam(nameof(DismissSuspensionAppealCommand.DecisionDate), o =>
                o.MapFrom(s => s.Decision.DecisionDate))
            .ForCtorParam(nameof(DismissSuspensionAppealCommand.Justification), o =>
                o.MapFrom(s => s.Decision.Justification));

        this.CreateMap<GetMemberSuspensionsRequest, GetMemberSuspensionsQuery>()
            .ForCtorParam(nameof(GetMemberSuspensionsQuery.MemberId), o =>
                o.MapFrom(s => s.MemberId))
            .ForCtorParam(nameof(GetMemberSuspensionsQuery.Limit), o =>
                o.MapFrom(s => s.Limit))
            .ForCtorParam(nameof(GetMemberSuspensionsQuery.Page), o =>
                o.MapFrom(s => s.Page));

        this.CreateMap<MemberExpelRequest, ExpelMemberCommand>()
            .ForCtorParam(nameof(ExpelMemberCommand.TenantId), o =>
                o.MapFrom(s => s.TenantId))
            .ForCtorParam(nameof(ExpelMemberCommand.MemberId), o =>
                o.MapFrom(s => s.MemberId))
            .ForCtorParam(nameof(ExpelMemberCommand.ExpelDate), o =>
                o.MapFrom(s => s.Expel.ExpelDate))
            .ForCtorParam(nameof(ExpelMemberCommand.Justification), o =>
                o.MapFrom(s => s.Expel.Justification))
            .ForCtorParam(nameof(ExpelMemberCommand.DaysToAppeal), o =>
                o.MapFrom(s => s.Expel.DaysToAppeal));

        this.CreateMap<MemberExpelAppealRequest, AppealMemberExpelCommand>()
            .ForCtorParam(nameof(AppealMemberExpelCommand.TenantId), o =>
                o.MapFrom(s => s.TenantId))
            .ForCtorParam(nameof(AppealMemberExpelCommand.MemberId), o =>
                o.MapFrom(s => s.MemberId))
            .ForCtorParam(nameof(AppealMemberExpelCommand.AppealDate), o =>
                o.MapFrom(s => s.Appeal.AppealDate))
            .ForCtorParam(nameof(AppealMemberExpelCommand.Justification), o =>
                o.MapFrom(s => s.Appeal.Justification));

        this.CreateMap<AcceptMemberExpelAppealRequest, AcceptExpelAppealCommand>()
            .ForCtorParam(nameof(AcceptExpelAppealCommand.TenantId), o =>
                o.MapFrom(s => s.TenantId))
            .ForCtorParam(nameof(AcceptExpelAppealCommand.MemberId), o =>
                o.MapFrom(s => s.MemberId))
            .ForCtorParam(nameof(AcceptExpelAppealCommand.DecisionDate), o =>
                o.MapFrom(s => s.Decision.DecisionDate))
            .ForCtorParam(nameof(AcceptExpelAppealCommand.Justification), o =>
                o.MapFrom(s => s.Decision.Justification));

        this.CreateMap<RejectMemberExpelAppealRequest, DismissExpelAppealCommand>()
            .ForCtorParam(nameof(DismissExpelAppealCommand.TenantId), o =>
                o.MapFrom(s => s.TenantId))
            .ForCtorParam(nameof(DismissExpelAppealCommand.MemberId), o =>
                o.MapFrom(s => s.MemberId))
            .ForCtorParam(nameof(DismissExpelAppealCommand.DecisionDate), o =>
                o.MapFrom(s => s.Decision.DecisionDate))
            .ForCtorParam(nameof(DismissExpelAppealCommand.Justification), o =>
                o.MapFrom(s => s.Decision.Justification));

        this.CreateMap<GetMemberExpelsRequest, GetMemberExpelsQuery>()
            .ForCtorParam(nameof(GetMemberExpelsQuery.MemberId), o =>
                o.MapFrom(s => s.MemberId))
            .ForCtorParam(nameof(GetMemberExpelsQuery.Limit), o =>
                o.MapFrom(s => s.Limit))
            .ForCtorParam(nameof(GetMemberExpelsQuery.Page), o =>
                o.MapFrom(s => s.Page));

        this.CreateMap<CreateMemberRequest, CreateMemberCommand>()
            .ForCtorParam(nameof(CreateMemberCommand.TenantId), o =>
                o.MapFrom(s => s.TenantId))
            .ForCtorParam(nameof(CreateMemberCommand.FirstName), o =>
                o.MapFrom(s => s.FirstName))
            .ForCtorParam(nameof(CreateMemberCommand.LastName), o =>
                o.MapFrom(s => s.LastName))
            .ForCtorParam(nameof(CreateMemberCommand.Email), o =>
                o.MapFrom(s => s.Email))
            .ForCtorParam(nameof(CreateMemberCommand.Phone), o =>
                o.MapFrom(s => s.Phone))
            .ForCtorParam(nameof(CreateMemberCommand.Address), o =>
                o.MapFrom(s => s.Address))
            .ForCtorParam(nameof(CreateMemberCommand.JoinDate), o =>
                o.MapFrom(s => s.JoinDate))
            .ForCtorParam(nameof(CreateMemberCommand.PaidFee), o =>
                o.MapFrom(s => new Money(s.PaidFeeAmount, s.PaidFeeCurrency)));

        this.CreateMap<UpdateMemberDataRequest, ModifyMemberContactDataCommand>()
            .ForCtorParam(nameof(ModifyMemberContactDataCommand.TenantId), o =>
                o.MapFrom(s => s.TenantId))
            .ForCtorParam(nameof(ModifyMemberContactDataCommand.MemberId), o =>
                o.MapFrom(s => s.MemberId))
            .ForCtorParam(nameof(ModifyMemberContactDataCommand.Email), o =>
                o.MapFrom(s => s.ContactData.Email))
            .ForCtorParam(nameof(ModifyMemberContactDataCommand.Phone), o =>
                o.MapFrom(s => s.ContactData.Phone))
            .ForCtorParam(nameof(ModifyMemberContactDataCommand.Address), o =>
                o.MapFrom(s => s.ContactData.Address));

        this.CreateMap<ChangeMemberTypeRequest, MakeMemberRegularCommand>()
            .ForCtorParam(nameof(MakeMemberRegularCommand.TenantId), o =>
                o.MapFrom(s => s.TenantId))
            .ForCtorParam(nameof(MakeMemberRegularCommand.MemberId), o =>
                o.MapFrom(s => s.MemberId));

        this.CreateMap<ChangeMemberTypeRequest, MakeMemberHonoraryCommand>()
            .ForCtorParam(nameof(MakeMemberHonoraryCommand.TenantId), o =>
                o.MapFrom(s => s.TenantId))
            .ForCtorParam(nameof(MakeMemberHonoraryCommand.MemberId), o =>
                o.MapFrom(s => s.MemberId));

        this.CreateMap<MemberExpirationRequest, ExpireMembershipCommand>()
            .ForCtorParam(nameof(ExpireMembershipCommand.TenantId), o =>
                o.MapFrom(s => s.TenantId))
            .ForCtorParam(nameof(ExpireMembershipCommand.MemberId), o =>
                o.MapFrom(s => s.MemberId))
            .ForCtorParam(nameof(ExpireMembershipCommand.ExpirationDate), o =>
                o.MapFrom(s => s.Expiration.ExpirationDate))
            .ForCtorParam(nameof(ExpireMembershipCommand.Reason), o =>
                o.MapFrom(s => s.Expiration.Reason));
        
        this.CreateMap<GetMemberFeesRequest, GetMemberFeesQuery>()
            .ForCtorParam(nameof(GetMemberFeesQuery.MemberId), o =>
                o.MapFrom(s => s.MemberId))
            .ForCtorParam(nameof(GetMemberFeesQuery.Limit), o =>
                o.MapFrom(s => s.Limit))
            .ForCtorParam(nameof(GetMemberFeesQuery.Page), o =>
                o.MapFrom(s => s.Page));
        
        this.CreateMap<Member, MemberResult>()
            .ForCtorParam(nameof(MemberResult.MemberId), o =>
                o.MapFrom(s => s.Id))
            .ForCtorParam(nameof(MemberResult.CardNumber), o =>
                o.MapFrom(s => s.MemberNumber))
            .ForCtorParam(nameof(MemberResult.FirstName), o =>
                o.MapFrom(s => s.FirstName))
            .ForCtorParam(nameof(MemberResult.LastName), o =>
                o.MapFrom(s => s.LastName))
            .ForCtorParam(nameof(MemberResult.Email), o =>
                o.MapFrom(s => s.Email))
            .ForCtorParam(nameof(MemberResult.Status), o =>
                o.MapFrom(s => (int)s.Status))
            .ForCtorParam(nameof(MemberResult.JoinDate), o =>
                o.MapFrom(s => s.JoinDate));

        this.CreateMap<MemberFee, MemberFeeResult>()
            .ForCtorParam(nameof(MemberFeeResult.MemberFeeId), o =>
                o.MapFrom(s => s.Id))
            .ForCtorParam(nameof(MemberFeeResult.MemberId), o =>
                o.MapFrom(s => s.MemberId))
            .ForCtorParam(nameof(MemberFeeResult.FeeDue), o =>
                o.MapFrom(s => s.DueAmount))
            .ForCtorParam(nameof(MemberFeeResult.Currency), o =>
                o.MapFrom(s => s.Currency))
            .ForCtorParam(nameof(MemberFeeResult.DueDate), o =>
                o.MapFrom(s => s.DueDate))
            .ForCtorParam(nameof(MemberFeeResult.PaidDate), o =>
                o.MapFrom(s => s.PaidDate))
            .ForCtorParam(nameof(MemberFeeResult.AmountPaid), o =>
                o.MapFrom(s => s.PaidAmount))
            .ForCtorParam(nameof(MemberFeeResult.ValidToDate), o =>
                o.MapFrom(s => s.FeeEndDate));

        this.CreateMap<MemberExpel, MemberExpelResult>()
            .ForCtorParam(nameof(MemberExpelResult.MemberId), o =>
                o.MapFrom(s => s.MemberId))
            .ForCtorParam(nameof(MemberExpelResult.ExpelDate), o =>
                o.MapFrom(s => s.ExpelDate))
            .ForCtorParam(nameof(MemberExpelResult.Justification), o =>
                o.MapFrom(s => s.Justification))
            .ForCtorParam(nameof(MemberExpelResult.AppealDate), o =>
                o.MapFrom(s => s.AppealDate))
            .ForCtorParam(nameof(MemberExpelResult.AppealJustification), o =>
                o.MapFrom(s => s.AppealJustification))
            .ForCtorParam(nameof(MemberExpelResult.AppealDecisionDate), o =>
                o.MapFrom(s => s.AppealDecisionDate));
        
        this.CreateMap<MemberSuspension, MemberSuspensionResult>()
            .ForCtorParam(nameof(MemberSuspensionResult.MemberId), o =>
                o.MapFrom(s => s.MemberId))
            .ForCtorParam(nameof(MemberSuspensionResult.SuspensionDate), o =>
                o.MapFrom(s => s.SuspensionDate))
            .ForCtorParam(nameof(MemberSuspensionResult.Justification), o =>
                o.MapFrom(s => s.Justification))
            .ForCtorParam(nameof(MemberSuspensionResult.AppealDate), o =>
                o.MapFrom(s => s.AppealDate))
            .ForCtorParam(nameof(MemberSuspensionResult.AppealJustification), o =>
                o.MapFrom(s => s.AppealJustification))
            .ForCtorParam(nameof(MemberSuspensionResult.AppealDecisionDate), o =>
                o.MapFrom(s => s.AppealDecisionDate));
    }
}