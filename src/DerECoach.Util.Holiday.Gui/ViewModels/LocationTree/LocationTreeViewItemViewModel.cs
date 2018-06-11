using System.Collections.Generic;
using System.Linq;
using DerECoach.Util.Holiday.Gui.ViewModels.HolidayGrid;

namespace DerECoach.Util.Holiday.Gui.ViewModels.LocationTree
{
    internal interface ILocationTreeViewItemViewModel
    {
        List<ILocationTreeViewItemViewModel> Children { get; }
        bool IsSelected { get; set; }
        bool IsExpanded { get; set; }
        string Flag { get; }
        string Description { get; }
    }

    internal class LocationTreeViewItemViewModel : ILocationTreeViewItemViewModel
    {
        #region fields --------------------------------------------------------
        private readonly ILocation _location;
        private readonly IHolidayGridViewModel _holidayGridViewModel;
        #endregion

        #region ILocationTreeViewItemViewModel members ------------------------
        private readonly List<ILocationTreeViewItemViewModel> _children = new List<ILocationTreeViewItemViewModel>();
        public List<ILocationTreeViewItemViewModel> Children
        {
            get { return _children; }
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get
            {
                return _isSelected;
            }
            set
            {
                _isSelected = value;
                if (value)
                    _holidayGridViewModel.CurrentLocation = _location;
                
            }
        }

        public bool IsExpanded { get; set; }

        public string Flag
        {
            get
            {
                return null;
                //_location.Flag; 
            }
        }

        public string Description
        {
            get { return _location.Description; }
        }
        #endregion

        #region constructor ---------------------------------------------------

        internal LocationTreeViewItemViewModel(IHolidayGridViewModel holidayGridViewModel, ILocation location)
        {
            _location = location;
            _holidayGridViewModel = holidayGridViewModel;
            _location.Children.OrderBy(ob => ob.Description).ToList().ForEach(sub =>
            {
                var newitem = new LocationTreeViewItemViewModel(holidayGridViewModel, sub);
                Children.Add(newitem);
            });
        }
        #endregion
    }
}
