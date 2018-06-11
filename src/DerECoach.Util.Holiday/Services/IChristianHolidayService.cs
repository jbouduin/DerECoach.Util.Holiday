using System;
using DerECoach.Util.Holiday.Configurations;

namespace DerECoach.Util.Holiday.Services
{
    internal interface IChristianHolidayService
    {
        DateTime? GetChristianHoliday(ChristianHoliday christianHoliday, int year);
    }
}
