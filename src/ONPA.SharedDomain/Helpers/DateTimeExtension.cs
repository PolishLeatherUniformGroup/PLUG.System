namespace PLUG.System.SharedDomain.Helpers;

public static class DateTimeExtension
{
    public static DateTime ToYearEnd(this DateTime dateTime)
    {
        return new DateTime(dateTime.Year, 12, 31);
    }
    
    public static DateTime MonthEnd(this DateTime dateTime, int month, int year)
    {
        return new DateTime(year, month, DateTime.DaysInMonth(dateTime.Year, dateTime.Month));
    }
}