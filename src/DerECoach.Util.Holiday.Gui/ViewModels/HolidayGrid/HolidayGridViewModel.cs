using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using DerECoach.Util.Holiday.Gui.Extensions;

namespace DerECoach.Util.Holiday.Gui.ViewModels.HolidayGrid
{
    internal interface IHolidayGridViewModel
    {
        ILocation CurrentLocation { get; set; }
        ObservableCollection<IHolidayDate> CurrentHolidays { get; }
        CultureInfo CurrentCultureInfo { get; set; }
    }

    internal class HolidayGridViewModel: IHolidayGridViewModel, INotifyPropertyChanged
    {
        #region INotifyPropertyChanged members --------------------------------
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region IHolidayGridViewModel members ---------------------------------

        private CultureInfo _currentCultureInfo;

        public CultureInfo CurrentCultureInfo
        {
            get { return _currentCultureInfo; }
            set
            {
                _currentCultureInfo = value;
                LoadHolidays();
            }
        }
        private ILocation _currentLocation;
        public ILocation CurrentLocation
        {
            get
            {
                return _currentLocation;
            }
            set
            {
                _currentLocation = value; 
                this.TriggerNotification(PropertyChanged, () => CurrentLocation);
                LoadHolidays();
                //this.TriggerNotification(PropertyChanged, () => CurrentHolidays);
            }
        }

        private ObservableCollection<IHolidayDate> _currentHolidays = new ObservableCollection<IHolidayDate>();

        public ObservableCollection<IHolidayDate> CurrentHolidays
        {
            get { return _currentHolidays; }
            set
            {
                _currentHolidays = value;
                this.TriggerNotification(PropertyChanged, () => CurrentHolidays);
            }
        }
        #endregion

        #region private methods -----------------------------------------------

        private void LoadHolidays()
        {
            if (CurrentLocation == null)
                return;

            var serviceResult = Service.GetHolidayService().GetHolidayDates(CurrentLocation.Path, DateTime.Today.Year, CurrentCultureInfo);

            _currentHolidays.Clear();
            serviceResult.OrderBy(ob => ob.Date).ToList().ForEach(_currentHolidays.Add);
        }

        #endregion
        
    }
}
