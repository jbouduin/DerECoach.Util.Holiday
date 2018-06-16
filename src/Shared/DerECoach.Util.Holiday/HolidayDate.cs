using System;

namespace DerECoach.Util.Holiday
{
    internal class HolidayDate : IHolidayDate
    {
        public DateTime Date { get; private set; }
        public string Path { get; private set; }
        public string Hierarchy { get; private set; }
        public string Description { get; private set; }
        // TODO public string LocalizedHolidayType { get; private set }

        internal HolidayDate(DateTime date, string path, string hierarchy, string description)
        {
            Date = date;
            Path = path;
            Hierarchy = hierarchy;
            Description = description;
        }
    }
}
