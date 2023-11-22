using PLUG.System.Common.Domain;
using PLUG.System.Common.Exceptions;

namespace PLUG.System.Apply.Domain;

public sealed class Recommendation : Entity
{
    internal Recommendation(Guid memberId, string memberNumber, DateTime requestedDate):base(Guid.NewGuid())
    {
        this.MemberId = memberId;
        this.MemberNumber = memberNumber ?? throw new ArgumentNullException(nameof(memberNumber));
        this.RequestedDate = requestedDate;
    }

    internal Recommendation(Guid id, Guid memberId, string memberNumber, DateTime requestedDate) : base(id)
    {
        this.MemberId = memberId;
        this.MemberNumber = memberNumber;
        this.RequestedDate = requestedDate;
    }

    public Guid MemberId { get; private set; }
    public string MemberNumber { get; private set; }
    
    public DateTime RequestedDate { get; private set; }

    private bool? _isEndorsed;

    public bool IsEndorsed => this._isEndorsed.GetValueOrDefault(false);
    public bool IsRefused => this._isEndorsed.HasValue && !this.IsEndorsed;

    public void EndorseRecommendation()
    {
        if (this.IsEndorsed || this.IsRefused)
        {
            throw new InvalidDomainOperationException();
        }
        this._isEndorsed = true;
    }
    public void RefuseRecommendation()
    {
        if (this.IsEndorsed || this.IsRefused)
        {
            throw new InvalidDomainOperationException();
        }

        this._isEndorsed = false;
    }

}