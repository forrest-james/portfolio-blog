using System;
using System.Text;

namespace Application.Common.Services
{
    public static class DateTimeEncoderService
    {
        public static string Encode(this DateTime dateTime)
        {
            var tempString = new StringBuilder();

            // Convert Year to "Terran Year" (i.e. years since 3000 BCE, recorded history)
            tempString.Append((dateTime.Year + 3000).ToString());

            // Add Day separator
            tempString.Append(".");

            // Add three-digit Day of Year
            tempString.Append(dateTime.DayOfYear.ToString("000"));

            // Add Time separator
            tempString.Append(".");

            // Add six-digit Time of Day
            tempString.Append(dateTime.ToString("HHmmss"));

            return tempString.ToString();
        }
    }
}