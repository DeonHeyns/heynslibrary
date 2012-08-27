using System;


namespace HeynsLibrary
{
    public static class DateTimeExtensions
    {
        public static bool IsBetweenInclusive(this DateTime date, DateTime dateFrom, DateTime dateTo)
        {
            return date >= dateFrom && date <= dateTo;
        }

        public static bool IsBetweenInclusive(this DateTime? date, DateTime? dateFrom, DateTime? dateTo)
        {
            if (!date.HasValue)
                throw new ArgumentNullException("date");
            if (!dateFrom.HasValue)
                throw new ArgumentNullException("dateFrom");
            if (!dateTo.HasValue)
                throw new ArgumentNullException("dateTo");
            return date.Value >= dateFrom.Value && date <= dateTo.Value;
        }
    }
}
