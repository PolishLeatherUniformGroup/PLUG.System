using System.Runtime.CompilerServices;
using ONPA.Common.Domain;

namespace ONPA.Gatherings.Domain;

public class EventStatus : Enumeration
{
    public static EventStatus Draft = new(0);
    public static EventStatus ReadyToPublish = new(1);
    public static EventStatus Published = new(2);
    public static EventStatus Archived = new(3);
    public static EventStatus Cancelled = new(4);
    public EventStatus(int value, [CallerMemberName]string displayName="") : base(value, displayName)
    {
    }
}