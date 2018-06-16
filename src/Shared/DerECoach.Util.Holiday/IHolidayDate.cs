using System;

namespace DerECoach.Util.Holiday
{
    public interface IHolidayDate
    {
        DateTime Date { get; }
        string Path { get; }
        string Hierarchy { get; }
        string Description { get; }
        // TODO string LocalizedHolidayType { get; }
    }
}