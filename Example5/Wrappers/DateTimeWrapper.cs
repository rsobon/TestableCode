using System;

namespace Example5.Wrappers
{
    public class DateTimeWrapper : IDateTimeWrapper
    {
        public DateTime GetNow()
        {
            return DateTime.Now;
        }
    }
}