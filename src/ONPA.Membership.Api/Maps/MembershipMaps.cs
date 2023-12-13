using AutoMapper;
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
    }
}