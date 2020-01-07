using System;

namespace Example4.Wrappers
{
    public class DateTimeWrapper : IDateTimeWrapper
    {
        public DateTime GetNow()
        {
            return DateTime.Now;
        }
    }
}