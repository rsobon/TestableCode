﻿namespace Example6.Wrappers;

public class DateTimeWrapper : IDateTimeWrapper
{
    public DateTime GetNow()
    {
        return DateTime.Now;
    }
}