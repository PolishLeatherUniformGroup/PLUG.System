using PLUG.System.Common.Application;
using PLUG.System.Gatherings.Domain;

namespace PLUG.System.Gatherings.Api.Application.Commands;

public sealed record EnrollToPublicGatheringCommand : ApplicationCommandBase
{
    public Guid PublicGatheringId { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Email { get; init; }
    public List<Participant> Participants { get; init; }

    public DateTime EnrollDate { get; init; }

    public EnrollToPublicGatheringCommand(Guid publicGatheringId, string firstName, string lastName, string email,
        List<Participant> participants, DateTime enrollDate)
    {
        this.PublicGatheringId = publicGatheringId;
        this.FirstName = firstName;
        this.LastName = lastName;
        this.Email = email;
        this.Participants = participants;
        this.EnrollDate = enrollDate;
    }
}