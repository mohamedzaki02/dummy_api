using System;

namespace DatingApp.Helpers
{
    public static class DatetimeHelper
    {
        public static int TillNow(this DateTime date)
        {
            var age = DateTime.Today.Year - date.Year;
            if (date.AddYears(age) > DateTime.Today)
            {
                age--;
            }
            return age;
        }
    }
}