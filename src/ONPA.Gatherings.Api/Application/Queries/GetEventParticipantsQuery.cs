using System.Linq.Expressions;
using ONPA.Common.Application;
using ONPA.Gatherings.Contract.Responses;
using ONPA.Gatherings.Infrastructure.ReadModel;

namespace ONPA.Gatherings.Api.Application.Queries;

public record GetEventParticipantsQuery(Guid TenantId, Guid EventId, int Page, int Limit) : ApplicationCollectionQueryBase<ParticipantResponse>(TenantId,Page,Limit)
{
    public  string ToQueryString()
    {
        return $"{this.EventId}/participants?page={this.Page}&limit={this.Limit}";
    }
    
    public Expression<Func<EventEnrollment,bool>> AsFilter()
    {
        return x => x.EventId == this.EventId;
    }
}