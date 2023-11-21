namespace PLUG.System.SharedDomain.Helpers;

public static class DateTimeExtension
{
    public static DateTime ToYearEnd(this DateTime dateTime)
    {
        return new DateTime(dateTime.Year, 12, 31);
    }
}