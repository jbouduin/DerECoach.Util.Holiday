
using DerECoach.Util.Holiday.App.ViewModels.LocationTree;

namespace DerECoach.Util.Holiday.App.Views
{
    /// <summary>
    /// Interaction logic for LocationTreeView.xaml
    /// </summary>
    internal partial class LocationTreeView
    {
        #region constructor ---------------------------------------------------
        internal LocationTreeView()
        {
            InitializeComponent();
        }

        internal LocationTreeView(ILocationTreeViewModel locationTreeViewModel)
        {
            InitializeComponent();
            DataContext = locationTreeViewModel;
        }
        #endregion



    }
}
