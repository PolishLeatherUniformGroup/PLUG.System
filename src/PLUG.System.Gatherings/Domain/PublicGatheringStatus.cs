using System.Runtime.CompilerServices;
using PLUG.System.Common.Domain;

namespace PLUG.System.Gatherings.Domain;

public class PublicGatheringStatus : Enumeration
{
    public static PublicGatheringStatus Draft = new(0);
    public static PublicGatheringStatus ReadyToPublish = new(1);
    public static PublicGatheringStatus Published = new(2);
    public static PublicGatheringStatus Archived = new(3);
    public PublicGatheringStatus(int value, [CallerMemberName]string displayName="") : base(value, displayName)
    {
    }
}