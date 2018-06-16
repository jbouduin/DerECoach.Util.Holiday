using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using DerECoach.Util.Holiday.Extensions;
using DerECoach.App.Holiday.Extensions;
using DerECoach.App.Holiday.ViewModels.LocationTree;

namespace DerECoach.App.Holiday.ViewModels.MainWindow
{
    internal class MainWindowViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged members --------------------------------

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region ViewMembers ---------------------------------------------------

        private ObservableCollection<LocalisationMenuItemViewModel> _menuItemViewModels =
            new ObservableCollection<LocalisationMenuItemViewModel>();

        public ObservableCollection<LocalisationMenuItemViewModel> MenuItemViewModels
        {
            get { return _menuItemViewModels; }
            set
            {
                _menuItemViewModels = value;
                this.TriggerNotification(PropertyChanged, () => MenuItemViewModels);
            }
        }

        #endregion

        #region constructor ---------------------------------------------------

        public MainWindowViewModel(ILocationTreeViewModel locationTreeViewModel)
        {
            _menuItemViewModels.Add(new LocalisationMenuItemViewModel(CultureInfo.InvariantCulture,
                locationTreeViewModel));
            _menuItemViewModels.Add(new LocalisationMenuItemViewModel(CultureInfo.GetCultureInfo("de-DE"),
                locationTreeViewModel));
            _menuItemViewModels.Add(new LocalisationMenuItemViewModel(CultureInfo.GetCultureInfo("en-US"),
                locationTreeViewModel));
            _menuItemViewModels.Add(new LocalisationMenuItemViewModel(CultureInfo.GetCultureInfo("nl-BE"),
                locationTreeViewModel));
            _menuItemViewModels.Add(new LocalisationMenuItemViewModel(CultureInfo.GetCultureInfo("fr-FR"),
                locationTreeViewModel));
            _menuItemViewModels.Add(new LocalisationMenuItemViewModel(CultureInfo.GetCultureInfo("pt-PT"),
                locationTreeViewModel));

        }

        #endregion
    }
}
