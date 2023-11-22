using AutoMapper;
using PLUG.System.Apply.Api__OLD.Requests.Apply;
using PLUG.System.Apply.Api.Application.Commands;
using PLUG.System.SharedDomain;

namespace PLUG.System.Apply.Api.Maps;

public class ApplyMaps : Profile
{
    public ApplyMaps()
    {
        this.CreateMap<CreateApplicationRequest, CreateApplicationFormCommand>()
            .ForCtorParam(nameof(CreateApplicationFormCommand.FirstName), o=>
                    o.MapFrom(s=>s.FirstName))
            .ForCtorParam(nameof(CreateApplicationFormCommand.LastName),
                o=>
                    o.MapFrom(s=>s.LastName))
            .ForCtorParam(nameof(CreateApplicationFormCommand.Email), o=>
                    o.MapFrom(s=>s.Email))
            .ForCtorParam(nameof(CreateApplicationFormCommand.Address), o=>
                    o.MapFrom(s=>s.Address))
            .ForCtorParam(nameof(CreateApplicationFormCommand.Recommendations), o=>
                    o.MapFrom(s=>s.Recommenders));

        this.CreateMap<ApproveApplicationRequest, ApproveApplicationCommand>()
            .ForCtorParam(nameof(ApproveApplicationCommand.ApplicationId), o => 
                    o.MapFrom(s => s.ApplicationId))
            .ForCtorParam(nameof(ApproveApplicationCommand.DecisionDate), o => 
                    o.MapFrom(s => s.Decision.DecisionDate));

        this.CreateMap<RejectApplicationRequest, RejectApplicationCommand>()
            .ForCtorParam(nameof(RejectApplicationCommand.ApplicationId), o => 
                    o.MapFrom(s => s.ApplicationId))
            .ForCtorParam(nameof(RejectApplicationCommand.DaysToAppeal), o => 
                    o.MapFrom(s => s.Decision.DaysToAppeal))
            .ForCtorParam(nameof(RejectApplicationCommand.DecisionDetail), o => 
                    o.MapFrom(s => s.Decision.DecisionDetail));

        this.CreateMap<EndorseRecommendationRequest, EndorseApplicationRecommendationCommand>()
            .ForCtorParam(nameof(EndorseApplicationRecommendationCommand.ApplicationFormId), o => 
                    o.MapFrom(s => s.ApplicationId))
            .ForCtorParam(nameof(EndorseApplicationRecommendationCommand.RecommendationId), o => 
                    o.MapFrom(s => s.RecommendationId))
            .ForCtorParam(nameof(EndorseApplicationRecommendationCommand.RecommendingMemberId), o => 
                    o.MapFrom(s => s.Endorsement.RecommendingMemberId));

        this.CreateMap<RefuseRecommendationRequest, RefuseApplicationRecommendationCommand>()
            .ForCtorParam(nameof(RefuseApplicationRecommendationCommand.ApplicationFormId), o => 
                o.MapFrom(s => s.ApplicationId))
            .ForCtorParam(nameof(RefuseApplicationRecommendationCommand.RecommendationId), o => 
                o.MapFrom(s => s.RecommendationId))
            .ForCtorParam(nameof(RefuseApplicationRecommendationCommand.RecommendingMemberId), o => 
                o.MapFrom(s => s.Refusal.RecommendingMemberId));

        this.CreateMap<RegisterApplicationPaymentRequest, RegisterApplicationFeePaymentCommand>()
            .ForCtorParam(nameof(RegisterApplicationFeePaymentCommand.ApplicationId), 
                o => 
                    o.MapFrom(s => s.ApplicationId))
            .ForCtorParam(nameof(RegisterApplicationFeePaymentCommand.Paid), o => 
                    o.MapFrom(s =>new Money(s.Payment.Amount,s.Payment.Currency)));

        this.CreateMap<AppealApplicationRejectionRequest, AppealApplicationRejectionCommand>()
            .ForCtorParam(nameof(AppealApplicationRejectionCommand.ApplicationId), o => 
                o.MapFrom(s => s.ApplicationId))
            .ForCtorParam(nameof(AppealApplicationRejectionCommand.AppealReceived), o => 
                o.MapFrom(s => s.Appeal.ReceivedDate))
            .ForCtorParam(nameof(AppealApplicationRejectionCommand.Justification), o => 
                o.MapFrom(s => s.Appeal.Justification));
    }
}