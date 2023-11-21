using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using PLUG.System.Common.Domain;

namespace PLUG.System.Apply.Domain;

public sealed class ApplicationStatus : Enumeration
{
    public static ApplicationStatus Received = new(0);
    public static ApplicationStatus Validated = new(1);
    public static ApplicationStatus Invalid = new(2);
    public static ApplicationStatus NotRecomended = new(3);
    public static ApplicationStatus AwaitDecision = new(4);
    public static ApplicationStatus Accepted = new(5);
    public static ApplicationStatus Rejected = new(6);
    public static ApplicationStatus RejectionAppealed = new(7);
    public static ApplicationStatus AppealSuccessful = new(8);
    public static ApplicationStatus AppealRejected = new(9);

    public ApplicationStatus()
    {
    }
    
    [JsonConstructor]
    public ApplicationStatus(int value, [CallerMemberName] string displayName = "") : base(value, displayName)
    {
    }
}