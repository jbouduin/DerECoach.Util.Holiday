using DerECoach.Util.Holiday.App.ViewModels.HolidayGrid;
using DerECoach.Util.Holiday.App.ViewModels.LocationTree;
using DerECoach.Util.Holiday.App.ViewModels.MainWindow;
using DerECoach.Util.Holiday.App.Views;

namespace DerECoach.Util.Holiday.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            
            var holidayGridViewModel = new HolidayGridViewModel();
            var locationTreeViewModel = new LocationTreeViewModel(holidayGridViewModel);
            DataContext = new MainWindowViewModel(locationTreeViewModel);
            TreeViewGrid.Children.Add(new LocationTreeView(locationTreeViewModel));
            ContentGrid.Children.Add(new HolidayGrid(holidayGridViewModel));

        }

        
    }
}
