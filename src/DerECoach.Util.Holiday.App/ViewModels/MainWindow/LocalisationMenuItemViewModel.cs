using System.ComponentModel;
using System.Globalization;
using DerECoach.Util.Holiday.Extensions;
using DerECoach.Util.Holiday.App.Extensions;
using DerECoach.Util.Holiday.App.ViewModels.LocationTree;

namespace DerECoach.Util.Holiday.App.ViewModels.MainWindow
{
    internal class LocalisationMenuItemViewModel: INotifyPropertyChanged
    {
        #region INotifyPropertyChanged members --------------------------------
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region fields --------------------------------------------------------
        private readonly CultureInfo _cultureInfo;
        private readonly ILocationTreeViewModel _locationTreeViewModel;
        #endregion

        #region properties ----------------------------------------------------
        public string Language
        {
            get { return _cultureInfo.DisplayName; }
        }

        private bool _isChecked;

        public bool IsChecked
        {
            get { return _isChecked; }
            set
            {
                _isChecked = value;
                this.TriggerNotification(PropertyChanged, () => IsChecked);
                if (value)
                    _locationTreeViewModel.CurrentCultureInfo = _cultureInfo;
            }
        }
        #endregion

        #region constructor ---------------------------------------------------
        public LocalisationMenuItemViewModel(CultureInfo cultureInfo, ILocationTreeViewModel locationTreeViewModel)
        {
            _locationTreeViewModel = locationTreeViewModel;
            _cultureInfo = cultureInfo;
            if (cultureInfo.TwoLetterISOLanguageName == CultureInfo.CurrentUICulture.TwoLetterISOLanguageName)
                IsChecked = true;
            
        }
        #endregion

    }
}
