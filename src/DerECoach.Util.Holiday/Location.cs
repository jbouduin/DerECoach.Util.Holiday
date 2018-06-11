using System.Collections.Generic;

namespace DerECoach.Util.Holiday
{
    internal class Location: ILocation
    {
        #region fields --------------------------------------------------------
        #endregion
        
        #region ILocation Members ---------------------------------------------
        public string Path { get; private set; }
        public string Description { get; internal set; }
        public List<ILocation> Children { get; private set; }
        #endregion

        #region constructor ---------------------------------------------------
        private Location(string description)
        {
            Children = new List<ILocation>();
            Description = description;
        }
        #endregion

        #region factory methods -----------------------------------------------

        internal static Location CreateRootLocation(string hierarchy, string description)
        {
            var result = new Location(description) {Path = hierarchy};
            return result;
        }

        internal Location AddChild(string hierarchy, string description)
        {
            var result = new Location(description) {Path = string.Format(@"{0}/{1}", Path, hierarchy)};
            Children.Add(result);
            return result;
        }
        #endregion
    }
}
