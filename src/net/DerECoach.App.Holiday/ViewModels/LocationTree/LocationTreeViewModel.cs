using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using DerECoach.App.Holiday.Extensions;
using DerECoach.App.Holiday.ViewModels.HolidayGrid;
using DerECoach.Util.Holiday;

namespace DerECoach.App.Holiday.ViewModels.LocationTree
{
    internal interface ILocationTreeViewModel
    {
        List<ILocationTreeViewItemViewModel> Locations { get; }
        CultureInfo CurrentCultureInfo { get; set; }
    }

    internal class LocationTreeViewModel: ILocationTreeViewModel, INotifyPropertyChanged
    {
        #region INotifyPropertyChanged members --------------------------------
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region fields --------------------------------------------------------
        private readonly IHolidayGridViewModel _holidayGridViewModel;
        #endregion

        #region ILocationTreeViewModel members --------------------------------

        public List<ILocationTreeViewItemViewModel> Locations { get; private set; }

        private CultureInfo _currentCultureInfo;

        public CultureInfo CurrentCultureInfo
        {
            get { return _currentCultureInfo; }
            set
            {
                _currentCultureInfo = value;
                var locations = Service.GetSupportedLocations(value);
                Locations = new List<ILocationTreeViewItemViewModel>();
                locations.OrderBy(ob => ob.Description).ToList().ForEach(AddRootLocation);
                this.TriggerNotification(PropertyChanged, () => Locations);
                _holidayGridViewModel.CurrentCultureInfo = value;
            }
        }
        #endregion

        #region constructor ---------------------------------------------------

        public LocationTreeViewModel(IHolidayGridViewModel holidayGridViewModel)
        {
            _holidayGridViewModel = holidayGridViewModel;
            var locations = Service.GetSupportedLocations();
            Locations = new List<ILocationTreeViewItemViewModel>();
            locations.OrderBy(ob => ob.Description).ToList().ForEach(AddRootLocation);
        }
        #endregion

        #region fill model ----------------------------------------------------

        private void AddRootLocation(ILocation location)
        {
            var newLocation = new LocationTreeViewItemViewModel(_holidayGridViewModel, location);
            Locations.Add(newLocation);
            
        }
        #endregion
   

}
}
