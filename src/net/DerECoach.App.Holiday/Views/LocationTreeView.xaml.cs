
using DerECoach.App.Holiday.ViewModels.LocationTree;

namespace DerECoach.App.Holiday.Views
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
