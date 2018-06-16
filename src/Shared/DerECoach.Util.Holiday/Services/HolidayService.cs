using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using DerECoach.Util.Holiday.Configurations;

namespace DerECoach.Util.Holiday.Services
{
    internal class HolidayService: IHolidayService
    {
        #region fields --------------------------------------------------------
        private readonly IConfigurationService _configurationService;
        private readonly IChristianHolidayService _christianHolidayService;
        private readonly ICalendarService _calendarService;
        private readonly ILocalizationService _localizationService;
        #endregion

        #region constructors --------------------------------------------------
        public HolidayService(IConfigurationService configurationService, ILocalizationService localizationService,
            IChristianHolidayService christianHolidayService, ICalendarService calendarService)
        {
            _configurationService = configurationService;
            _christianHolidayService = christianHolidayService;
            _calendarService = calendarService;
            _localizationService = localizationService;
        }

        #endregion

        #region IHolidayService members ---------------------------------------

        public IEnumerable<IHolidayDate> GetHolidayDates(string hierarchyPath, int year, CultureInfo cultureInfo)
        {
            _localizationService.SetCurrentCulture(cultureInfo);
            var definitions = _configurationService.GetHolidays(hierarchyPath);
            var result = new List<IHolidayDate>();
            definitions.ToList().ForEach(fe => result.AddRange(ProcessHolidays(fe.Key, fe.Value, year)));
            return result;
        }

        #endregion

        #region helper methods ------------------------------------------------

        private IEnumerable<IHolidayDate> ProcessHolidays(string path, Configurations.Holidays holidays, int year)
        {
            var result = new List<IHolidayDate>();
            holidays.ChristianHoliday.ForEach(fe =>
            {
                var date = _christianHolidayService.GetChristianHoliday(fe, year);
                if (date.HasValue)
                    result.Add(new HolidayDate(date.Value, path, path,
                        _localizationService.GetChristianHolidayDescription(fe.type.ToString())));
            });

            // TODO
            holidays.EthiopianOrthodoxHoliday.ForEach(fe =>
            {
                    
            });

            holidays.Fixed.ForEach(fe =>
            {
                var date = _calendarService.GetFixedHolidyday(fe, year);
                if (date.HasValue)
                    result.Add(new HolidayDate(date.Value, path, path,
                        _localizationService.GetHolidayDescription(fe.descriptionPropertiesKey)));
            });

            holidays.FixedWeekday.ForEach(fe =>
            {
                var date = _calendarService.GetFixedWeekdayInMonthHoliday(fe, year);
                if (date.HasValue)
                    result.Add(new HolidayDate(date.Value, path, path,
                        _localizationService.GetHolidayDescription(fe.descriptionPropertiesKey)));
            });

            holidays.FixedWeekdayBetweenFixed.ForEach(fe =>
            {
                var date = _calendarService.GetFixedWeekdayBetweenFixedHoliday(fe, year);
                if (date.HasValue)
                    result.Add(new HolidayDate(date.Value, path, path,
                        _localizationService.GetHolidayDescription(fe.descriptionPropertiesKey)));
            });

            holidays.FixedWeekdayRelativeToFixed.ForEach(fe =>
            {
                var date = _calendarService.GetFixedWeekdayRelativeToFixedHoliday(fe, year);
                if (date.HasValue)
                    result.Add(new HolidayDate(date.Value, path, path,
                        _localizationService.GetHolidayDescription(fe.descriptionPropertiesKey)));
            });

            // TODO
            holidays.HebrewHoliday.ForEach(fe =>
            {
                
            });

            holidays.HinduHoliday.ForEach(fe =>
            {
                
            });

            holidays.IslamicHoliday.ForEach(fe =>
            {
                
            });

            holidays.RelativeToEasterSunday.ForEach(fe =>
            {
                var date = _calendarService.GetRelativeToEasterSundayHoliday(fe, year);
                if (date.HasValue)
                    result.Add(new HolidayDate(date.Value, path, path,
                        _localizationService.GetHolidayDescription(fe.descriptionPropertiesKey)));
            });

            holidays.RelativeToFixed.ForEach(fe =>
            {
                var date = _calendarService.GetRelativeToFixedHoliday(fe, year);
                if (date.HasValue)
                    result.Add(new HolidayDate(date.Value, path, path,
                        _localizationService.GetHolidayDescription(fe.descriptionPropertiesKey)));
            });

            holidays.RelativeToWeekdayInMonth.ForEach(fe =>
            {
                var date = _calendarService.GetRelativeToWeekdayInMonthHoliday(fe, year);
                if (date.HasValue)
                    result.Add(new HolidayDate(date.Value, path, path,
                        _localizationService.GetHolidayDescription(fe.descriptionPropertiesKey)));
            });
            return result;
        }

        #endregion

    }
}
