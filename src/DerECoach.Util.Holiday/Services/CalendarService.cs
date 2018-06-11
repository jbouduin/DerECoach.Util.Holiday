using System;
using System.Linq;
using DerECoach.Util.Holiday.Configurations;

namespace DerECoach.Util.Holiday.Services
{
    internal class CalendarService: ICalendarService
    {
        #region ICalendarService methods --------------------------------------
        public bool IsValid(Configurations.Holiday holiday,int year)
        {
            var result = (holiday.validFrom < year || holiday.validFrom <= 0) &&
                   (holiday.validTo >= year || holiday.validTo == 0) && IsValidForCyle(holiday, year);
            return result;
        }
        
        public DateTime? GetFixedHolidyday(Fixed fixedHoliday, int year)
        {
            if (!IsValid(fixedHoliday, year))
                return null;

            return MoveDate(fixedHoliday, new DateTime(year, ConvertMonth(fixedHoliday.month), fixedHoliday.day));
        }

        public DateTime GetEasternSunday(ChronologyType chronology, int year)
        {
            switch (chronology)
            {
                case ChronologyType.JULIAN:
                    return year <= 1583 ? GetJulianEasternSunday(year) : GetGregorianEasterSunday(year);
                case ChronologyType.GREGORIAN:
                    return GetGregorianEasterSunday(year);
                default:
                    throw new ArgumentOutOfRangeException("chronology");
            }
        }

        public DateTime MoveDate(MoveableHoliday moveableHoliday, DateTime calculatedDateTime)
        {
            var currentCondition = moveableHoliday.MovingCondition.FirstOrDefault(a => MustMove(a, calculatedDateTime));
            while (currentCondition != null)
            {
                calculatedDateTime = MoveHoliday(currentCondition, calculatedDateTime);
                currentCondition = moveableHoliday.MovingCondition.FirstOrDefault(a => MustMove(a, calculatedDateTime));
            }
            return calculatedDateTime;
        }

        public DateTime? GetFixedWeekdayBetweenFixedHoliday(FixedWeekdayBetweenFixed fixedWeekdayBetweenFixed, int year)
        {
            var fromFixed = GetFixedHolidyday(fixedWeekdayBetweenFixed.from, year);
            if (!fromFixed.HasValue)
                return null;
            var toFixed = GetFixedHolidyday(fixedWeekdayBetweenFixed.to, year);

            if (!toFixed.HasValue)
                return null;
            var daytoGet = ConvertWeekday(fixedWeekdayBetweenFixed.weekday);
            var calculatedDate = fromFixed.Value;
            while (calculatedDate.DayOfWeek != daytoGet && calculatedDate <= toFixed.Value)
            {
                calculatedDate = calculatedDate.AddDays(1);
            }
            return calculatedDate;
        }

