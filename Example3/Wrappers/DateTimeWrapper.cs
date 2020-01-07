using System;

namespace Example3.Wrappers
{
    public class DateTimeWrapper : IDateTimeWrapper
    {
        public DateTime GetNow()
        {
            return DateTime.Now;
        }
    }
}