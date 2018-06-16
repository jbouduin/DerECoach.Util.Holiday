using System.Collections.Generic;
using System.Globalization;

namespace DerECoach.Util.Holiday
{

    public interface IHolidayService
    {
        IEnumerable<IHolidayDate> GetHolidayDates(string hierarchyPath, int year, CultureInfo cultureInfo);
    }
}