        public DateTime? GetFixedWeekdayInMonthHoliday(FixedWeekdayInMonth fixedWeekdayInMonth, int year)
        {
            if (!IsValid(fixedWeekdayInMonth, year))
                return null;

            var dayToGet = ConvertWeekday(fixedWeekdayInMonth.weekday);
            var monthToGet = ConvertMonth(fixedWeekdayInMonth.month);

            if (fixedWeekdayInMonth.which == Which.LAST)
            {
                var lastDate = new DateTime(year, monthToGet + 1, 1).AddDays(-1);
                while (lastDate.DayOfWeek != dayToGet)
                {
                    lastDate = lastDate.AddDays(-1);
                }
                return lastDate;
            }

            var notLastDate = new DateTime(year, monthToGet, 1);
            while (notLastDate.DayOfWeek != dayToGet)
            {
                notLastDate = notLastDate.AddDays(1);
            }
            switch (fixedWeekdayInMonth.which)
            {
                case Which.FIRST:
                    return notLastDate;
                case Which.SECOND:
                    return notLastDate.AddDays(7);
                case Which.THIRD:
                    return notLastDate.AddDays(14);
                case Which.FOURTH:
                    return notLastDate.AddDays(21);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public DateTime? GetFixedWeekdayRelativeToFixedHoliday(FixedWeekdayRelativeToFixed fixedWeekdayRelativeToFixed, int year)
        {
            if (!IsValid(fixedWeekdayRelativeToFixed, year))
                return null;

            var fixedDate = GetFixedHolidyday(fixedWeekdayRelativeToFixed.day, year);
            if (!fixedDate.HasValue)
                return null;

            var dayToGet = ConvertWeekday(fixedWeekdayRelativeToFixed.weekday);
            if (fixedWeekdayRelativeToFixed.which == Which.LAST)
            {
                throw new NotSupportedException();
            }
            int increment;
            switch (fixedWeekdayRelativeToFixed.when)
            {
                case When.BEFORE:
                    increment = -1;
                    break;
                case When.AFTER:
                    increment = 1;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            var calculatedDate = fixedDate.Value.AddDays(increment);
            while (calculatedDate.DayOfWeek != dayToGet)
            {
                calculatedDate = calculatedDate.AddDays(1);
            }
            switch (fixedWeekdayRelativeToFixed.which)
            {
                case Which.FIRST:
                    return calculatedDate;
                case Which.SECOND:
                    return calculatedDate.AddDays(7 * increment);
                case Which.THIRD:
                    return calculatedDate.AddDays(14 * increment);
                case Which.FOURTH:
                    return calculatedDate.AddDays(21 * increment);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public DateTime? GetRelativeToEasterSundayHoliday(RelativeToEasterSunday relativeToEasterSunday, int year)
        {
            if (!IsValid(relativeToEasterSunday, year))
                return null;

            var easternSunday = GetEasternSunday(relativeToEasterSunday.chronology, year);
            // apparently this is not a moveable holiday descendant
            return easternSunday.AddDays(relativeToEasterSunday.days);
        }

        public DateTime? GetRelativeToFixedHoliday(RelativeToFixed relativeToFixed, int year)
        {
            if (!IsValid(relativeToFixed, year))
                return null;

            var fixedDate = GetFixedHolidyday(relativeToFixed.Date, year);
            if (!fixedDate.HasValue)
                return null;
            if (relativeToFixed.Item is Weekday)
            {
                var dayToGet = ConvertWeekday((Weekday)relativeToFixed.Item);
                int increment;
                switch (relativeToFixed.When)
                {
                    case When.BEFORE:
                        increment = -1;
                        break;
                    case When.AFTER:
                        increment = 1;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                var calculatedDate = fixedDate.Value.AddDays(increment);
                while (calculatedDate.DayOfWeek != dayToGet)
                {
                    calculatedDate = calculatedDate.AddDays(increment);
                }
                return MoveDate(relativeToFixed.Date, calculatedDate);
                
            }

            if (relativeToFixed.Item is int)
            {
                // What with before and after here?
                return MoveDate(relativeToFixed.Date, fixedDate.Value.AddDays((int) relativeToFixed.Item));
            }
            throw new InvalidCastException();
        }

        public DateTime? GetRelativeToWeekdayInMonthHoliday(RelativeToWeekdayInMonth relativeToWeekdayInMonth, int year)
        {
            if (!IsValid(relativeToWeekdayInMonth, year))
                return null;

            var weekDayInMonth = GetFixedWeekdayInMonthHoliday(relativeToWeekdayInMonth.FixedWeekday, year);
            if (!weekDayInMonth.HasValue)
                return null;

            var dayToGet = ConvertWeekday(relativeToWeekdayInMonth.weekday);
            int increment;
            switch (relativeToWeekdayInMonth.when)
            {
                case When.BEFORE:
                    increment = -1;
                    break;
                case When.AFTER:
                    increment = 1;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            var calculatedDate = weekDayInMonth.Value.AddDays(increment);

            while (calculatedDate.DayOfWeek != dayToGet)
            {
                calculatedDate = calculatedDate.AddDays(increment);
            }
            return calculatedDate;
        }
        #endregion

        #region helper factory methods ----------------------------------------
        //private Calendar GetCalendarForChronology(ChronologyType chronologyType)
        //{
        //    switch (chronologyType)
        //    {
        //        case ChronologyType.JULIAN:
        //           return new GregorianCalendar();
        //        case ChronologyType.GREGORIAN:
        //            return new GregorianCalendar();
        //        default:
        //            throw new ArgumentOutOfRangeException("chronologyType");
        //    }
        //}
        #endregion

        #region helper methods ------------------------------------------------
        private bool IsValidForCyle(Configurations.Holiday holiday, int year)
        {
            int cycleYears;
            switch (holiday.every)
            {
                case HolidayCycleType.EVERY_YEAR:
                    return true;
                case HolidayCycleType.Item2_YEARS:
                    cycleYears = 2;
                    break;
                case HolidayCycleType.Item4_YEARS:
                    cycleYears = 4;
                    break;
                case HolidayCycleType.Item5_YEARS:
                    cycleYears = 5;
                    break;
                case HolidayCycleType.Item6_YEARS:
                    cycleYears = 6;
                    break;
                case HolidayCycleType.ODD_YEARS:
                    return year % 2 != 0;
                case HolidayCycleType.EVEN_YEARS:
                    return year % 2 == 0;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (holiday.validFrom == 0)
                return true;

            return (year - holiday.validFrom) % cycleYears == 0;
        }

        private DateTime MoveHoliday(MovingCondition movingCondition, DateTime dateTimeToMove)
        {
            var targetWeekday = ConvertWeekday(movingCondition.weekday);
            var direction = movingCondition.with == With.NEXT ? 1 : -1;

            while (dateTimeToMove.DayOfWeek != targetWeekday)
                dateTimeToMove = dateTimeToMove.AddDays(direction);

            return dateTimeToMove;
        }

        private bool MustMove(MovingCondition movingCondition, DateTime calculatedDateTime)
        {
            switch (movingCondition.substitute)
            {
                case Weekday.SATURDAY:
                    return calculatedDateTime.DayOfWeek == DayOfWeek.Saturday;
                case Weekday.SUNDAY:
                    return calculatedDateTime.DayOfWeek == DayOfWeek.Sunday;
                case Weekday.MONDAY:
                    return calculatedDateTime.DayOfWeek == DayOfWeek.Monday;
                case Weekday.TUESDAY:
                    return calculatedDateTime.DayOfWeek == DayOfWeek.Tuesday;
                case Weekday.WEDNESDAY:
                    return calculatedDateTime.DayOfWeek == DayOfWeek.Wednesday;
                case Weekday.THURSDAY:
                    return calculatedDateTime.DayOfWeek == DayOfWeek.Thursday;
                case Weekday.FRIDAY:
                    return calculatedDateTime.DayOfWeek == DayOfWeek.Friday;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private int ConvertMonth(Month month)
        {
            switch (month)
            {
                case Month.JANUARY:
                    return 1;
                case Month.FEBRUARY:
                    return 2;
                case Month.MARCH:
                    return 3;
                case Month.MAY:
                    return 5;
                case Month.JUNE:
                    return 6;
                case Month.JULY:
                    return 7;
                case Month.AUGUST:
                    return 8;
                case Month.SEPTEMBER:
                    return 9;
                case Month.OCTOBER:
                    return 10;
                case Month.NOVEMBER:
                    return 11;
                case Month.DECEMBER:
                    return 12;
                case Month.APRIL:
                    return 4;
                default:
                    throw new ArgumentOutOfRangeException("month");
            }
        }

        private DayOfWeek ConvertWeekday(Weekday weekday)
        {
            switch (weekday)
            {
                case Weekday.MONDAY:
                    return DayOfWeek.Monday;
                case Weekday.TUESDAY:
                    return DayOfWeek.Tuesday;
                case Weekday.WEDNESDAY:
                    return DayOfWeek.Wednesday;
                case Weekday.THURSDAY:
                    return DayOfWeek.Thursday;
                case Weekday.FRIDAY:
                    return DayOfWeek.Friday;
                case Weekday.SATURDAY:
                    return DayOfWeek.Saturday;
                case Weekday.SUNDAY:
                    return DayOfWeek.Sunday;
                default:
                    throw new ArgumentOutOfRangeException("weekday");
            }
        }
        
        private DateTime GetJulianEasternSunday(int year)
        {
            var a = year%4;
            var b = year%7;
            var century = year%19;
            var d = (19*century + 15)%30;
            var e = (2*a + 4*b - d + 34)%7;
            var x = d + e + 114;
            var month = x/31;
            var day = (x%31) + 1;
            return new DateTime(year, month == 3 ? 3 : 4, day);

        }

        private DateTime GetGregorianEasterSunday(int year)
        {
            var a = year%19;
            var b = year/100;
            var c = year%100;
            var d = b/4;
            var e = b%4;
            var f = (b + 8)/25;
            var g = (b - f + 1)/3;
            var h = (19*a + b - d - g + 15)%30;
            var i = c/4;
            var j = c%4;
            var k = (32 + 2*e + 2*i - h - j)%7;
            var l = (a + 11*h + 22*k)/451;
            var x = h + k - 7*l + 114;
            var month = x/31;
            var day = (x%31) + 1;
            return new DateTime(year, month == 3 ? 3 : 4, day);
        }

        #endregion

    }
}
