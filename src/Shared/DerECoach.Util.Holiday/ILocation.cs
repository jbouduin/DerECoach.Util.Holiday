using System.Collections.Generic;

namespace DerECoach.Util.Holiday
{
    public interface ILocation
    {
        // TODO Flag { get; }
        string Path { get; }
        string Description { get; }
        List<ILocation> Children { get; }
    }
}
