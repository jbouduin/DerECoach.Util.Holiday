using System;
using DerECoach.Util.Holiday.Configurations;

namespace DerECoach.Util.Holiday.Services
{
    interface ICalendarService
    {
        /// <summary>
        /// Checks the validity for the holiday, using the validfrom and validto years
        /// and the cycling property
        /// </summary>
        /// <param name="holiday"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        bool IsValid(Configurations.Holiday holiday, int year);

        /// <summary>
        /// get the date for the fixed holiday in the given year
        /// </summary>
        /// <param name="fixedHoliday"></param>
        /// <param name="year"></param>
        /// <returns>the Date (Local time) if the holiday occurs in the given year</returns>
        DateTime? GetFixedHolidyday(Fixed fixedHoliday, int year);

        /// <summary>
        /// Get a fixed weekday between to fixed days
        /// </summary>
        /// <param name="fixedWeekdayBetweenFixed"></param>
        /// <param name="year"></param>
        /// <returns>the Date (Local time) if the holiday occurs in the given year</returns>
        DateTime? GetFixedWeekdayBetweenFixedHoliday(FixedWeekdayBetweenFixed fixedWeekdayBetweenFixed, int year);

        /// <summary>
        /// Get a Holiday on a fixed weekday in a mont
        /// </summary>
        /// <param name="fixedWeekdayInMonth"></param>
        /// <param name="year"></param>
        /// <returns>the Date (Local time) if the holiday occurs in the given year</returns>
        DateTime? GetFixedWeekdayInMonthHoliday(FixedWeekdayInMonth fixedWeekdayInMonth, int year);

        /// <summary>
        /// Get a fixed Holiday relative to another fixed day
        /// </summary>
        /// <param name="fixedWeekdayRelativeToFixed"></param>
        /// <param name="year"></param>
        /// <returns>the Date (Local time) if the holiday occurs in the given year</returns>
        DateTime? GetFixedWeekdayRelativeToFixedHoliday(FixedWeekdayRelativeToFixed fixedWeekdayRelativeToFixed, int year);

        /// <summary>
        /// Get a (non Christian) holiday relative to easter Sunday
        /// </summary>
        /// <param name="relativeToEasterSunday"></param>
        /// <param name="year"></param>
        /// <returns>the Date (Local time) if the holiday occurs in the given year</returns>
        DateTime? GetRelativeToEasterSundayHoliday(RelativeToEasterSunday relativeToEasterSunday, int year);

        /// <summary>
        /// Get a holiday relative to a fixed day
        /// </summary>
        /// <param name="relativeToFixed"></param>
        /// <param name="year"></param>
        /// <returns>the Date (Local time) if the holiday occurs in the given year</returns>
        DateTime? GetRelativeToFixedHoliday(RelativeToFixed relativeToFixed, int year);

        /// <summary>
        /// Get a holiday relative to a fixed weekday in a month
        /// </summary>
        /// <param name="relativeToWeekdayInMonth"></param>
        /// <param name="year">the Date (Local time) if the holiday occurs in the given year</param>
        /// <returns></returns>
        DateTime? GetRelativeToWeekdayInMonthHoliday(RelativeToWeekdayInMonth relativeToWeekdayInMonth, int year);

        /// <summary>
        /// Get eastern sunday for the given year
        /// </summary>
        /// <param name="chronology"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        DateTime GetEasternSunday(ChronologyType chronology, int year);

        /// <summary>
        /// Move a date according to its moving definitions
        /// </summary>
        /// <param name="moveableHoliday"></param>
        /// <param name="calculatedDateTime"></param>
        /// <returns></returns>
        DateTime MoveDate(MoveableHoliday moveableHoliday, DateTime calculatedDateTime);
    }
}
