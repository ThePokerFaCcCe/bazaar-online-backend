namespace BazaarOnline.Application.Utils;

public static class TimeUtils
{
    public static string PassedFromNowString(this DateTime pastTime)
    {
        var now = DateTime.Now;
        var result = now - pastTime;

        if (result.TotalDays > 29)
            return $"{(int)result.TotalDays / 30} ماه پیش";
        else if (result.TotalDays > 1)
            return $"{(int)result.TotalDays} روز پیش";
        else if ((int)result.TotalDays == 1)
            return "یک روز پیش";
        else if (result.TotalHours > 1)
            return $"{(int)result.TotalHours} ساعت پیش";
        else if (result.TotalMinutes > 40)
            return "یک ساعت پیش";
        else if (result.TotalMinutes > 25)
            return "نیم ساعت پیش";
        else if (result.TotalMinutes > 10)
            return "یک ربع پیش";
        else if (result.TotalMinutes > 3)
            return "دقایقی پیش";
        else
            return "لحظاتی پیش";
    }
}